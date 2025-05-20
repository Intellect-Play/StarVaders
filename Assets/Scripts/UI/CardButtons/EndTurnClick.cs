using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnClick : MonoBehaviour
{

    Button endTurnButton;
    private void Start()
    {
        endTurnButton = GetComponent<Button>();
        endTurnButton.onClick.AddListener(EndTurnButton);
    }
    public void EndTurnButton()
    {
        StartCoroutine(WaitForEndTurn());
    }

    IEnumerator WaitForEndTurn()
    {
        endTurnButton.interactable = false;
        CardManager.Instance.ExitTurnButton();
        GameManager.Instance.EndTurn();
        CardManager.Instance.cardManagerMove.ReturnAllCards();
        yield return new WaitForSeconds(1);
        CardManager.Instance.cardManagerMove.SpawnCards();
        yield return new WaitForSeconds(1);

        endTurnButton.interactable = true;

    }
}
