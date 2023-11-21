using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisasterGenerator : MonoBehaviour
{
    private Timer timer;
    private float delayTime;
    private UnityAction disaster;
    private List<UnityAction> disastersList = new List<UnityAction>();


    private UnityAction RandomDisaster()
    {
        delayTime = RandomGenerator.NextFloat(1f, 10f);

        int idx = RandomGenerator.NextInt(0, disastersList.Count);
        return disastersList[idx];
    }

    void RunTimer()
    {
        disaster = RandomDisaster();
        timer.Init(delayTime, disaster);
        timer.Run();
    }

    // Start is called before the first frame update
    void Start()
    {
        disastersList.Add(SetFire);
        disastersList.Add(EmployeeDeath);
        disastersList.Add(CyberAttack);
        disastersList.Add(EmployeeIssue);

        timer = gameObject.AddComponent<Timer>();
        RunTimer();
    }


    void SetFire()
    {
        Debug.Log("FIRE !!!!!");

        RunTimer();
    }

    void EmployeeDeath()
    {
        Debug.Log("Employee Death");

        RunTimer();
    }

    void CyberAttack()
    {
        Debug.Log("Cyber Attack");

        RunTimer();
    }

    void EmployeeIssue()
    {
        Debug.Log("Employee Issue");

        RunTimer();
    }
}
