using System;
using UnityEngine;
using UnityEngine.UIElements;


public class ConfirmUI : MonoBehaviour
{
    private Action action = null;

    private VisualElement root;
    private Label messageLabel;
    private Button yesButton;
    private Button noButton;

    private void Start()
    {
        SetVisualELement();
        root.style.display = DisplayStyle.None;
    }

    void SetVisualELement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        messageLabel = root.Q<Label>("Message");
        yesButton = root.Q<Button>("Yes");
        yesButton.clicked += () =>
        {
            root.style.display = DisplayStyle.None;
        };
        noButton = root.Q<Button>("No");
        noButton.clicked += () =>
        {
            root.style.display = DisplayStyle.None;
        };
    }

    public void SetConfirm(string message, Action newAction)
    {
        messageLabel.text = message;

        if (action != null) yesButton.clicked -= action;

        action = newAction;
        yesButton.clicked += action;
    }
}
