using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class AddLoanUI : MonoBehaviour
{
    SliderInt money;
    VisualElement root;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        money = root.Q<SliderInt>("Money");
        root.Q<Button>("Exit").clicked += () => 
        {
            root.style.display = DisplayStyle.None;
        };
        root.Q<Button>("Send").clicked += () =>
        {
            if (money.value > GameManager.StartUp.Budget / 2)
            {
                WindowManager.ShowNotificationAlert("Soory, But The Bank Refused Rour Requist.");
            }
            else
            {
                Loan loan = new Loan(money.value);
                GameManager.Loans.Add(loan);
                WindowManager.GetWindowGameObject(WindowName.Loan).GetComponent<LoanUI>().RebuildLoansList();
                WindowManager.ShowNotificationAlert("Congrites The Bank Accepted Your Requist.");

            }
        };
        root.style.display = DisplayStyle.None;
    }

}
