using UnityEngine;
using UnityEngine.EventSystems;
using TMPro;
using DG.Tweening;

public class CardClick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler,  IBeginDragHandler,  IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private CardBase mCard;
    [SerializeField] private TextMeshProUGUI mCardNameText;
    public float TriggerHeight = 150f;
    public float ReturnDuration = 0.5f;
    private float UseDistance=150;
    private bool isDragging = false;

    public Transform spawnParent;

    private RectTransform rectTransform;
    private Canvas canvas;
    private float startPosY;
    public void Initialize(Transform transform, Canvas _canvas)
    {
        canvas = _canvas;
        spawnParent = transform;
        Debug.Log("CardClick Initialized");
    }

    void OnEnable()
    {
        rectTransform = GetComponent<RectTransform>();
        mCard = GetComponent<CardBase>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        startPosY = transform.position.y;
        Debug.Log(transform.position);
        transform.parent = canvas.transform;

        isDragging = true;

        mCard.ClickGiveManagerSelectedCard();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (transform.position.y - startPosY > UseDistance)
        {
            Debug.Log("Card Used "+ transform.position.y + "  "+startPosY);
            mCard.UseForAllCards();
            return;
        }
        transform.parent = spawnParent;
        transform.position = Vector3.zero;
        isDragging = false;

       
        mCard.ExitCard();
    }

   public void OnDrag(PointerEventData eventData)
   {
    if (canvas == null) return;

        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
   }

    public void OnBeginDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IEndDragHandler.OnEndDrag(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
    {
        throw new System.NotImplementedException();
    }
}
public enum CardState
{
    Idle, IsDragging, Played
}