using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartUp : MonoBehaviour
{
    #region Fields
    private Dictionary<EmployeeSpecialization, List<GameObject>> employees;
    private long budget = 1000;
    private int popularity = 200;
    private int popularitySpeedPerUnit;
    private Timer popularityChangeTimer;
    private int entertainment;
    private int totalHrSkills;
    private int fireSystemLevel;
    private int securityLevel;
    private bool hasDataAnalyst;

    private VoidEventInvoker onBudgetChange;
    [SerializeField] UIDocument gameplayUI;
    private Label popularityLabel;
    #endregion


    #region Props
    public long Budget { get { return budget; } }
    public int Popularity { get { return popularity; } }
    public int PopularitySpeedPerUnit { get {  return popularitySpeedPerUnit; } }
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

        popularityLabel = gameplayUI.rootVisualElement.Q<Label>("Popularity");

        popularityChangeTimer = gameObject.AddComponent<Timer>();
        popularityChangeTimer.Init(1, UpdatePopularity);
        popularityChangeTimer.Run();
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
    public void SetPopularitySpeed(int newSpeed)
    {
        popularitySpeedPerUnit = newSpeed;
    }
    public void IncreasePopularitySpeed(int newSpeed)
    {
        popularitySpeedPerUnit += newSpeed;
    }
    public void DecreasePopularitySpeed(int newSpeed)
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
        int effect = (int)GameManager.GetMarketingEffect(name);
        if (price <= budget)
        {
            WindowManager.ShowConfirmationAlert("Are you sure ?!!",
                () =>
                { 
                    PayMoney(price);
                    IncreasePopularitySpeed(effect);
                });
        }
        else
        {
            // alert
            WindowManager.ShowNotificationAlert("No Enough Money");
        }

    }

    private void UpdatePopularity()
    {
        popularity += popularitySpeedPerUnit;
        popularityLabel.text = popularity.ToString();
        popularitySpeedPerUnit += RandomGenerator.NextInt(-5, 3);
        if (popularitySpeedPerUnit < -5) popularitySpeedPerUnit = -5;
        popularityChangeTimer.Run();
    }
    
    #endregion
}
