using Dreamteck.Splines;
using UnityEngine;

public class Road : MonoBehaviour
{
    [field: SerializeField]
    public SplineComputer SplineComputer {  get; private set; }

    [field: SerializeField]
    public Transform Finish { get; private set; }
}
