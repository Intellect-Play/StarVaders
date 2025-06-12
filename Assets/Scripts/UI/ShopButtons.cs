using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButtons : MonoBehaviour
{
    public void OnClickShopButton()
    {
        GameManagerMain.Instance.OpenShop();
    }
    public void OnClickCloseShopButton()
    {
        GameManagerMain.Instance.CloseShop();
    }
}
