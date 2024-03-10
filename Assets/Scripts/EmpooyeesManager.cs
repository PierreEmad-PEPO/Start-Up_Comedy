using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmpooyeesManager : MonoBehaviour
{
    Timer timerManage;
    float itmerManageDuration = 30f; //for Naw
    // Start is called before the first frame update
    void Start()
    {
        timerManage = gameObject.AddComponent<Timer>();
        timerManage.Init(30, PaySalaries);
        timerManage.Run();
    }

    void PaySalaries()
    {
        List<Employee> hiedEmployees = GameManager.HiredEmployee;
        for(int index = 0; index < hiedEmployees.Count; index++) 
        {
            if (GameManager.StartUp.Budget >= hiedEmployees[index].Salary)
                GameManager.StartUp.AddMoney(-hiedEmployees[index].Salary);
            else
            {
                Debug.Log("You Losssssssss");
            }
        }
        timerManage.Run();
    }

}
