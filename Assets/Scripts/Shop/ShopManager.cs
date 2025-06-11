using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public Transform cardButtonParent; // Butonlar bu parent altında
    public GameObject cardButtonPrefab; // UI prefabı

    private CardDataList cardDataList;
    private string jsonPath;
    private Dictionary<int, GameObject> cardButtons = new Dictionary<int, GameObject>();

    void Start()
    {
        jsonPath = Application.persistentDataPath + "/cards.json";

        LoadData();
        CreateCardButtons();
    }

    void LoadData()
    {
        if (File.Exists(jsonPath))
        {
            string json = File.ReadAllText(jsonPath);
            cardDataList = JsonUtility.FromJson<CardDataList>(json);
        }
        else
        {
            // İlk kez açılıyorsa varsayılan veriler
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

    void SaveData()
    {
        string json = JsonUtility.ToJson(cardDataList, true);
        File.WriteAllText(jsonPath, json);
    }

    void CreateCardButtons()
    {
        foreach (var card in cardDataList.cards)
        {
            GameObject buttonGO = Instantiate(cardButtonPrefab, cardButtonParent);
            cardButtons[card.id] = buttonGO;

            UpdateCardButtonUI(card);
        }
    }

    void UpdateCardButtonUI(CardData card)
    {
        GameObject btn = cardButtons[card.id];
        var ui = btn.GetComponent<CardButtonUI>();

        ui.cardId = card.id;
        ui.nameText.text = card.name;
        ui.powerText.text = "Power: " + card.power;

        ui.actionButton.onClick.RemoveAllListeners();

        if (card.isUnlocked)
        {
            ui.actionButtonText.text = "Upgrade (" + card.upgradeCost + ")";
            ui.actionButton.onClick.AddListener(() => UpgradeCard(ui.cardId));
        }
        else
        {
            ui.actionButtonText.text = "Buy (" + card.buyCost + ")";
            ui.actionButton.onClick.AddListener(() => BuyCard(ui.cardId));
        }
    }


    void BuyCard(int cardId)
    {
        var card = cardDataList.cards.Find(c => c.id == cardId);

        // Burada para kontrolü ekleyin (örn: PlayerPrefs.GetInt("Coins"))
        if (true) // yeterli para varsa
        {
            card.isUnlocked = true;
            SaveData();
            UpdateCardButtonUI(card);
        }
    }

    void UpgradeCard(int cardId)
    {
        var card = cardDataList.cards.Find(c => c.id == cardId);

        // Para kontrolü
        if (true)
        {
            card.power += 10;
            card.upgradeCost += 50;
            SaveData();
            UpdateCardButtonUI(card);
        }
    }
}
