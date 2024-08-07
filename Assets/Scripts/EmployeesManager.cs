using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeesManager : MonoBehaviour
{
    Timer timerManage;
    float timerManageDuration = 600f; //for Naw

    EmployeeEventInvoker onEmployeeFired;

    // Start is called before the first frame update
    void Start()
    {
        timerManage = gameObject.AddComponent<Timer>();
        timerManage.Init(timerManageDuration, PaySalaries);
        timerManage.Run();

        onEmployeeFired = gameObject.AddComponent<EmployeeEventInvoker>();
        EventManager.AddEmployeeEventInvoker(EventEnum.OnEmployeeFired, onEmployeeFired);
    }

    void PaySalaries()
    {
        List<Employee> hiredEmployees = GameManager.HiredEmployee;
        long totalEmployeesSalary = 0;
        int rent = GameManager.StartUp.Rent;
        for(int index = 0; index < hiredEmployees.Count; index++) 
        {
            totalEmployeesSalary += hiredEmployees[index].Salary;

        }

        GameManager.StartUp.PayMoney(totalEmployeesSalary + rent);

        WindowManager.ShowNotificationAlert("You paid " + totalEmployeesSalary.ToString() + "$ salaries\n"
            + "and " + rent.ToString() + "$ rent");

        timerManage.Run();
    }

    public void FireEmployee(Employee employee)
    {
        employee.Fire();
        onEmployeeFired.Invoke(employee);
    }

}
