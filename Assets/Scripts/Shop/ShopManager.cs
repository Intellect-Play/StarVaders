using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;
using Unity.VisualScripting;
using System.Collections;

public class ShopManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] BuyUpgradeShopButton BuyorUpgradeButton;
    [SerializeField] TextMeshProUGUI CoinText;

    public Transform cardButtonParent;
    public GameObject cardButtonPrefab;

    public CardDataList cardDataList;
    public string jsonPath;
    public Dictionary<int, CardList> cardButtons = new Dictionary<int, CardList>();

    private GetcardImage getcardImage;
    private int selectedCardId = -1;
    private CardAction selectedCardAction = CardAction.None;
    private CardData selectedCardData;
    int coins;
    private void Start()
    {
        getcardImage = GetComponent<GetcardImage>();
        //DeleteSaveFile();
        cardDataList = SaveManager.Instance.cardDataList;
        StartCoroutine(StartTime());
        CoinText.text= SaveManager.Instance.saveData.playerData.coins.ToString();
    }

    IEnumerator StartTime()
    {
       
        CreateCardButtons(); 
        yield return new WaitForSeconds(.1f);

        cardButtonParent.GetComponent<GridLayoutGroup>().enabled = false; // GridLayoutGroup'u devre dışı bırak
        BuyorUpgradeButton.button.onClick.AddListener(BuyOrUpgradeFunc);

    }
    #region Data Load/Save

    private void LoadData()
    {
        if (File.Exists(jsonPath))
        {
            Debug.Log("cards.json yüklendi: " + jsonPath);

            string json = File.ReadAllText(jsonPath);
            cardDataList = JsonUtility.FromJson<CardDataList>(json);
        }
        else
        {
            TextAsset jsonAsset = Resources.Load<TextAsset>("Data/cards");
            Debug.Log("cards.json " + jsonPath);
         
            if (jsonAsset != null)
            {
                Debug.Log("cards.json bulunamadı, varsayılan veriler yüklenecek: " + jsonPath);
                cardDataList = JsonUtility.FromJson<CardDataList>(jsonAsset.text);
                // File.WriteAllText(jsonPath, json);
            }
            else
            {
                Debug.Log("varsayılan veriler yüklenecek: " + jsonPath);

                // JSON yoksa 6 örnek kart yarat
                cardDataList = new CardDataList { cards = new List<CardData>() };
                for (int i = 0; i < 6; i++)
                {
                    cardDataList.cards.Add(new CardData
                    {
                        id = i,
                        name = "Card " + (i + 1),
                        power = 10,
                        isUnlocked = false,
                        buyCost = 100 + i * 50,
                        upgradeCost = 50 + i * 30
                    });
                }
                SaveData();
            }
        }
    }
    public void DeleteSaveFile()
    {
        jsonPath = Path.Combine(Application.persistentDataPath, "Data/cards.json");

        if (File.Exists(jsonPath))
        {
            File.Delete(jsonPath);
            Debug.Log("cards.json dosyası silindi: " + jsonPath);
        }
        else
        {
            Debug.LogWarning("Silinecek cards.json dosyası bulunamadı: " + jsonPath);
        }
    }


    private void SaveData()
    {
        string directory = Path.GetDirectoryName(jsonPath);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        string json = JsonUtility.ToJson(cardDataList, true);
        File.WriteAllText(jsonPath, json);
    }


    #endregion

    #region UI

    private void CreateCardButtons()
    {
        foreach (var card in cardDataList.cards)
        {
            GameObject buttonGO = Instantiate(cardButtonPrefab, cardButtonParent);
            cardButtons[card.id] = new CardList(buttonGO);
            buttonGO.GetComponent<CardButtonUI>().SetCardDetails(
                card,
                getcardImage.cardImagesList.Find(x => x.cardType.ToString() == card.name).cardImage
            );
            UpdateCardButtonUI(card);
        }
    }

    private void UpdateCardButtonUI(CardData card)
    {

        GameObject btn = cardButtons[card.id]?.cardObject;
        var ui = btn.GetComponent<CardButtonUI>();
        ui.cardId = card.id;
        ui.nameText.text = card.name;
        ui.powerText.text = "Power: " + card.power;
        Debug.Log($"Updating UI for card: {card.name} (ID: {card.id})+{cardButtons[card.id].cardButtonUI.nameCard}");

        ui.actionButton.onClick.RemoveAllListeners();

        if (card.isUnlocked)
        {
            //cardButtons[card.id].cardButtonUI.UpgradeCard();

            ui.actionButtonText.text = $"Upgrade ({card.upgradeCost})";
            ui.actionButton.onClick.AddListener(() => SelectCard(card, CardAction.Upgrade));
        }
        else
        {
            ui.actionButtonText.text = $"Buy ({card.buyCost})";
            ui.actionButton.onClick.AddListener(() => SelectCard(card,CardAction.Buy));
        }
    }

    #endregion

    #region Actions

    public void BuyCard(int cardId)
    {
        var card = cardDataList.cards.Find(c => c.id == cardId);
        if (card == null || card.isUnlocked) return;

        coins = SaveManager.Instance.saveData.playerData.coins; // debug amaçlı varsayılan yüksek
        if (coins >= card.buyCost)
        {
            Debug.Log($"Kart satın alınıyor: {card.name} (ID: {card.id}), Fiyat: {card.buyCost}, Mevcut Para: {coins}");
            cardButtons[cardId].cardButtonUI.BuyCard();
            SaveManager.Instance.saveData.playerData.coins = coins;

            coins -= card.buyCost;
            PlayerPrefs.SetInt("Coins", coins);

            card.isUnlocked = true;
            SaveData();
            UpdateCardButtonUI(card);
        }
        else
        {
            Debug.Log("Yetersiz para.");
        }
    }
  
  
    public void UpgradeCard(int cardId)
    {
        var card = cardDataList.cards.Find(c => c.id == cardId);
        if (card == null || !card.isUnlocked) return;

        coins = SaveManager.Instance.saveData.playerData.coins;
        if (coins >= card.upgradeCost)
        {
            cardButtons[cardId].cardButtonUI.UpgradeCard();

            Debug.Log($"Kart yükseltiliyor: {card.name} (ID: {card.id}), Fiyat: {card.upgradeCost}, Mevcut Para: {coins}");
            coins -= card.upgradeCost;
            SaveManager.Instance.saveData.playerData.coins = coins;
            PlayerPrefs.SetInt("Coins", coins);

            card.power += 10;
            card.upgradeCost += 50;
            SaveData();
            UpdateCardButtonUI(card);
        }
        else
        {
            Debug.Log("Yetersiz para.");
        }
    }

    public void SelectCard(CardData card, CardAction cardAction)
    {
        if (selectedCardId != -1) cardButtons[selectedCardId].cardButtonUI.SelectCard(false);
        cardButtons[card.id].cardButtonUI.SelectCard(true);
        Debug.Log("select Card");
        selectedCardAction = cardAction;
        selectedCardId = card.id;
        int coins = SaveManager.Instance.saveData.playerData.coins;
        int buyOrUpgrade = cardAction == CardAction.Buy ? card.buyCost : card.upgradeCost;
        bool activeBuy = cardAction == CardAction.Buy ? coins >= card.buyCost : coins >= card.upgradeCost;
        BuyorUpgradeButton.SetButtonText(activeBuy, buyOrUpgrade, cardAction);
    }


    #endregion


    public void BuyOrUpgradeFunc()
    {
        if (selectedCardId == -1 || selectedCardAction == CardAction.None)
        {
            Debug.Log("Lütfen bir kart seçin.");
            return;
        }
        if (selectedCardAction == CardAction.Buy)
        {
            BuyCard(selectedCardId);
        }
        else if (selectedCardAction == CardAction.Upgrade)
        {
            UpgradeCard(selectedCardId);
        }
        SaveManager.Instance.saveData.playerData.coins = coins;
        SaveManager.Instance.Save();
        CoinText.text = coins.ToString();
       // UpdateCardButtonUI(selectedCardData);
    }
}
public enum CardAction
{
    Buy,
    Upgrade,
    None
}
[System.Serializable]
public class CardList
{
    public GameObject cardObject;
    public CardButtonUI cardButtonUI;

    public CardList(GameObject cardObject)
    {
        this.cardObject = cardObject;
        this.cardButtonUI = cardObject.GetComponent<CardButtonUI>();
    }
}