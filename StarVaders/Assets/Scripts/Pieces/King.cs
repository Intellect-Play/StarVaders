using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class King : BasePiece
{


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



}