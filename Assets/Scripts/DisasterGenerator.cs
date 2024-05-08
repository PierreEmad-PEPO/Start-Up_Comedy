using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DisasterGenerator : MonoBehaviour
{
    [SerializeField] private EmpooyeesManager empooyeesManager;
    private Timer timer;
    private List<UnityAction> disastersList = new List<UnityAction>();


    private void UpdateRandomDisaster()
    {
        timer.Duration = RandomGenerator.NextFloat(1500f, 2550f);

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
        int fireLevel = RandomGenerator.NextInt(1, 6);
        if (fireLevel > GameManager.StartUp.FireSystemLevel)
        {
            GameManager.StartUp.PayMoney(5000);
            WindowManager.ShowNotificationAlert("The Company had a FIRE !!!\n" +
                "You had to pay 5000$ to fix the damages");
        }
        else
        {
            WindowManager.ShowNotificationAlert("You had a FIRE but the fire system handled it safely");
        }

        SetNewDisaster();
    }

    void EmployeeDeath()
    {

        int randomEpoyeeIndex = RandomGenerator.NextInt(0, GameManager.HiredEmployee.Count);
        Employee employee = GameManager.HiredEmployee[randomEpoyeeIndex];

        WindowManager.ShowNotificationAlert(employee.Name +" has Died");

        empooyeesManager.FireEmpoyee(employee);

        SetNewDisaster();
    }

    void CyberAttack()
    {

        int cyberAttackLevel = RandomGenerator.NextInt(1, 6);
        if (cyberAttackLevel > GameManager.StartUp.SecurityLevel)
        {
            GameManager.StartUp.PayMoney(7000);
            WindowManager.ShowNotificationAlert("The Company had a CYBERATTACK !!!\n" +
                "You had to pay 7000$ to fix the damages");
        }
        else
        {
            WindowManager.ShowNotificationAlert("You had a CYBERATTACK but your security system handled it safely");
        }

        SetNewDisaster();
    }

    void EmployeeIssue()
    {
        Debug.Log("Employee Issue");

        SetNewDisaster();
    }
}
