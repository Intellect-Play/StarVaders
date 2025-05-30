using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance { get; private set; }


    [Header("Objects")]
    public GameObject tutorialCanvas;
    public GameObject tutorialHand;
    public TutorialHandAnimator tutorialHandAnimator;

    [Header("Tutorial Settings")]
    [SerializeField] private Vector3 ButtonClickOffset = new Vector3(280,70,0); 

    public bool IsTutorialActive = false;
    int tutorialLevel;
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

        if(PlayerPrefs.GetInt("Tutorial", 0) == 0)
        {
            IsTutorialActive = true;
            tutorialLevel = 0;
        }
        else
        {
            HideTutorialHand();
            IsTutorialActive = false;
            
        }
    }
    void Start()
    {
        
    }
    public void SelectCard(int currentCardCount)
    {
        if (!IsTutorialActive) return;
        tutorialLevel++;
        for (int i = 0;i<CardManagerMove.Instance.spawnedCards.Count; i++)
        {
            CardClick card = CardManagerMove.Instance.spawnedCards[i].GetComponent<CardClick>();
            if (i == 0)
            {
                if(tutorialLevel<3)
                   tutorialHandAnimator.ShowTapAnimationUI(card.gameObject.GetComponent<RectTransform>(), new Vector3(50,0,0));
                else tutorialHandAnimator.ShowMoveHandAnimationUI(card.gameObject.GetComponent<RectTransform>(), new Vector3(50, 0, 0));

            }
            else
            {
                if (card != null)
                {
                    card.moveImage.FadeOutCard();
                    card.enabled = false;
                }
            }
            
        }
        
    }
    public void TutorialCardSelected(CardClick cardClick)
    {
        if (!IsTutorialActive) return;
        if (cardClick != null&& tutorialLevel < 3)
        {
            if (tutorialLevel==1)
                tutorialHandAnimator.ShowTapAnimationWorldUI(GameManager.Instance.mBoard.mAllCells[2,3].GetComponent<RectTransform>(),new Vector3(3,-3,0));
            else tutorialHandAnimator.ShowTapAnimationWorldUI(GameManager.Instance.mBoard.mAllCells[2, 3].GetComponent<RectTransform>(), Vector3.zero);

        }
    }
    public void CardStartClickTutorial()
    {
        if (tutorialLevel >= 1)
        {
            HideTutorialHand();
        }
    }
 

    public void TutorialHandClickButton(RectTransform rectTransform)
    {
        if (!IsTutorialActive) return;
        tutorialHandAnimator.ShowTapAnimationUI(rectTransform, ButtonClickOffset);
    }
    public void HideTutorialMoveHand()
    {
        if (!IsTutorialActive || tutorialLevel < 3) return;
        tutorialHandAnimator.HideHandTouch();
    }
    public void HideTutorialHand()
    {
       
        if (!IsTutorialActive) return;
        tutorialHandAnimator.HideHandTouch();
    }

    public void CardMoveTutorialinGame()
    {
        if ((CardManagerMove.Instance.currentCardClick != null && !CardManagerMove.Instance.currentCardClick.MoveCard)|| CardManagerMove.Instance.currentCardClick == null)
        {
            if (CardManagerMove.Instance.spawnedCards[0] != null)
            {
                tutorialHandAnimator.ShowMoveHandAnimationUI(CardManagerMove.Instance.spawnedCards[0].GetComponent<RectTransform>(), new Vector3(50, 0, 0));

            }

        }
    }

    public void MoveCardTutorialinGame()
    {
        if (CardManagerMove.Instance.currentCardClick != null && CardManagerMove.Instance.currentCardClick.MoveCard)
        {
            tutorialHandAnimator.ShowTapAnimationWorldUI(GameManager.Instance.mBoard.mAllCells[2, 3].GetComponent<RectTransform>(), new Vector3(3, -3, 0));

        }
    }

    public void EndGameTutorial()
    {
        tutorialHandAnimator.HideHandTouch();
    }
}
