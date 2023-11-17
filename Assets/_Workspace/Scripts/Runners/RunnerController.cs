using Dreamteck.Splines;
using System;
using System.Collections.Generic;
using UnityEngine;

public class RunnerController : BaseBehaviour, ICameraTarget
{
    public event Action OnEmptyRunners;

    public Transform Transform { get; private set; }
    public IEnumerable<Runner> Runners { get => _runners; }

    [SerializeField]
    private Runner _runnerPrefab;

    private SplineFollower _splineFolloer;

    private List<Runner> _runners;

    private readonly int _maxRunners = 30;
    private readonly int _startRunners = 20;
    private readonly float _startPosition = -3f;
    private readonly float _offsetPosition = 1.5f;

    private Vector3 _startPositionController;
    private Transform _finishTransform;

    public void Enable()
    {
        _splineFolloer.Restart();
        _splineFolloer.enabled = true;

        foreach (var runner in Runners) 
            runner.IsMove(true);
    }

    public void Disable()
    {
        foreach (var runner in Runners)
            runner.IsMove(false);

        _splineFolloer.enabled = false;
    }

    public void StartRunners()
    {
        if (_runners.Count != 0)
        {
            foreach (var item in _runners)
                Destroy(item.gameObject);

            _runners.Clear();
        }

        Transform.position = _startPositionController;

        int Y = 4;
        int X = _startRunners / Y;

        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                Runner newRunner = Instantiate(_runnerPrefab, Transform);
                newRunner.Initialize();

                newRunner.SetPosition(new Vector3(
                    x: _startPosition + (_offsetPosition * x),
                    y: 0,
                z: _startPosition + (_offsetPosition * y)));

                newRunner.OnDeath += (value) => RemoveRunner(value);

                _runners.Add(newRunner);
            }
        }
    }

    public void SetFinishPositions()
    {
        int Y = 5;
        int X = 6;

        int i = 0;

        for (int x = 0; x < X; x++)
        {
            for (int y = 0; y < Y; y++)
            {
                if (i >= _runners.Count)
                    break;

                _runners[i].SetPosition(new Vector3(
                    x: _startPosition + (_offsetPosition * x),
                    y: 0,
                    z: _startPosition + (_offsetPosition * y)));

                i++;
            }
        }
    }

    public void AddRunner(Vector3 position)
    {
        if (_runners.Count >= _maxRunners)
            return;

        Runner newRunner = Instantiate(_runnerPrefab, Transform);
        newRunner.Initialize();
        newRunner.transform.position = position;
        newRunner.OnDeath += (value) => RemoveRunner(value);
        newRunner.IsMove(true);

        _runners.Add(newRunner);
    }

    public Transform GetTransform() => Transform;

    private void RemoveRunner(Runner runner)
    {
        runner.SetParent(null);
        _runners.Remove(runner);

        if (_runners.Count == 0)
            OnEmptyRunners?.Invoke();
    }

    protected override void Init()
    {
        Transform = transform;
        _runners = new List<Runner>();
        _splineFolloer = GetComponent<SplineFollower>();
        _splineFolloer.spline = DIContainer.GetMonoBehaviour<SplineComputer>();
        _finishTransform = DIContainer.GetMonoBehaviour<Road>().Finish;
        Debug.Log(_finishTransform.position);

        _splineFolloer.enabled = false;
        _splineFolloer.onEndReached += (value) => 
        {
            Transform.position = _finishTransform.position;
            SetFinishPositions();
            OnEmptyRunners?.Invoke(); 
        };

        _startPositionController = Transform.position;
    }
}
