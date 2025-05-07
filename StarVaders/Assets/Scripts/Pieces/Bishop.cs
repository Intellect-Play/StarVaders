using UnityEngine;
using UnityEngine.UI;

public class Bishop : BasePiece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        // Base setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Pawn Stuff
        mMovement = new Vector3Int(0, 2, 0);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Enemy1");
    }


    public override void CheckPathing()
    {
        // Horizontal
        CreateCellPath(0, -1, mMovement.y);

    }
}
