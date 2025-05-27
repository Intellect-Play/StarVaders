using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class TutorialHandAnimator : MonoBehaviour
{
    public RectTransform handImage;
    public float moveDistance = 100f;
    public float duration = 1f;

    public Canvas canvas;
    private Tweener currentTween;
    public bool TweenPlay;

    [Header("Optional Directional Arrows")]
    public RectTransform fourArrow;


    void Awake()
    {
        TweenPlay = false;
        canvas = handImage.GetComponentInParent<Canvas>();
        handImage.gameObject.SetActive(false); // başlanğıcda gizli olsun
    }

    public void ShowHand(Transform target3DObject)
    {
        if (target3DObject == null)
        {
            HideHand();
            return;
        }
        TweenPlay = true;

        Vector3 screenPos = Camera.main.WorldToScreenPoint(target3DObject.position);

        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            screenPos,
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out uiPos
        );

        handImage.anchoredPosition = uiPos;
        handImage.gameObject.SetActive(true);

        currentTween?.Kill();
        Vector2 endPos = uiPos + new Vector2(0, moveDistance + 100);

        currentTween = handImage.DOAnchorPos(endPos, duration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);

        //AnimateArrows(uiPos);
    }

    public void ShowTapAnimationUI(RectTransform uiTarget,Vector3 vector3)
    {
        if (uiTarget == null)
        {
            HideHand();
            return;
        }
        TweenPlay = true;

        Vector2 uiPos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            canvas.transform as RectTransform,
            RectTransformUtility.WorldToScreenPoint(null, uiTarget.localPosition+vector3),
            canvas.renderMode == RenderMode.ScreenSpaceOverlay ? null : Camera.main,
            out uiPos
        );

        handImage.localPosition = uiTarget.localPosition + vector3;
        handImage.gameObject.SetActive(true);
        gameObject.SetActive(true);

        currentTween?.Kill();
        handImage.localScale = Vector3.one;

        currentTween = handImage
            .DOScale(1.3f, duration * 0.5f)
            .SetEase(Ease.OutSine)
            .SetLoops(-1, LoopType.Yoyo);

        //AnimateArrows(uiPos);
    }

    public void HideHandTouch()
    {
        Debug.Log("HideHandTouch");
        TweenPlay = false;
        currentTween?.Kill();
        currentTween = null;
        handImage.gameObject.SetActive(false);
        gameObject.SetActive(false);
        
    }

    public void HideHand()
    {
        Debug.Log("HideHand");
        if (!TweenPlay)
        {
            currentTween?.Kill();
            currentTween = null;
            handImage.gameObject.SetActive(false);
            gameObject.SetActive(false);
           
        }
    }
  
  
    public void StartHorizontalAnimation()
    {
        if (handImage == null) return;
        handImage.gameObject.SetActive(true);

        handImage.anchoredPosition = fourArrow.anchoredPosition; // Local pozisiyasından başlasın

        Vector2 endPos = new Vector2(moveDistance * 4, 0);

        currentTween = handImage.DOAnchorPos(endPos, duration)
            .SetRelative(true)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }
  
}
