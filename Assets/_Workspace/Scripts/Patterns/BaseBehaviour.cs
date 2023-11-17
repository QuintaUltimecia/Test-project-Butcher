using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour
{
    public static List<BaseBehaviour> _updates = new List<BaseBehaviour>(10001);
    public static List<BaseBehaviour> _lateUpdates = new List<BaseBehaviour>(10001);
    public static List<BaseBehaviour> _fixedUpdates = new List<BaseBehaviour>(10001);

    protected bool _isInitialized = false;

    public void Tick() => OnTick();
    protected virtual void OnTick() { }

    public void LateTick() => OnLateTick();
    protected virtual void OnLateTick() { }

    public void FixedTick() => OnFixedTick();
    protected virtual void OnFixedTick() { }

    public void Initialize()
    {
        if (_isInitialized == true)
        {
            Debug.LogWarning($"{this} is already initialized.");
            return;
        }

        _isInitialized = true;
        Init();
    }

    protected abstract void Init();

    protected void Start()
    {
        if (_isInitialized == false)
            Debug.LogWarning($"{this} is not initialized.");
    }
}