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

        if(PlayerPrefs.GetInt("Tutorial", 0) == 1)
        {

        }
        else
        {

        }
    }
    void Start()
    {
        
    }

    public void ShowTutorialHand(Transform target3DObject)
    {
        tutorialHandAnimator.ShowHand(target3DObject);
    }   

    public void TutorialHandClickButton(RectTransform rectTransform)
    {
        tutorialHandAnimator.ShowTapAnimationUI(rectTransform, ButtonClickOffset);
    }

    public void HideTutorialHand()
    {
        tutorialHandAnimator.HideHandTouch();
    }
}
