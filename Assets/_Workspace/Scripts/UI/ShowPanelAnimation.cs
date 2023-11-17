using UnityEngine;
using DG.Tweening;

public class ShowPanelAnimation : MonoBehaviour
{
    private Transform _transform;
    private Tweener _tweener;

    private void Awake()
    {
        _transform = transform;
    }

    private void OnEnable()
    {
        if (_transform == null)
            _transform = transform;

        _transform.localScale = Vector3.zero;
        _tweener = _transform.DOScale(Vector3.one, 1f).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        if (_transform == null)
            _transform = transform;

        _tweener.Kill();
    }
}
