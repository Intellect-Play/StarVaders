using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;

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
    private Mask maskCards;
    private Image imageCards;

    public static bool MoveCard = false;
    float time = 0.1f;

    private void Start()
    {
        maskCards = cardFollowPrefabParent.GetComponent<Mask>();
        imageCards = cardFollowPrefabParent.GetComponent<Image>();
    }
    public void SpawnCards()
    {
        ClearCards();
        VisualMask(true);

        StartCoroutine(SpawnCardWithTime(time));
    }
    IEnumerator SpawnCardWithTime(float _time)
    {
        for (int i = 0; i < cardCount; i++)
        {
            GameObject randomPrefab = cardPrefabs[Random.Range(0, cardPrefabs.Count)];
            GameObject card = Instantiate(randomPrefab, Vector3.zero, Quaternion.identity, spawnParent);
            GameObject cardFollow = Instantiate(cardFollowPrefab, Vector3.zero, Quaternion.identity, cardFollowPrefabParent);
            RectTransform cardRect = card.GetComponent<RectTransform>();
            cardFollow.GetComponent<CardMoveImage>().spawnParent = cardFollowPrefabParent;
            Vector3 targetPos = Vector3.zero;
            card.GetComponent<CardClick>().Initialize(spawnParent, canvas, cardFollow);
            spawnedCards.Add(card);
            yield return new WaitForSeconds(_time);
        }yield return new WaitForSeconds(_time);
    }
    public void ReturnAllCards()
    {
        VisualMask(false);
        foreach (var card in spawnedCards)
        {
            if (card != null) {
                card.transform.parent = cardExit;
                card.GetComponent<CardClick>().moveImage.CardDestroy();
                card.transform.localPosition = Vector3.zero;
            }
                
        }
    }
    void VisualMask(bool active)
    {
        maskCards.enabled = active;
        imageCards.enabled = active;
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
