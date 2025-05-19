using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void Shake(float duration = 0.2f, float strength = 0.3f, int vibrato = 10, float randomness = 90f)
    {
        transform.DOShakePosition(duration, strength, vibrato, randomness);
    }
}
