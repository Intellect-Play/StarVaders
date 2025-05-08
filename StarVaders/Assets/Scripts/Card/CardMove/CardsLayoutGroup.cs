using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CardsLayoutGroup : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public CardsManager cardsManager;
    public GameObject SelectedCard;
    public List<GameObject> Cards = new List<GameObject>();

    private void Update()
    {
        if(cardsManager.SelectedCard)
            SelectedCard = cardsManager.SelectedCard;
        else
            SelectedCard = null;

        if (SelectedCard)
        {
            if (cardsManager.HoveringMenu == gameObject)
            {
                for (int i = 0; i < Cards.Count; i++)
                {
                    if (SelectedCard.transform.position.x > Cards[i].transform.position.x)
                    {
                        if (SelectedCard.transform.parent.GetSiblingIndex() < Cards[i].transform.parent.GetSiblingIndex())
                        {
                            SwapCards(SelectedCard, Cards[i]);
                            break;
                        }
                    }

                    if (SelectedCard.transform.position.x < Cards[i].transform.position.x)
                    {
                        if (SelectedCard.transform.parent.GetSiblingIndex() > Cards[i].transform.parent.GetSiblingIndex())
                        {
                            SwapCards(SelectedCard, Cards[i]);
                            break;
                        }
                    }
                }
            }
        }
    }

    public void SwapCards(GameObject currentCard, GameObject targetCard)
    {
        Transform currentCardParent = currentCard.transform.parent;
        Transform targetedCardParent = targetCard.transform.parent;

        currentCard.transform.SetParent(targetedCardParent);
        targetCard.transform.SetParent(currentCardParent);

        if (!currentCard.transform.GetComponent<Card>().IsDragging)
        {
            currentCard.transform.localPosition = Vector2.zero;
        }

        targetCard.transform.localPosition = Vector2.zero;

    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        cardsManager.HoveringMenu = gameObject;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        cardsManager.HoveringMenu = null;
    }
}
