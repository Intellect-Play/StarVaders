using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;
using static Card;
using Unity.VisualScripting;

public class CardClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler,  IBeginDragHandler,  IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    private CardBase mCard;
    public CardMoveImage moveImage;

    [SerializeField] private TextMeshProUGUI mCardNameText;
    public float TriggerHeight = 150f;
    public float ReturnDuration = 0.5f;
    private float UseDistance=150;

    public Transform spawnParent;
    public CardTypeMove cardType;

    private RectTransform rectTransform;
    private Canvas canvas;
    private float startPosY;
    [HideInInspector] public bool Hovering;
    [HideInInspector] public bool CanDrag;

    [Header("Settings")]
    public CardState _CardState;
    public void Initialize(Transform transform, Canvas _canvas,GameObject cardMoveImage)
    {
        canvas = _canvas;
        spawnParent = transform;
        moveImage = cardMoveImage.GetComponent<CardMoveImage>();
        moveImage.SetTarget(this.gameObject);
        mCard.mCardMoveImage = moveImage;
    }
    
    public enum CardState
    {
        Idle, IsDragging, Played
    }
    void OnEnable()
    {
        CanDrag = true;

        rectTransform = GetComponent<RectTransform>();
        mCard = GetComponent<CardBase>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosY = transform.position.y;
        Debug.Log(transform.position);
        transform.parent = canvas.transform;
        moveImage.transform.parent = canvas.transform;

        mCard.ClickGiveManagerSelectedCard();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (transform.position.y - startPosY > UseDistance)
        {
            mCard.UseForAllCards();
            return;
        }
        transform.parent = spawnParent;
        moveImage.ReturnParent();
        transform.position = Vector3.zero;

       
        mCard.ExitCard();
    }

   public void OnDrag(PointerEventData eventData)
    {
        if (!CanDrag)
            return;
        if (canvas == null) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
   }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!CanDrag)
            return;
        _CardState = CardState.IsDragging;

    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        _CardState = CardState.Idle;

    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        Hovering = true || _CardState == CardState.IsDragging;

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        Hovering = false;

    }

  
}
