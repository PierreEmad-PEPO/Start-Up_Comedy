using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameObjectEventInvoker : MonoBehaviour
{
    private UnityEvent<GameObject> unityEvent = new UnityEvent<GameObject>();

    public void AddListener(UnityAction<GameObject> unityAction)
    {
        unityEvent.AddListener(unityAction);
    }

    public void RemoveListener(UnityAction<GameObject> unityAction) 
    {
        unityEvent.RemoveListener(unityAction);
    }

    public void Invoke(GameObject gameObject)
    {
        unityEvent.Invoke(gameObject);
    }
}
