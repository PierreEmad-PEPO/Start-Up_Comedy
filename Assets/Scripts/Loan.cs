using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loan 
{
    long money;
    float deadline;
    //float minSecondForLoan = 15; // for now
    //float maxSeconedForLoan = 30; // for now

    public long Money { get { return money; } }
    public float Deadline { get { return deadline; } }

    public Loan (long money) 
    {
        this.money = money;
        this.deadline = money/10;
    }

    public void UpdateLoan()
    {
        deadline--;
    }

}
