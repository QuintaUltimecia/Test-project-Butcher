using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ComponentsProvider : MonoBehaviour
{
    [SerializeField]
    private List<MonoBehaviour> _components = new List<MonoBehaviour>();

    public T Get<T>() where T : MonoBehaviour
    {
        return (T)_components.FirstOrDefault(s => s is T);
    }

    public List<T> GetAll<T>() where T : MonoBehaviour
    {
        List<T> list = new List<T>();

        for (int i = 0; i < _components.Count; i++)
            if (_components[i].TryGetComponent(out T component))
                list.Add(component);

        return list;
    }
}
