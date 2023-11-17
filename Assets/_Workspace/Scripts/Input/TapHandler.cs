using System;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapHandler : BaseBehaviour, IDragHandler, IEndDragHandler
{
    public event Action OnDraw;

    [SerializeField]
    private float _tick = 10f;

    [SerializeField]
    private RectTransform _tapPoint;

    [SerializeField]
    private UILineRenderer _lineRenderer;

    private RectTransform _rectTransform;

    private RunnerController _runnerController;

    private Camera _camera;
    private Ray _ray;

    public void OnDrag(PointerEventData eventData)
    {
        _tapPoint.position = eventData.position;
        _tapPoint.anchoredPosition = new Vector2(
            x: Mathf.Max(Mathf.Min(_tapPoint.anchoredPosition.x, 200), -200),
            y: Mathf.Max(Mathf.Min(_tapPoint.anchoredPosition.y, 200), -200));

        _ray.origin = CalculatePosition(_tapPoint.anchoredPosition);
        _ray.direction = -_runnerController.Transform.up;

        if (Physics.Raycast(_ray, 100f))
        {

        }

        if (_lineRenderer.GetDistanceLast(_tapPoint.anchoredPosition) > _tick)
            _lineRenderer.AddPoint(_tapPoint.anchoredPosition);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        int increament;
        int count = 0;

        if (_lineRenderer.Point.Count() > _runnerController.Runners.Count())
        {
            increament = Mathf.RoundToInt((float)_lineRenderer.Point.Count() / (float)_runnerController.Runners.Count());
        }
        else
        {
            increament = 1;
        }

        for (int i = _runnerController.Runners.Count()-1; i >= 0; i--)
        {
            Runner runner = _runnerController.Runners.ElementAt(i);

            try
            {
                runner.SetPosition(CalculatePosition(_lineRenderer.Point.ElementAt(count)));
            }
            catch
            {
                runner.SetPosition(CalculatePosition(_lineRenderer.Point.ElementAt(_lineRenderer.Point.Count()-1)));
            }

            count += increament;

            if (count > _lineRenderer.Point.Count() - 1)
                count--;
        }

        _lineRenderer.ClearPoints();

        if (GameState.CurrentState.GetType() == typeof(MenuState))
            OnDraw.Invoke();
    }

    public Vector3 CalculatePosition(Vector3 position)
    {
        return new Vector3(position.x / 40, _runnerController.Transform.position.y, position.y / 40);
    }

    public void OnDrawGizmos()
    {
        if (_camera == null)
            return;

        Gizmos.color = Color.green;

        Gizmos.DrawRay(_ray.origin, _ray.direction * 100);
    }

    protected override void Init()
    {
        _camera = Camera.main;
        _rectTransform = GetComponent<RectTransform>();
        _runnerController = DIContainer.GetMonoBehaviour<RunnerController>();
    }
}
