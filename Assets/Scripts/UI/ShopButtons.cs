using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    public RectTransform StartButton;
    [SerializeField] GameObject BuyImage;
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
        if(BuyImage != null)BuyImage.SetActive(CheckBuyCondition());
    }
    private void OnEnable()
    {
        if(SaveManager.Instance != null&& BuyImage != null) BuyImage.SetActive(CheckBuyCondition());
    }

    public bool CheckBuyCondition()
    {
        List<CardData> _cards = SaveManager.Instance.cardDataList.cards;
        int coin = SaveManager.Instance.saveData.playerData.coins;
        foreach (CardData card in _cards) {
            if (card.isUnlocked&&card.upgradeCost< coin)
            {
                return true;
            }else if(!card.isUnlocked && card.buyCost< coin)
            {
                return true;
            }
        }

        return false;        
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
