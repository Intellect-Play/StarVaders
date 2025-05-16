using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;

public class CardManagerMove : MonoBehaviour
{
    public List<GameObject> cardPrefabs;
    public Transform spawnParent;
    public Transform spawnButtonTransform;
    public int cardCount = 5;

    private List<GameObject> spawnedCards = new List<GameObject>();
    [SerializeField] private Canvas canvas;

    public void SpawnCards()
    {
        ClearCards();

        for (int i = 0; i < cardCount; i++)
        {
            GameObject randomPrefab = cardPrefabs[Random.Range(0, cardPrefabs.Count)];

            GameObject card = Instantiate(randomPrefab, Vector3.zero, Quaternion.identity, spawnParent);
            RectTransform cardRect = card.GetComponent<RectTransform>();

            Vector3 targetPos = Vector3.zero;
            //cardRect.DOLocalMove(targetPos, 0.5f).SetEase(Ease.OutBack);

            card.GetComponent<CardClick>().Initialize(spawnParent, canvas);

            spawnedCards.Add(card);
        }
    }

    public void ReturnAllCards()
    {
        //foreach (var card in spawnedCards)
        //{
        //    if (card != null)
        //        card.GetComponent<CardClick>().ReturnToStart();
        //}
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
