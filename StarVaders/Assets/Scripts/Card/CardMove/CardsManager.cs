using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardsManager : MonoBehaviour
{
    public GameObject SelectedCard;
    public GameObject HoveringMenu;

    public CardsLayoutGroup cardsLayoutGroup;

    public GameObject CardParent;
    public List<GameObject> CardsFaces = new List<GameObject>();

    private void Start()
    {
        AddCard();
        AddCard();
        AddCard();
        AddCard();
        AddCard();
        AddCard();
    }

    public void AddCard()
    {
        if(cardsLayoutGroup.transform.childCount < 6)
        {
            GameObject card = Instantiate(CardParent, cardsLayoutGroup.transform);

            int randomCard = Random.Range(0, CardsFaces.Count);
            GameObject cardFace = Instantiate(CardsFaces[randomCard], GameObject.Find("CardVisuals").transform);

            cardFace.GetComponent<CardFace>().target = card.GetComponentInChildren<Card>().gameObject;
        }
    }
}
