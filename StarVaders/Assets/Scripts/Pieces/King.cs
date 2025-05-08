using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class King : BasePiece
{
    public bool isDragging = false;


    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        // Base setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // King stuff
        mMovement = new Vector3Int(2, 2, 1);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Player");
    }

    public override void Kill()
    {
        base.Kill();

        mPieceManager.mIsKingAlive = false;
    }

    #region Cross-Platform Input
    void Update()
    {
        if(!moveCard) return;
        Vector2 pointerPosition = Vector2.zero;
        bool begin = false, move = false, end = false;

#if UNITY_EDITOR || UNITY_STANDALONE || UNITY_WEBGL
        if (Input.GetMouseButtonDown(0))
        {
            pointerPosition = Input.mousePosition;
            begin = true;
        }
        else if (Input.GetMouseButton(0))
        {
            pointerPosition = Input.mousePosition;
            move = true;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            pointerPosition = Input.mousePosition;
            end = true;
        }
#else
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            pointerPosition = touch.position;
            begin = touch.phase == TouchPhase.Began;
            move = touch.phase == TouchPhase.Moved;
            end = touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled;
        }
#endif

        if (begin)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(mRectTransform, pointerPosition))
            {
                CheckPathing();
                ShowCells();
                isDragging = true;
            }
        }

        if (move && isDragging)
        {
            Vector3 worldPos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(mRectTransform, pointerPosition, null, out worldPos);
            transform.position = worldPos;

            mTargetCell = null;
            foreach (Cell cell in mHighlightedCells)
            {
                if (RectTransformUtility.RectangleContainsScreenPoint(cell.mRectTransform, pointerPosition))
                {
                    mTargetCell = cell;
                    break;
                }
            }
        }

        if (end && isDragging)
        {
            ClearCells();
            isDragging = false;

            if (mTargetCell != null)
            {
                Move();
               // mPieceManager.SwitchSides(mColor);
                moveCard = false;
                if (mColor == Color.white)
                {
                }
            }
            else
            {
                transform.position = mCurrentCell.transform.position;
            }
        }
    }
    #endregion

}