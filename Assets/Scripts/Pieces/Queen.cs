using UnityEngine;
using UnityEngine.UI;

public class Queen : BasePiece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        // Base setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Pawn Stuff
        mMovement = new Vector3Int(0, 2, 0);
        GetComponent<Image>().sprite = Resources.Load<Sprite>("Enemy5");
    }


    public override void CheckPathing()
    {
        Debug.Log(down);
        // Horizontal
        if (down)
            CreateCellPath(0, -1, mMovement.y);
        else
            CreateCellPath(0, 1, mMovement.y);
    }
}
