using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisasterGenerator : MonoBehaviour
{
    private Timer timer;
    private List<UnityAction> disastersList = new List<UnityAction>();


    private void UpdateRandomDisaster()
    {
        timer.Duration = RandomGenerator.NextFloat(1f, 10f);

        int idx = RandomGenerator.NextInt(0, disastersList.Count);
        timer.ToDoFunction = disastersList[idx];
    }

    private void SetNewDisaster()
    {
        UpdateRandomDisaster();
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
        // sending dummy data
        timer.Init(timer.Duration, disastersList[0]);
        SetNewDisaster();
    }


    void SetFire()
    {
        Debug.Log("FIRE !!!!!");

        SetNewDisaster();
    }

    void EmployeeDeath()
    {
        Debug.Log("Employee Death");

        SetNewDisaster();
    }

    void CyberAttack()
    {
        Debug.Log("Cyber Attack");

        SetNewDisaster();
    }

    void EmployeeIssue()
    {
        Debug.Log("Employee Issue");

        SetNewDisaster();
    }
}
