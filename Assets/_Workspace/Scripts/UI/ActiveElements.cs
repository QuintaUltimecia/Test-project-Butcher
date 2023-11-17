using UnityEngine;

public class ActiveElements : MonoBehaviour
{
    public static ActiveElements Instance { get; private set; }

    private static Transform _transform;

    private void Awake()
    {
        Instance = this;
        _transform = transform;
    }

    public Transform GetTransform()
    {
        return _transform;
    }
}
