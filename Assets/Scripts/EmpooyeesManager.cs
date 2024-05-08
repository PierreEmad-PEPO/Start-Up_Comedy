using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpooyeesManager : MonoBehaviour
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
        List<Employee> hiedEmployees = GameManager.HiredEmployee;
        long totalEMployeeSalary = 0;
        int rent = GameManager.StartUp.Rent;
        for(int index = 0; index < hiedEmployees.Count; index++) 
        {
            totalEMployeeSalary += hiedEmployees[index].Salary;

        }

        GameManager.StartUp.PayMoney(totalEMployeeSalary + rent);

        WindowManager.ShowNotificationAlert("You paid " + totalEMployeeSalary.ToString() + "$ salaries\n"
            + "and " + rent.ToString() + "$ rent");

        timerManage.Run();
    }

    public void FireEmpoyee(Employee employee)
    {
        employee.Fire();
        onEmployeeFired.Invoke(employee);
    }

}
