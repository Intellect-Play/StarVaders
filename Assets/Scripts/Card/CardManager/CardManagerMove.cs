using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class CardManagerMove : MonoBehaviour
{
    public List<GameObject> cardPrefabs;
    public Transform spawnParent;
    public Transform spawnButtonTransform;
    public GameObject cardFollowPrefab;
    public Transform cardFollowPrefabParent;
    [SerializeField] private Transform cardExit;
    public int cardCount = 5;

    private List<GameObject> spawnedCards = new List<GameObject>();
    [SerializeField] private Canvas canvas;

    public static bool MoveCard = false;
    public void SpawnCards()
    {
        ClearCards();

        for (int i = 0; i < cardCount; i++)
        {
            GameObject randomPrefab = cardPrefabs[Random.Range(0, cardPrefabs.Count)];

            GameObject card = Instantiate(randomPrefab, Vector3.zero, Quaternion.identity, spawnParent);
            GameObject cardFollow = Instantiate(cardFollowPrefab, Vector3.zero, Quaternion.identity, cardFollowPrefabParent);
            RectTransform cardRect = card.GetComponent<RectTransform>();
            cardFollow.GetComponent<CardMoveImage>().spawnParent = cardFollowPrefabParent;
            Vector3 targetPos = Vector3.zero;

            card.GetComponent<CardClick>().Initialize(spawnParent, canvas,cardFollow);

            spawnedCards.Add(card);
        }
    }

    public void ReturnAllCards()
    {
        foreach (var card in spawnedCards)
        {
            if (card != null)
                card.transform.parent=cardExit;
            card.GetComponent<CardClick>().moveImage.CardDestroy();
            card.transform.localPosition=Vector3.zero;
        }
    }

    private void ClearCards()
    {
        foreach (var card in spawnedCards)
        {
            if (card != null)
                Destroy(card);
        }
        spawnedCards.Clear();
    }
}
