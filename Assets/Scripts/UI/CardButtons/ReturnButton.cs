using UnityEngine;

public class ReturnButton : MonoBehaviour
{
    public CardManagerMove cardManager;

    public void OnClickSpawn()
    {
        cardManager.SpawnCards();
    }
    public void OnClickReturn()
    {
        cardManager.ReturnAllCards();
    }
}
