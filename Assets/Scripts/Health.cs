using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{

    public int HealthPlayer;
    bool isDead = false;
    private void Start()
    {
        HealthPlayer = SaveManager.Instance.saveData.playerData.health;
        Debug.Log("Health: " + HealthPlayer);
    }
    public void TakeDamage(int damage)
    {
        if(isDead) return;
        HealthPlayer -= damage;
        if (HealthPlayer <= 0)
        {
            isDead = true;
            GameManager.Instance.LoseGame();
        }
    }
}
