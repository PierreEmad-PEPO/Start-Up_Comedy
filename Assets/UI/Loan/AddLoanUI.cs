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
            if (money.value > GameManager.StartUp.Budget / 2 || GameManager.Loans.Count > 10)
            {
                WindowManager.ShowNotificationAlert("Sorry, But The Bank Refused Your Request.");
            }
            else
            {
                Loan loan = new Loan(money.value);
                GameManager.Loans.Add(loan);
                GameManager.StartUp.AddMoney(money.value);
                WindowManager.GetWindowGameObject(WindowName.Loan).GetComponent<LoanUI>().RebuildLoansList();
                WindowManager.ShowNotificationAlert("Congrats The Bank Accepted Your Request.");

            }
        };
        root.style.display = DisplayStyle.None;
    }

}
