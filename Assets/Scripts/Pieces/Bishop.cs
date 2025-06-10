using UnityEngine;
using UnityEngine.UI;

public class Bishop : BasePiece
{
    public override void Setup(Color newTeamColor, Color32 newSpriteColor, PieceManager newPieceManager)
    {
        // Base setup
        base.Setup(newTeamColor, newSpriteColor, newPieceManager);

        // Pawn Stuff
        mMovement = new Vector3Int(2, 2, 2);
        //GetComponent<Image>().sprite = Resources.Load<Sprite>("Enemy1");
        GetComponent<Image>().sprite = enemySO._EnemyImage;

    }


    public override void CheckPathing()
    {
        // Horizontal
        if (down)
        {
            CreateCellPathForEnemy(0, -1, mMovement.y);

        }
        else
            CreateCellPathForEnemy(0, 1, mMovement.y);
    }
}
