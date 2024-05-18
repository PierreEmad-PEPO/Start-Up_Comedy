using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class StartUp : MonoBehaviour
{
    #region Fields

    [SerializeField] GameObject[] grounds;
    [SerializeField] GameObject ground;

    private Dictionary<EmployeeSpecialization, List<GameObject>> employees;
    private string companyName = "";
    private long budget = 4000000;
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
    public string CompanyName {  get { return companyName; } set { companyName = value; } }
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
        popularityChangeTimer.Init(250, UpdatePopularity);
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
        GameOverCheack();
    }
    public void PayMoney(long money) 
    {
        budget -= money;
        onBudgetChange.Invoke();
        GameOverCheack();
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
        if (totalHrSkills > 500) totalHrSkills = 500;
    }
    public void AddTotalMarketingSkills(int marketingSkills)
    {
        totalMarketingSkills += marketingSkills;
        if (totalMarketingSkills > 500) totalMarketingSkills = 500;
    }

    public void UpgradeFireSystem(int cost)
    {
        if (cost <= budget)
        {
            PayMoney(cost);
            WindowManager.ShowNotificationAlert("Fire System Level Increassed By 1");
            fireSystemLevel++;
            return;
        }
        WindowManager.ShowNotificationAlert("Not enough money");
    }
    public void UpgradeSecurityLevel(int cost)
    {
        if (cost <= budget)
        {
            PayMoney(cost);
            WindowManager.ShowNotificationAlert("Cyperscurity System Level Increassed By 1");
            securityLevel++;
            return;
        }
        WindowManager.ShowNotificationAlert("Not enough money");
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
            IncreaseEntertainment(2);
            return true;
        }
        WindowManager.ShowNotificationAlert("Not enough money");
        return false;
    }

    public void BuyGround(int price)
    {
        if (price <= budget)
        {
            int groundCount = GameManager.GroundCount;
            Vector3 position = Vector3.zero;
            position.z = -(int)(groundCount / 3);
            position.x = -(int)(groundCount % 3 );
            if (position.z == 0)
            {
                position.x *= 10;
                position.z *= 10;
                GameObject plane = Instantiate(grounds[0],position, Quaternion.identity);
                plane.transform.parent = ground.transform;
            }
            else if (position.x == 0)
            {
                position.x *= 10;
                position.z *= 10;
                GameObject plane = Instantiate(grounds[1], position, Quaternion.identity);
                plane.transform.parent = ground.transform;
            }
            else
            {
                position.x *= 10;
                position.z *= 10;
                GameObject plane = Instantiate(grounds[2], position, Quaternion.identity);
                plane.transform.parent = ground.transform;
            }
            PayMoney(price);
            rent += price/20;
            GameManager.GroundCount++;
            return;
        }
        WindowManager.ShowNotificationAlert("Not enough money");
        return ;

    }

    public void GameOverCheack()
    {
        if (budget < 1 && popularity < 1 && GameManager.Projects.Count < 1)
            WindowManager.ShowNotificationAlert("Game Over.", () => { SceneManager.LoadScene("MainMenu"); });
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
        GameOverCheack();
    }
    #endregion
}
