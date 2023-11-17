using System;
using UnityEngine;
using DG.Tweening;

public class Runner : BaseBehaviour
{
    public event Action<Runner> OnDeath;
    private Transform _transform;

    [SerializeField]
    private RunnerAnimationController _animations;

    [SerializeField]
    private ParticleSystem _particleDeath;

    private Rigidbody _rigidbody;

    private Tweener _moveTweener;

    public void IsMove(bool value)
    {
        _animations.Move(value);
    }

    public void Death()
    {
        _moveTweener.Kill();
        _animations.Move(false);
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(Vector3.back * 100);
        _particleDeath.Play();

        OnDeath?.Invoke(this);
    }

    public void SetPosition(Vector3 position)
    {
        _moveTweener.Kill();
        _moveTweener = _transform.DOLocalMove(position, 1f).SetEase(Ease.Linear);
    }

    public void SetParent(Transform transform)
    {
        _transform.SetParent(transform);
    }

    public Vector3 GetPosition()
    {
        return _transform.position;
    }

    protected override void Init()
    {
        _transform = transform;
        _rigidbody = GetComponent<Rigidbody>();
    }
}
