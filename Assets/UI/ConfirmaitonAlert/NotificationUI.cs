using System;
using UnityEngine;
using UnityEngine.UIElements;


public class NotificationUI : MonoBehaviour
{
    private VisualElement root;
    private Label messageLabel;
    private Button okButton;
    Action action;

    private void Start()
    {
        SetVisualElement();
        root.style.display = DisplayStyle.None;
    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        messageLabel = root.Q<Label>("Message");
        okButton = root.Q<Button>("Ok");
        okButton.clicked += () =>
        {
            if (action != null)
                action.Invoke();
            root.style.display = DisplayStyle.None;
        };
        
    }

    public void SetNotification(string message)
    {
        messageLabel.text = message;
    }

    public void SetNotification(string message,Action action)
    {
        messageLabel.text = message;
        this.action = action;
    }
}
