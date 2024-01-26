using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectEventInvoker : MonoBehaviour
{
    private UnityEvent<Project> unityEvent = new UnityEvent<Project> ();

    public void AddListener(UnityAction<Project> unityAction)
    {
        unityEvent.AddListener(unityAction);
    }

    public void RemoveListener(UnityAction<Project> unityAction)
    {
        unityEvent.RemoveListener(unityAction);
    }

    public void Invoke(Project project)
    {
        unityEvent.Invoke(project);
    }
}
