using UnityEngine;

[RequireComponent(typeof(Canvas))]
[RequireComponent(typeof(ComponentsProvider))]
public class MainCanvas : BasePanel
{
    public Canvas Canvas { get; private set; }

    [field: SerializeField]
    public ComponentsProvider ComponentsProvider { get; private set; }

    public void Initialize()
    {
        Canvas = GetComponent<Canvas>();
    }
}
