using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    [SerializeField] public CameraSO cameraSO;
    [SerializeField] public RectTransform yourUIRectTransform;

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
        Debug.Log("ShakeCardAttack called");
        yourUIRectTransform.DOShakeAnchorPos(
    duration: 0.5f,
    strength: new Vector2(30f, 30f),
    vibrato: 10,
    randomness: 90
); 

       // transform.DOShakePosition(cameraSO.duration, cameraSO.strength, cameraSO.vibrato, cameraSO.randomness);
    }
}
