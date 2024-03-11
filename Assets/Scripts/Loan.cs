using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loan 
{
    long money;
    float deadline;
    float minSecondForLoan = 20; // for now
    float maxSeconedForLoan = 30; // for now

    public long Money { get { return money; } }
    public float Deadline { get { return deadline; } }

    public Loan (long money) 
    {
        this.money = money;
        this.deadline = Random.Range(minSecondForLoan, maxSeconedForLoan);
    }

    public void UpdateLoan()
    {
        deadline--;
    }

}
