using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StartUp : MonoBehaviour
{
    #region Fields
    private Dictionary<EmployeeSpecialization, List<GameObject>> employees;
    private long budget = 1000;
    private int popularity;
    private float popularitySpeedPerUnit;
    private Timer popularityChangeTimer;
    private int entertainment;
    private int totalHrSkills;
    private int fireSystemLevel;
    private int securityLevel;
    private bool hasDataAnalyst;

    private VoidEventInvoker onBudgetChange;
    #endregion


    #region Props
    public long Budget { get { return budget; } }
    public int Popularity { get { return popularity; } }
    public float PopularitySpeedPerUnit { get {  return popularitySpeedPerUnit; } }
    public int Entertainment { get {  return entertainment; } }
    public int TotalHrSkills { get {  return totalHrSkills; } }
    public int FireSystemLevel { get {  return fireSystemLevel; } }
    public int SecurityLevel { get { return securityLevel; } }
    public bool HasDataAnalyst { get {  return hasDataAnalyst; } }
    #endregion

    #region Unity Methods

    private void Start()
    {
        onBudgetChange = gameObject.AddComponent<VoidEventInvoker>();
        EventManager.AddVoidEventInvoker(EventEnum.OnBudgetChanged, onBudgetChange);
    }

    #endregion

    #region Methods
    public List<GameObject> GetEmployees(EmployeeSpecialization specialization)
    {
        return employees[specialization];
    }
    public void AddMoney(int  money)
    {
        budget += money;
        onBudgetChange.Invoke();
    }
    public void PayMoney(int money) 
    {
        budget -= money;
        onBudgetChange.Invoke();
    }
    public void SetPopularitySpeed(float newSpeed)
    {
        popularitySpeedPerUnit = newSpeed;
    }
    public void IncreasePopularitySpeed(float newSpeed)
    {
        popularitySpeedPerUnit += newSpeed;
    }
    public void DecreasePopularitySpeed(float newSpeed)
    {
        popularitySpeedPerUnit -= newSpeed;
    }
    public void IncreaseEntertainment(int _entertainment)
    {
        entertainment += _entertainment;
    }
    public void DecreaseEntertainment(int _entertainment)
    {
        entertainment -= _entertainment;
    }
    public void IncreaseTotalHrSkills(int hrSkills)
    {
        totalHrSkills += hrSkills;
    }
    public void DecreaseTotalHrSkills(int hrSkills)
    {
        totalHrSkills -= hrSkills;
    }
    public void UpgradeFireSystem()
    {
        fireSystemLevel++;
    }
    public void UpgradeSecurityLevel()
    {
        securityLevel++;
    }
    public void SetHasDataAnalyst(bool _hasDataAnalyst)
    {
        hasDataAnalyst = _hasDataAnalyst;
    }

    public void DoAD(MarketingEnum name)
    {
        int price = GameManager.GetMarketingPrice(name);
        float effect = GameManager.GetMarketingEffect(name);
        if (price <= budget)
        {
            PayMoney(price);
            IncreasePopularitySpeed(effect);
            //alert
            Debug.Log("Done");
        }
        else
        {
            // alert
            Debug.Log("Budget Not enough");
        }

    }
    
    #endregion
}
