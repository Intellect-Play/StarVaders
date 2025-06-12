using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetcardImage : MonoBehaviour
{
    [SerializeField] CardManagerMove cardManagerMove;
    public List<CardImages> cardImagesList = new List<CardImages>();
    private void Awake()
    {
        Debug.Log("Card Prefab: ");

        foreach (GameObject cardPrefab in cardManagerMove.cardPrefabs)
        {
            Debug.Log("Card Prefab: " + cardPrefab.name);
            CardImages card = new CardImages();
            card.cardType = cardPrefab.GetComponent<CardBase>()._CardType;
            card.cardImage = cardPrefab.GetComponent<Image>().sprite;
            cardImagesList.Add(card);
        }
    }
}
[System.Serializable]
public class CardImages
{
    public CardType cardType;
    public Sprite cardImage;
}