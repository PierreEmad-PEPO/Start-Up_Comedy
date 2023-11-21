using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class VoidEventInvoker : MonoBehaviour
{
    private UnityEvent unityEvent = new UnityEvent();

    public void AddListener(UnityAction unityAction)
    {
        unityEvent.AddListener(unityAction);
    }

    public void RemoveListener(UnityAction unityAction) 
    {
        unityEvent.RemoveListener(unityAction);
    }

    public void Invoke()
    {
        unityEvent.Invoke();
    }
}
