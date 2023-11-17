using UnityEngine;

public class RunnerAnimationController : MonoBehaviour
{
    private Animator _animator;

    private int MOVE = Animator.StringToHash("isMove");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    public void Move(bool value)
    {
        _animator.SetBool(MOVE, value);
    }
}
