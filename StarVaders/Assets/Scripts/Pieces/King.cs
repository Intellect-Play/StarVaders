using UnityEngine;
using UnityEngine.UI;

public class King : BasePiece
{
    private Rook mLeftRook = null;
    private Rook mRightRook = null;

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

        //mPieceManager.mIsKingAlive = false;
    }



}