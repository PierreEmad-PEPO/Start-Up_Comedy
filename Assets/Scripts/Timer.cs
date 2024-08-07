using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    private float totalTime;
    private float elapsedTime = 0f;
    private bool once = true;
    private bool pause = false;
    private VoidEventInvoker onTimeFinishEvent;
    private UnityAction toDoFunction;
    
    public float Duration { get { return totalTime; } set { totalTime = value; } }
    public float CurrentTime { get { return elapsedTime; } }
    public UnityAction ToDoFunction 
    { 
        get { return toDoFunction; } 
        set 
        {
            onTimeFinishEvent.RemoveListener(toDoFunction);
            toDoFunction = value; 
            onTimeFinishEvent.AddListener(toDoFunction);
        } 
    }
    public bool IsDone { get { return (elapsedTime >= totalTime); } }


    public void Init(float totalTime, UnityAction ToDoFunction)
    {
        this.totalTime = totalTime;
        this.elapsedTime = 0f;
        this.toDoFunction = ToDoFunction;

        onTimeFinishEvent = gameObject.AddComponent<VoidEventInvoker>();
        onTimeFinishEvent.AddListener(toDoFunction);
    }

    private void Start()
    {
    }

    public void Run()
    {
        if (elapsedTime >= totalTime) elapsedTime = 0f;
        once = true;
        pause = false;
    }
    public void Stop()
    {
        elapsedTime = totalTime+1;
        once = false;
    }

    public void Pause()
    {
        pause = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (pause) return;

        if (elapsedTime < totalTime)
        {
            elapsedTime += Time.deltaTime;
        }
        else if (once)
        {
            once = false;
            onTimeFinishEvent.Invoke();
        }
    }
}
