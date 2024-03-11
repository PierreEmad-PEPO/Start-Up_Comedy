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
    Label hasDataAnalystLabel;
    Label fireSystemLabel;
    Label securityLabel;


    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
        StartCoroutine(SecondTick());
        root.style.display = DisplayStyle.None;
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
        hasDataAnalystLabel = root.Q<Label>("DataAnalyst");
        fireSystemLabel = root.Q<Label>("FireSystem");
        securityLabel = root.Q<Label>("Security");
        root.Q<Button>("Exit").clicked += () => { root.style.display = DisplayStyle.None; };
    }

    void UpdateData()
    {
        var startUp = GameManager.StartUp;
        startupName.text = startUp.CompanyName;
        moneyLabel.text = "Money: " + startUp.Budget;
        popularityLabel.text = "Popularity: " + startUp.Popularity;
        totalHrSkillsLabel.text = "Total HR Skills: " + startUp.TotalHrSkills;
        totalMarketingLabel.text = "Total Marketing Skills: " + startUp.TotalMarketingSkills;
        entertainmentLabel.text = "Entertainment: " + startUp.Entertainment;
        hasDataAnalystLabel.text = "Has Data Analyst: " + startUp.HasDataAnalyst.ToString();
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
