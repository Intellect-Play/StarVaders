using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardButtonUI : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI powerText;
    public Button actionButton;
    public TextMeshProUGUI actionButtonText;

    [HideInInspector] public int cardId;
}
