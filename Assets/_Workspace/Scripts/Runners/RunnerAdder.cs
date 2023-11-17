using UnityEngine;

public class RunnerAdder : MonoBehaviour
{
    private RunnerController _runnerController;
    private Transform _transform;

    private void Start()
    {
        _runnerController = DIContainer.GetMonoBehaviour<RunnerController>();
        _transform = transform;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Runner runner))
        {
            _runnerController.AddRunner(_transform.position);
            gameObject.SetActive(false);
        }
    }
}
