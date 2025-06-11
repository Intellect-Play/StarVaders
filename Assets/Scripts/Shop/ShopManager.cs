using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ShopManager : MonoBehaviour
{
    [Header("UI")]
    public Transform cardButtonParent;
    public GameObject cardButtonPrefab;

    public CardDataList cardDataList;
    public string jsonPath;
    public Dictionary<int, GameObject> cardButtons = new Dictionary<int, GameObject>();

    private void Start()
    {
       // DeleteSaveFile();
        jsonPath = Path.Combine(Application.persistentDataPath, "Data/cards"); ;
        LoadData();
        CreateCardButtons();
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
          //  string json = File.ReadAllText(jsonPath);
          //  saveData = JsonUtility.FromJson<PlayerSaveData>(json);
            // İlk defa çalışıyorsa, default veriler
            //TextAsset defaultJson = Resources.Load<TextAsset>("Data/cards");
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
        string path = Application.persistentDataPath + "/cards.json";

        if (File.Exists(path))
        {
            File.Delete(path);
            Debug.Log("cards.json faylı silindi.");
        }
        else
        {
            Debug.LogWarning("Silinəcək cards.json faylı tapılmadı.");
        }
    }

    private void SaveData()
    {
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
            cardButtons[card.id] = buttonGO;

            UpdateCardButtonUI(card);
        }
    }

    private void UpdateCardButtonUI(CardData card)
    {
        GameObject btn = cardButtons[card.id];
        var ui = btn.GetComponent<CardButtonUI>();

        ui.cardId = card.id;
        ui.nameText.text = card.name;
        ui.powerText.text = "Power: " + card.power;

        ui.actionButton.onClick.RemoveAllListeners();

        if (card.isUnlocked)
        {
            ui.actionButtonText.text = $"Upgrade ({card.upgradeCost})";
            ui.actionButton.onClick.AddListener(() => UpgradeCard(card.id));
        }
        else
        {
            ui.actionButtonText.text = $"Buy ({card.buyCost})";
            ui.actionButton.onClick.AddListener(() => BuyCard(card.id));
        }
    }

    #endregion

    #region Actions

    public void BuyCard(int cardId)
    {
        var card = cardDataList.cards.Find(c => c.id == cardId);
        if (card == null || card.isUnlocked) return;

        int coins = PlayerPrefs.GetInt("Coins", 9999); // debug amaçlı varsayılan yüksek
        if (coins >= card.buyCost)
        {
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

        int coins = PlayerPrefs.GetInt("Coins", 9999);
        if (coins >= card.upgradeCost)
        {
            coins -= card.upgradeCost;
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

    #endregion
}
