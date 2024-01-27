using System;
using UnityEngine;
using UnityEngine.UIElements;

public class Negotiation : MonoBehaviour
{
    string[] rejectingMessages = new string[9]
    {
        "Salary too low, can't accept.",
        "Offer declined, low salary.",
        "Regretfully decline, salary below expectations.",
        "Salary doesn't meet goals, can't accept.",
        "Grateful, but can't accept low salary.",
        "Decline offer, salary doesn't match.",
        "Respectfully decline, salary not enough.",
        "Offer declined, salary below market standards.",
        "Can't accept, salary doesn't meet expectations."
    };

    string[] acceptingMessages = new string[10]
    {
        "Thank you for the offer, I gladly accept.",
        "I'm excited to accept the job offer.",
        "Offer accepted, looking forward to starting.",
        "Delighted to accept the job offer.",
        "I gladly accept the offer, thank you.",
        "Thrilled to accept the job offer.",
        "Accepting the offer with pleasure.",
        "I'm honored to accept the job offer.",
        "Offer accepted, eager to contribute to the team.",
        "Excited to join the team, offer accepted."
    };

    private const int MIN_TRIES = 1;
    private const int MAX_TRIES = 5;
    private int currentTry;


    [SerializeField] VisualTreeAsset sentMessage;
    [SerializeField] VisualTreeAsset recievedMessage;

    Employee employee;

    Button exit;
    Label employeeName;
    ScrollView chatView;
    Label finalMessage;
    SliderInt sliderInt;
    Button send;
    Scroller scroller;

    VisualElement root;

    EmployeeEventInvoker onEmployeeHired;
    EmployeeEventInvoker onEmployeeCanceled;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
        onEmployeeHired = gameObject.AddComponent<EmployeeEventInvoker>();
        onEmployeeCanceled = gameObject.AddComponent<EmployeeEventInvoker>();
        EventManager.AddEmployeeEventInvoker(EventEnum.OnEmployeeHired, onEmployeeHired);
        EventManager.AddEmployeeEventInvoker(EventEnum.OnEmployeeCanceled, onEmployeeCanceled);
        root.style.display = DisplayStyle.None;
    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        exit = root.Q<Button>("Exit");
        employeeName = root.Q<Label>("Name");
        chatView = root.Q<ScrollView>("ChatView");
        sliderInt = root.Q<SliderInt>("SliderInt");
        send = root.Q<Button>("Send");
        finalMessage = root.Q<Label>("FinalMessage");
        scroller = chatView.Q<Scroller>();

        exit.clicked += () =>
        {
            Exit();
        };

        send.clicked += () =>
        {
            Negotiate(sliderInt.value);
        };
    }

    private void Exit()
    {
        chatView.Clear();
        finalMessage.style.display = DisplayStyle.None;
        root.style.display = DisplayStyle.None;
    }

    public void SetTheEmployee(Employee employee)
    {
        currentTry = RandomGenerator.NextInt(MIN_TRIES, MAX_TRIES + 1);
        Debug.Log(currentTry);
        this.employee = employee;
        employeeName.text = employee.Name;
    }

    void Negotiate(int salary)
    {
        if (currentTry <= 0)
        {
            return;
        }

        var sent = sentMessage.Instantiate();
        sent.Q<Label>("Message").text = salary.ToString() + " $";
        chatView.contentContainer.Add(sent);

        var recieved = recievedMessage.Instantiate();

        if (salary >= employee.MinSalary)
        {
            recieved.Q<Label>("Message").text = acceptingMessages[RandomGenerator.NextInt(0, acceptingMessages.Length)];
            currentTry = 0;
            chatView.contentContainer.Add(recieved);
            finalMessage.style.display = DisplayStyle.Flex;
            finalMessage.text = "Employee Is Hired";
            finalMessage.style.color = Color.green;
            employee.Salary = salary;
            onEmployeeHired.Invoke(employee);
            return;
        }
        else 
        {
            recieved.Q<Label>("Message").text = rejectingMessages[RandomGenerator.NextInt(0, rejectingMessages.Length)];
            chatView.Add(recieved);
        }

        currentTry--;
        if (currentTry <= 0) 
        {
            finalMessage.style.display = DisplayStyle.Flex;
            finalMessage.text = "Employee Closed The Neogtiation";
            finalMessage.style.color = Color.red;
            onEmployeeCanceled.Invoke(employee);
        }
        Debug.Log(currentTry);

        scroller.lowValue = -10000;
        scroller.highValue = 10000;
        scroller.value = scroller.highValue;
    }
}
