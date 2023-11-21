using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private float totalTime;
    private float elapsedTime = 0f;
    private UnityEvent unityEvent;
    
    public float Duration { get { return totalTime; } set { totalTime = value; } }
    public bool IsDone { get { return (elapsedTime >= totalTime); } }


    public void Init(float totalTime, UnityAction ToDoFunction)
    {
        this.totalTime = totalTime;
        this.elapsedTime = 0f;
        this.unityEvent = new UnityEvent();
        this.unityEvent.AddListener(ToDoFunction);
    }

    public void Run()
    {
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
        }
        else
        {
            unityEvent.Invoke();
        }
    }
}
