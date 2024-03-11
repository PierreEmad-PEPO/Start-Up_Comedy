using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class LoanUI : MonoBehaviour
{
    [SerializeField]
    private VisualTreeAsset loanCard;

    ListView loanList;
    VisualElement root;
    List<Loan> loans;
    // Start is called before the first frame update
    void Start()
    {
        loans = GameManager.Loans;
        SetVisuelElement();
        InvokeRepeating("UpdateLoans", 1, 1);
    }

    void SetVisuelElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        loanList = root.Q<ListView>("LoanList");
        root.Q<Button>("Exit").clicked += () =>
        {
            root.style.display = DisplayStyle.None;
        };
        root.Q<Button>("AddLoan").clicked += () =>
        {
            WindowManager.OpenSubWindow(SubWindowName.AddLoan);
        };
        InitLoansList();
        root.style.display = DisplayStyle.None;
    }

    void InitLoansList()
    {
        loanList.makeItem = () =>
        {

            var temp = loanCard.Instantiate();
            return temp;

        };

        loanList.bindItem = (item, index) =>
        {
            item.userData = loans[index];
            item.Q<Label>("Money").text = loans[index].Money.ToString("N0");
            item.Q<Label>("Deadline").text = loans[index].Deadline.ToString("00:00:00");
        };
        loanList.fixedItemHeight = 110;
        loanList.itemsSource = loans;
    }

    void UpdateLoans()
    {
        for (int index = 0; index < loans.Count; index++)
        {
            loans[index].UpdateLoan();
            if (loans[index].Deadline == 0)
            {
                int mo = (int)loans[index].Money;
                int per = mo / 10;
                GameManager.StartUp.AddMoney(- (mo + per));
                loans.RemoveAt(index);
            }
            RebuildLoansList();
        }
    }

    public void RebuildLoansList()
    {
        loanList.Rebuild();
    }
}
