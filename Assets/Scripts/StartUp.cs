using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.Progress;

public class StartUp : MonoBehaviour
{
    #region Fields
    private Dictionary<EmployeeSpecialization, List<GameObject>> employees;
    private string companyName = "Abo Hadeda";
    private long budget = 40000;
    private int popularity = 350;
    private int rent = 500;
    private int popularitySpeedPerUnit = 100;
    private Timer popularityChangeTimer;
    private int entertainment;
    private int totalHrSkills = 50;
    private int totalMarketingSkills = 50;
    private int fireSystemLevel;
    private int securityLevel;
    private bool hasDataAnalyst;


    private VoidEventInvoker onBudgetChange;
    private VoidEventInvoker onMarketingDone;
    [SerializeField] UIDocument gameplayUI;
    [SerializeField] PlacementSystem placementSystem;
    private Label popularityLabel;
    #endregion


    #region Props
    public string CompanyName {  get { return companyName; } }
    public long Budget { get { return budget; } }
    public int MAX_POPULARITY { get { return 1001; } }
    public int Popularity { get { return popularity; } }
    public int Rent { get { return rent; } }
    public int PopularitySpeedPerUnit { get {  return popularitySpeedPerUnit; } }
    public int Entertainment { get {  return entertainment; } }
    public int TotalHrSkills { get {  return totalHrSkills; } }
    public int TotalMarketingSkills { get { return totalMarketingSkills; } }
    public int FireSystemLevel { get {  return fireSystemLevel; } }
    public int SecurityLevel { get { return securityLevel; } }
    public bool HasDataAnalyst { get {  return hasDataAnalyst; } }

    #endregion

    #region Unity Methods

    private void Start()
    {
        onBudgetChange = gameObject.AddComponent<VoidEventInvoker>();
        EventManager.AddVoidEventInvoker(EventEnum.OnBudgetChanged, onBudgetChange);

        onMarketingDone = gameObject.AddComponent<VoidEventInvoker>();
        EventManager.AddVoidEventInvoker(EventEnum.OnMarketingDone, onMarketingDone);

        popularityLabel = gameplayUI.rootVisualElement.Q<Label>("Popularity");
        UpdatePopularityLabel();
        

        popularityChangeTimer = gameObject.AddComponent<Timer>();
        popularityChangeTimer.Init(240, UpdatePopularity);
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
    public void PayMoney(long money) 
    {
        budget -= money;
        onBudgetChange.Invoke();
    }

    public void UpdateRint(int _rint)
    {
        rent += _rint;
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
    public void AddTotalHrSkills(int hrSkills)
    {
        totalHrSkills += hrSkills;
    }
    public void AddTotalMarketingSkills(int marketingSkills)
    {
        totalMarketingSkills += marketingSkills;
    }

    public void UpgradeFireSystem(int cost)
    {
        if (cost <= budget)
        {
            PayMoney(cost);
            WindowManager.ShowNotificationAlert("Fire System Level Increassed By 1");
            return;
        }
        WindowManager.ShowNotificationAlert("Not enough money");
        fireSystemLevel++;
    }
    public void UpgradeSecurityLevel(int cost)
    {
        if (cost <= budget)
        {
            PayMoney(cost);
            WindowManager.ShowNotificationAlert("Cyperscurity System Level Increassed By 1");
            return;
        }
        WindowManager.ShowNotificationAlert("Not enough money");
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
                    UpdatePopularity();
                    onMarketingDone.Invoke();
                });
        }
        else
        {
            // alert
            WindowManager.ShowNotificationAlert("No Enough Money");
        }

    }

    public bool BuyFromStore(Item item)
    {
        if (item.cost <= budget)
        {
            PayMoney(item.cost);
            placementSystem.intiobject(item.id);
            IncreaseEntertainment(1);
            return true;
        }
        WindowManager.ShowNotificationAlert("Not enough money");
        return false;
    }

    private void UpdatePopularity()
    {
        popularity += popularitySpeedPerUnit;
        if (popularity > MAX_POPULARITY) popularity = MAX_POPULARITY-1;
        UpdatePopularityLabel();
        popularitySpeedPerUnit += RandomGenerator.NextInt(-100, 20);
        if (popularitySpeedPerUnit < -50) popularitySpeedPerUnit = -50;
        popularityChangeTimer.Run();
    }

    private void UpdatePopularityLabel()
    {
        float ratio = (float)popularity / (float)MAX_POPULARITY;
        popularityLabel.text = ((int)(ratio * 100)).ToString() + "%";
        popularityLabel.style.color = Color.Lerp(Color.red, Color.black, ratio);
    }
    
    #endregion
}
