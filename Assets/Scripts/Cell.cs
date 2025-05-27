using UnityEngine;
using UnityEngine.UI;

public class Cell : MonoBehaviour
{
    public Image mOutlineImage;
    public Image mOutlineEnemyImage;
    public Image mOutlineMoveImage;
    //[HideInInspector]
    public Vector2Int mBoardPosition = Vector2Int.zero;
    //[HideInInspector]
    public Board mBoard = null;
    //[HideInInspector]
    public RectTransform mRectTransform = null;

    //[HideInInspector]
    public BasePiece mCurrentPiece = null;

    [SerializeField] private Color mMoveColor;
    Button cellButton;

    private void OnEnable()
    {
        cellButton = GetComponent<Button>();
        //cellButton.onClick.AddListener(CellButtonClick);
        MoveActive(false);
    }

    public void CellButtonClick()
    {
        Debug.Log("Cell Button Clicked");
        PieceManager.Instance.mWhitePiece.MoveKing(this);
    }
    public void Setup(Vector2Int newBoardPosition, Board newBoard)
    {
        mBoardPosition = newBoardPosition;
        mBoard = newBoard;

        mRectTransform = GetComponent<RectTransform>();
    }

    public void MoveActive(bool active)
    {
        mOutlineImage.color = active ? mMoveColor : Color.black;
        mOutlineImage.enabled = active;
        cellButton.interactable = active;
    }

    public void RemovePiece()
    {
        if (mCurrentPiece != null)
        {
            mCurrentPiece.Kill();
        }
    }
}
