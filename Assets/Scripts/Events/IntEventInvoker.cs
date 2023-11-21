using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class IntEventInvoker : MonoBehaviour
{
    private UnityEvent<int> unityEvent = new UnityEvent<int>();

    public void AddListener(UnityAction<int> unityAction)
    {
        unityEvent.AddListener(unityAction);
    }

    public void RemoveListener(UnityAction<int> unityAction)
    {
        unityEvent.RemoveListener(unityAction);
    }

    public void Invoke(int x)
    {
        unityEvent.Invoke(x);
    }
}
