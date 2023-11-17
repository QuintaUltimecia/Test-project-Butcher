using UnityEngine;
using UnityEngine.Events;

public class Border : MonoBehaviour
{
    [SerializeField]
    private UnityEvent _onTriggerEnter;

    public void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Runner runner))
        {
            runner.Death();
            _onTriggerEnter?.Invoke();
        }
    }
}
