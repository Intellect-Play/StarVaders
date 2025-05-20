using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    [SerializeField] public CameraSO cameraSO;
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

    public void ShakeCardAttack()
    {
        transform.DOShakePosition(cameraSO.duration, cameraSO.strength, cameraSO.vibrato, cameraSO.randomness);
    }
}
