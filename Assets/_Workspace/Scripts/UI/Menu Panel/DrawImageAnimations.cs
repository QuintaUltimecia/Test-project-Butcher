using UnityEngine;
using DG.Tweening;

public class DrawImageAnimations : MonoBehaviour
{
    private Transform _transform;
    private Tweener _tweenerMove;
    private Tweener _tweenerRotate;

    private void Awake()
    {
        _transform = transform;
        _transform.position = new Vector3(_transform.position.x - 100f, _transform.position.y, _transform.position.z);
    }

    private void OnEnable()
    {
        Vector3 newPos = new Vector3(_transform.position.x + 200f, _transform.position.y, _transform.position.z);

        _tweenerMove = _transform.DOMove(newPos, 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        _tweenerRotate = _transform.DORotate(new Vector3(0, 0, 15), 1f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
    }

    private void OnDisable()
    {
        _tweenerMove.Kill();
        _tweenerRotate.Kill();
    }
}
