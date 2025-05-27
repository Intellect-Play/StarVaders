using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class BattleButton : MonoBehaviour
{
    Button endTurnButton;
    [Header("Fade Panel")]
    public CanvasGroup fadeCanvasGroup;
    public float fadeDuration = 0.5f;
    private void Start()
    {
        endTurnButton = GetComponent<Button>();
        endTurnButton.onClick.AddListener(StartBattle);
        fadeCanvasGroup.alpha = 0;
    }

    void StartBattle() { 
    
        StartCoroutine(FadeTime());

    }

    IEnumerator FadeTime()
    {
        fadeCanvasGroup.alpha = 0;
        // Fade In
        yield return fadeCanvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.InOutQuad).WaitForCompletion();
        GameManagerMain.Instance.BattleStartInMainMenu(true);
    }
}
