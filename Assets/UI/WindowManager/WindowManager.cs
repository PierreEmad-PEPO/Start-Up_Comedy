using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public static class WindowManager
{
    static Dictionary<WindowName, VisualElement> window = new Dictionary<WindowName, VisualElement>();
    static Dictionary<SubWindowName, VisualElement> subWindow = new Dictionary<SubWindowName, VisualElement>();

    static Dictionary<WindowName, GameObject> windowGameObject = new Dictionary<WindowName, GameObject>();
    static Dictionary<SubWindowName, GameObject> subWindowGameObject = new Dictionary<SubWindowName, GameObject>();
    
    static WindowName currentWindow;
    static SubWindowName currentSubWindow;

    public static void Init(GameObject UI)
    {
        // for now 
        GameObject hiringUI = UI.transform.Find("HiringEmployee").gameObject;
        VisualElement hiringRoot = hiringUI.GetComponent<UIDocument>().rootVisualElement;
        window.Add(WindowName.HiringEmployees, hiringRoot);
        windowGameObject.Add(WindowName.HiringEmployees, hiringUI);

        GameObject projectsUI = UI.transform.Find("AcceptedProject").gameObject;
        VisualElement projectsRoot = projectsUI.GetComponent<UIDocument>().rootVisualElement;
        window.Add(WindowName.Projects, projectsRoot);
        windowGameObject.Add(WindowName.Projects, projectsUI);

        GameObject marketingUI = UI.transform.Find("Marketing").gameObject;
        VisualElement marketingRoot = marketingUI.GetComponent<UIDocument>().rootVisualElement;
        window.Add(WindowName.Markiting, marketingRoot);
        windowGameObject.Add(WindowName.Markiting, marketingUI);

        GameObject employeesUI = UI.transform.Find("HiredEmployees").gameObject;
        VisualElement employeesRoot = employeesUI.GetComponent<UIDocument>().rootVisualElement;
        window.Add(WindowName.Employees, employeesRoot);
        windowGameObject.Add(WindowName.Employees, employeesUI);

        GameObject loans = UI.transform.Find("Loans").gameObject;
        VisualElement loansRoot = loans.GetComponent<UIDocument>().rootVisualElement;
        window.Add(WindowName.Loan, loansRoot);
        windowGameObject.Add(WindowName.Loan, loans);

        GameObject stor = UI.transform.Find("Store").gameObject;
        VisualElement storRoot = stor.GetComponent<UIDocument>().rootVisualElement;
        window.Add(WindowName.Store, storRoot);
        windowGameObject.Add(WindowName.Store, stor);

        GameObject officeManager = UI.transform.Find("OfficeManager").gameObject;
        VisualElement officeRoot = officeManager.GetComponent<UIDocument>().rootVisualElement;
        window.Add(WindowName.Office, officeRoot);
        windowGameObject.Add(WindowName.Office, officeManager);

        GameObject negotiationUI = UI.transform.Find("Negotiation").gameObject;
        VisualElement negotiationRoot = negotiationUI.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.Negotation, negotiationRoot);
        subWindowGameObject.Add(SubWindowName.Negotation, negotiationUI);

        GameObject addLoan = UI.transform.Find("AddLoan").gameObject;
        VisualElement addLoanRoot = addLoan.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.AddLoan, addLoanRoot);
        subWindowGameObject.Add(SubWindowName.AddLoan, addLoan);

        GameObject projectInfoUI = UI.transform.Find("ProjectInfo").gameObject;
        VisualElement projectInfoRoot = projectInfoUI.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.ProjectInfo, projectInfoRoot);
        subWindowGameObject.Add(SubWindowName.ProjectInfo, projectInfoUI);

        GameObject confirmationAlertUI = UI.transform.Find("ConfirmationAlert").gameObject;
        VisualElement confirmationAlertRoot = confirmationAlertUI.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.ConfirmationAlert, confirmationAlertRoot);
        subWindowGameObject.Add(SubWindowName.ConfirmationAlert, confirmationAlertUI);

        GameObject notificationAlertUI = UI.transform.Find("NotificationAlert").gameObject;
        VisualElement notificationAlertRoot = notificationAlertUI.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.NotificationAlert, notificationAlertRoot);
        subWindowGameObject.Add(SubWindowName.NotificationAlert, notificationAlertUI);

        GameObject startUpStatusUI = UI.transform.Find("StartUpStatus").gameObject;
        VisualElement startUpStatusRoot = startUpStatusUI.GetComponent<UIDocument>().rootVisualElement;
        window.Add(WindowName.StartUpStatus, startUpStatusRoot);
        windowGameObject.Add(WindowName.StartUpStatus, startUpStatusUI);
    }

    public static void OpenWindow(WindowName windowName)
    {
        if (window.ContainsKey(windowName))
        {
            window[currentWindow].style.display = DisplayStyle.None;
            currentWindow = windowName;
            window[currentWindow].style.display = DisplayStyle.Flex;
        }
    }

    public static void OpenSubWindow(SubWindowName subWindowName)
    {
        if (subWindow.ContainsKey(subWindowName))
        {
            subWindow[currentSubWindow].style.display = DisplayStyle.None;
            currentSubWindow = subWindowName;
            subWindow[currentSubWindow].style.display = DisplayStyle.Flex;
        }
    }

    public static void ShowConfirmationAlert(string message, Action action)
    {
        GetSubWindowGameObject(SubWindowName.ConfirmationAlert).GetComponent<ConfirmUI>().SetConfirm(message, action);
        OpenSubWindow(SubWindowName.ConfirmationAlert);
    }

    public static void ShowNotificationAlert(string message)
    {
        GetSubWindowGameObject(SubWindowName.NotificationAlert).GetComponent<NotificatoinUI>().SetNotification(message);
        OpenSubWindow(SubWindowName.NotificationAlert);
    }

    public static void AddWindow(WindowName windowName, VisualElement UIDecomentRoot, GameObject gameObject)
    {
        window.Add(windowName, UIDecomentRoot);
        windowGameObject.Add(windowName, gameObject);
    }

    public static void AddSubWindow(SubWindowName subWindowName, VisualElement UIDecomentRoot, GameObject gameObject)
    {
        subWindow.Add(subWindowName, UIDecomentRoot);
        subWindowGameObject.Add(subWindowName, gameObject);
    }

    public static VisualElement GetWindow(WindowName windowName)
    {
        if (window.ContainsKey(windowName))
        {
            return window[windowName];
        }

        return null;
    }
    public static VisualElement GetSubWindow(SubWindowName subWindowName)
    {
        if (subWindow.ContainsKey(subWindowName))
        {
            return subWindow[subWindowName];
        }

        return null;
    }

    public static GameObject GetSubWindowGameObject(SubWindowName subWindowName)
    {
        if (subWindow.ContainsKey(subWindowName))
        {
            return subWindowGameObject[subWindowName];
        }

        return null;
    }

    public static GameObject GetWindowGameObject(WindowName windowName)
    {
        if (window.ContainsKey(windowName))
        {
            return windowGameObject[windowName];
        }

        return null;
    }

    public static bool isThereWindoOpend()
    {
        if (GetWindow(currentWindow).style.display == DisplayStyle.None
            && GetSubWindow(currentSubWindow).style.display == DisplayStyle.None)
        {
            return false;
        }
        return true;
    }
}