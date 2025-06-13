using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    public RectTransform StartButton;
    private void Start()
    {
        if ((SaveManager.Instance.saveData.playerData.currentLevel == 1))
        {
            StartButton.anchorMin = new Vector2(0.5f, 0);
            StartButton.anchorMax = new Vector2(0.5f, 0);
            StartButton.pivot = new Vector2(0.5f,0);
            StartButton.anchoredPosition = new Vector2(0, 175);
            gameObject.SetActive(false);
        }
    }
    public void OnClickShopButton()
    {
        GameManagerMain.Instance.OpenShop();
    }
    public void OnClickCloseShopButton()
    {
        GameManagerMain.Instance.CloseShop();
    }
}
