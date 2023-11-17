using UnityEngine;

public class CameraMovement : BaseBehaviour
{
    public bool IsMoveToTarget { get; set; } = true;

    [SerializeField]
    private float _offsetZ = 9f;

    [SerializeField]
    private float _offsetY = 4f;

    [SerializeField]
    private float _offsetX = 4f;

    [SerializeField]
    private float _moveFade = 10f;

    private Transform _target;

    private Transform _transform;

    public void SetPosition(Vector3 position)
    {
        _transform.position = position;
    }

    public void SetRotation(Quaternion quaternion)
    {
        _transform.rotation = quaternion;
    }

    protected override void OnLateTick()
    {
        if (_target != null && IsMoveToTarget == true)
        {
            Move(_target.position);
            //Rotation();
        }
    }

    protected override void Init()
    {
        ICameraTarget target = (ICameraTarget)DIContainer.GetInterface(typeof(ICameraTarget));
        _target = target.GetTransform();
        _transform = transform;
    }

    private void Move(Vector3 position)
    {
        Vector3 direction = new Vector3(position.x + _offsetX, position.y + _offsetY, position.z - _offsetZ);
        Vector3 directionFade = Vector3.Lerp(_transform.position, direction, _moveFade * Time.deltaTime);

        _transform.position = directionFade;
    }

    private void Rotation()
    {
        _transform.LookAt(_target.position);
        _transform.rotation = Quaternion.Euler(_transform.eulerAngles.x, 0, 0);
    }

    private void OnEnable()
    {
        _lateUpdates.Add(this);
    }

    private void OnDisable()
    {
        _lateUpdates.Remove(this);
    }
}
