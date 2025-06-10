using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using DG.Tweening;
using System.Collections;
using static Unity.VisualScripting.Member;
using static UnityEngine.GraphicsBuffer;

public class CardManagerMove : MonoBehaviour
{
    public static CardManagerMove Instance;
    public List<GameObject> cardPrefabs;
    public Transform spawnParent;
    public Transform spawnButtonTransform;
    public GameObject cardFollowPrefab;
    public Transform cardFollowPrefabParent;
    [SerializeField] private Transform cardSpawn;
    [SerializeField] private Transform cardExit;

    public int cardCount = 5;
    public bool TutorialBool;
    public List<GameObject> spawnedCards = new List<GameObject>();
    public CardClick currentCardClick;
    [SerializeField] private Canvas canvas;
    [SerializeField] private CardStartButton cardStartButton;
    [SerializeField] public RectTransform rectTransformCardLimit;
    [SerializeField] public float spawnSpace;

    private Mask maskCards;
    private Image imageCards;
    public int CardLimit = 4;
    int MoveCardLimit = 1;

    public static bool MoveCard = false;
    float time = 0.1f;
    public int cardNumber;
    bool healthCard = false;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        TutorialBool = false;
    }
    private void Start()
    {
        maskCards = cardFollowPrefabParent.GetComponent<Mask>();
        imageCards = cardFollowPrefabParent.GetComponent<Image>();
        healthCard = false;
    }
    public void SpawnCards()
    {
        DestroyAllCards();
       // Debug.Log("SpawnCards");
        //ClearCards();
        VisualMask(true);
        StartCoroutine(SpawnCardWithTime(time));
        //if (spawnedCards.Count< CardLimit)
        //{
        //    if(SaveManager.Instance.saveData.playerData.currentLevel==1) StartCoroutine(SpawnCardWithTimeFotTutorial(time));
        //    else StartCoroutine(SpawnCardWithTime(time));
        //}
    }
    IEnumerator SpawnCardWithTime(float _time)
    {
        int j = 0;

        int moveCount = 0;
        foreach (var card in spawnedCards)
        {
            if (card.GetComponent<Move>() != null) moveCount++;
        }
        if (moveCount <= MoveCardLimit)
        {
            j = 1;
            SpawnCard(0);
            //SpawnCard(8);

        }
        else j = 0;
        //cardCount = CardLimit - spawnedCards.Count + 1;
        for (int i = j ; i < CardLimit; i++)
        {
            cardNumber = Random.Range(1, cardPrefabs.Count);
            if (!healthCard && cardNumber == 5) { 
                cardPrefabs.RemoveAt(5);
                healthCard = true;
            }
            SpawnCard(cardNumber);
            yield return new WaitForSeconds(_time);
        }
        yield return new WaitForSeconds(_time);
    }
    IEnumerator SpawnCardWithTimeFotTutorial(float _time)
    {
        int i = 0;

        int moveCount = 0;
        foreach (var card in spawnedCards)
        {
            if (card.GetComponent<Move>() != null) moveCount++;
        }
        if (spawnedCards.Count == 0)
        {
            i = 1;
            SpawnCard(0);
            SpawnCard(4);
            SpawnCard(9);
            SpawnCard(1);
            SpawnCard(9);
            //SpawnCard(8);

        }
        else i = 0;
        cardCount = CardLimit - spawnedCards.Count + 1;
        for (i = 1; i < cardCount; i++)
        {
            cardNumber = Random.Range(1, cardPrefabs.Count);
            SpawnCard(cardNumber);
            yield return new WaitForSeconds(_time);
        }
        yield return new WaitForSeconds(_time);
        TutorialManager.Instance.SelectCard(spawnedCards.Count);
    }
    private void SpawnCard(int _cardNumber)
    {
       // Debug.Log("SpawnCard: " + _cardNumber);
        GameObject randomPrefab = cardPrefabs[_cardNumber];
        GameObject card = Instantiate(randomPrefab, cardExit.localPosition, Quaternion.identity, spawnParent);
        GameObject cardFollow = Instantiate(cardFollowPrefab, cardExit.localPosition, Quaternion.identity, cardFollowPrefabParent);
        RectTransform cardRect = card.GetComponent<RectTransform>();
        RectTransform cardRectFollow = cardFollow.GetComponent<RectTransform>();
        cardRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransformCardLimit.GetComponent<RectTransform>().rect.width / CardLimit- spawnSpace);
    

        cardRect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cardRectFollow.rect.height);
        cardRectFollow.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, rectTransformCardLimit.GetComponent<RectTransform>().rect.width / CardLimit - spawnSpace );
        cardRectFollow.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cardRectFollow.rect.height);
     

        cardFollow.GetComponent<CardMoveImage>().spawnParent = cardFollowPrefabParent;
        Vector3 targetPos = Vector3.zero;
        card.GetComponent<CardClick>().Initialize(spawnParent, canvas, cardFollow);
        spawnedCards.Add(card);
    }

    public void ReturnAllCards()
    {
        VisualMask(false);
        foreach (var card in spawnedCards)
        {
            if (card != null) {
                card.transform.parent = cardSpawn;
                card.GetComponent<CardClick>().moveImage.CardDestroy();
                card.transform.localPosition = Vector3.zero;
                //spawnedCards.Remove(card);
            }
                
        }
    }
    public void DestroyAllCards()
    {
 
        foreach (var card in spawnedCards)
        {
            if (card != null)
            {
               
                
                Destroy(card);
            }

        }
        spawnedCards.Clear();
    }
    public void UseCurrentCard()
    {
        if (currentCardClick != null)
        {
            currentCardClick.card.UseForAllCards();
            currentCardClick = null;
            cardStartButton.gameObject.SetActive(false);
        }
    }
    public void ResetCardPositions(CardClick cardClick)
    {
        if(currentCardClick != null)
        {
            currentCardClick.GetComponent<CardClick>().ResetCardPosition();

        }
        currentCardClick = cardClick;
        if(currentCardClick.MoveCard) cardStartButton.gameObject.SetActive(false);
        else cardStartButton.gameObject.SetActive(true);
    }

    public void RemoveSpawnCard(GameObject card)
    {
        if (spawnedCards.Contains(card))
        {
            spawnedCards.Remove(card);
            if (spawnedCards.Count == 0)
            {
                GameManager.Instance.endTurnClick.EndTurnButton(1);
            }
        }
    }
    public void FadeOutCards()
    {
        foreach (var card in spawnedCards)
        {
            if (card != null)
            {
                card.GetComponent<CardClick>().moveImage.FadeOutCard();
                card.GetComponent<CardClick>().enabled = false;
            }
        }
    }
    public void FadeInCards()
    {
        foreach (var card in spawnedCards)
        {
            if (card != null)
            {
                card.GetComponent<CardClick>().moveImage.FadeInCard();
                card.GetComponent<CardClick>().enabled = true;
            }
        }
    }
    void VisualMask(bool active)
    {
       // maskCards.enabled = active;
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
