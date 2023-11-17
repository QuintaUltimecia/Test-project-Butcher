using UnityEngine;
using DG.Tweening;

public class SawAnimation : MonoBehaviour
{
    private Transform _transform;
    private Tweener _tweenerRotate;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        _tweenerRotate = _transform.DORotate(new Vector3(_transform.eulerAngles.x, _transform.eulerAngles.y, 90), 1f).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _tweenerRotate.Kill();
    }
}
