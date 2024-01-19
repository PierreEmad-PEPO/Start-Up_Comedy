using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Negotiation : MonoBehaviour
{
    [SerializeField] VisualTreeAsset sentMessage;
    [SerializeField] VisualTreeAsset recievedMessage;

    Employee employee;

    Button exit;
    Label employeeName;
    ScrollView chatView;
    SliderInt sliderInt;
    Button send;

    VisualElement root;


    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
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

        exit.clicked += () =>
        {
            chatView.Clear();
            root.style.display = DisplayStyle.None;
        };

        send.clicked += () =>
        {
            Negotiate(sliderInt.value);
        };
    }

    public void SetTheEmployee(Employee employee)
    {
        this.employee = employee;
        employeeName.text = employee.Name;
        root.style.display = DisplayStyle.Flex;
    }

    void Negotiate(int salary)
    {
        var sent = sentMessage.Instantiate();
        sent.Q<Label>("Message").text = salary.ToString() + " $";
        chatView.Add(sent);

        var recieved = recievedMessage.Instantiate();
        chatView.Add(recieved);
    }
}
