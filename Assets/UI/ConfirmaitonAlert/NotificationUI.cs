using System;
using UnityEngine;
using UnityEngine.UIElements;


public class NotificatoinUI : MonoBehaviour
{
    private VisualElement root;
    private Label messageLabel;
    private Button okButton;

    private void Start()
    {
        SetVisualELement();
        root.style.display = DisplayStyle.None;
    }

    void SetVisualELement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        messageLabel = root.Q<Label>("Message");
        okButton = root.Q<Button>("Ok");
        okButton.clicked += () =>
        {
            root.style.display = DisplayStyle.None;
        };
        
    }

    public void SetNotification(string message)
    {
        messageLabel.text = message;
    }
}
