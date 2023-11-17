using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public static class DIContainer
{
    private static List<MonoBehaviour> _monoBehaviours = new List<MonoBehaviour>();

    public static void AddMonoBehaviour(MonoBehaviour monoBehaviour)
    {
        _monoBehaviours.Add(monoBehaviour);
    }

    public static T GetMonoBehaviour<T>() where T : MonoBehaviour
    {
        return (T)_monoBehaviours.FirstOrDefault(s => s is T);
    }

    public static Component GetInterface(System.Type type)
    {
        for (int i = 0; i < _monoBehaviours.Count; i++)
        {
            MonoBehaviour mono = _monoBehaviours[i];

            if (mono.TryGetComponent(type, out Component component))
            {
                return component;
            }
        }

        return null;
    }
}
