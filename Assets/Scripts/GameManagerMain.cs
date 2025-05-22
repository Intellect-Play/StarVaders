
using UnityEngine;

public class GameManagerMain : MonoBehaviour
{
    public static GameManagerMain Instance { get; private set; }

    [SerializeField]private GameObject mMainStage;
    [SerializeField]private GameObject mGameUI;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        //BattleStartInMainMenu(false);
    }

    public void BattleStartInMainMenu(bool start)
    {
        mMainStage.SetActive(!start);
        mGameUI.SetActive(start);
    }
}

