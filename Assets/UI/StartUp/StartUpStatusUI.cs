using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class StartUpStatusUI : MonoBehaviour
{
    UIDocument startupStatusUI;
    VisualElement root;
    Label startupName;
    Label moneyLabel;
    Label popularityLabel;
    Label totalHrSkillsLabel;
    Label totalMarketingLabel;
    Label entertainmentLabel;
    Label rentLabel;
    Label fireSystemLabel;
    Label securityLabel;


    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
        StartCoroutine(SecondTick());
        root.style.display = DisplayStyle.None;
        UpdateData();
    }

    void SetVisualElement()
    {
        startupStatusUI = GetComponent<UIDocument>();
        root = startupStatusUI.rootVisualElement;
        startupName = root.Q<Label>("StartUpName");
        moneyLabel = root.Q<Label>("Money");
        popularityLabel = root.Q<Label>("Popularity");
        totalHrSkillsLabel = root.Q<Label>("TotalHR");
        totalMarketingLabel = root.Q<Label>("TotalMarketing");
        entertainmentLabel = root.Q<Label>("Entertainment");
        rentLabel = root.Q<Label>("Rent");
        fireSystemLabel = root.Q<Label>("FireSystem");
        securityLabel = root.Q<Label>("Security");
        root.Q<Button>("Exit").clicked += () => { root.style.display = DisplayStyle.None; };
    }

    void UpdateData()
    {
        var startUp = GameManager.StartUp;
        startupName.text = startUp.CompanyName;
        moneyLabel.text = "Money: " + startUp.Budget;
        popularityLabel.text = "Popularity: " + (startUp.Popularity * 100)/GameManager.StartUp.MAX_POPULARITY + "%";
        totalHrSkillsLabel.text = "Total HR Skills: " + (startUp.TotalHrSkills * 100)/500 + "%";
        totalMarketingLabel.text = "Total Marketing Skills: " + (startUp.TotalMarketingSkills * 100)/ 500 + "%";
        entertainmentLabel.text = "Entertainment: " + startUp.Entertainment;
        rentLabel.text = "Rent: " + startUp.Rent.ToString() + "$" ;
        fireSystemLabel.text = "Fire System Level: " + startUp.FireSystemLevel;
        securityLabel.text = "Security Level: " + startUp.SecurityLevel;
    }

    IEnumerator SecondTick()
    {
        while (true)
        {
            if (root.style.display == DisplayStyle.Flex)
            {
                UpdateData();
            }
            yield return new WaitForSeconds(1);
        }
    }
}
