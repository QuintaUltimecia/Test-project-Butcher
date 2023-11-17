using UnityEngine;

[RequireComponent(typeof(CameraMovement))]
public class MainCamera : BaseBehaviour
{
    public CameraMovement CameraMovement { get; private set; }
    public Camera Camera { get; private set; }

    protected override void Init()
    {
        CameraMovement = GetComponent<CameraMovement>();
        Camera = GetComponent<Camera>();

        CameraMovement.Initialize();
    }
}
