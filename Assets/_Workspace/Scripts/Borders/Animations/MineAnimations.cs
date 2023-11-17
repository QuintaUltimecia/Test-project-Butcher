using UnityEngine;

public class MineAnimations : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem _particleSystem;

    private MeshRenderer _meshRenderer;
    private Collider _collider;

    private void Awake()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        _collider = GetComponent<Collider>();
    }

    public void Enable()
    {
        _meshRenderer.enabled = true;
        _collider.enabled = true;
    }

    public void Disable()
    {
        _meshRenderer.enabled = false;
        _collider.enabled = false;
    }

    public void Play()
    {
        _particleSystem.Play();
        Disable();
    }
}
