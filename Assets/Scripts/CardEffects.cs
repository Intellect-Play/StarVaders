using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardEffects : MonoBehaviour
{
    public static CardEffects Instance { get; private set; }
    [Header("Tornado")]
    [SerializeField] private List<GameObject> TornadoEffects;

    [Header("BombEffect")]
    [SerializeField] private GameObject BombEffectParticle;
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
    }


    public void TornadoEffect()
    {
        StartCoroutine(TornadoEffectTime());
    }
    IEnumerator TornadoEffectTime()
    {
        foreach (GameObject tornado in TornadoEffects)
        {
            tornado.SetActive(true);
        }
        yield return new WaitForSeconds(.6f);

        EnemySpawner.Instance.EnemyBackMoveF();
        yield return new WaitForSeconds(.8f);

        foreach (GameObject tornado in TornadoEffects)
        {
            tornado.SetActive(false);
        }
    }
    public void BombEffect(List<Cell> bombPositions)
    {
        foreach (var cell in bombPositions)
        {
            GameObject bombEffect = Instantiate(BombEffectParticle, Vector3.zero, Quaternion.identity, cell.transform);
            bombEffect.transform.localPosition = Vector3.zero;
            Destroy(bombEffect, .4f);
        }
    }
}
