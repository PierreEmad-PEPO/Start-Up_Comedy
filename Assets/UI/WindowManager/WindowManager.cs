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

        GameObject negotiationUI = UI.transform.Find("Negotiation").gameObject;
        VisualElement negotiationRoot = negotiationUI.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.Negotation, negotiationRoot);
        subWindowGameObject.Add(SubWindowName.Negotation, negotiationUI);

        GameObject projectInfoUI = UI.transform.Find("ProjectInfo").gameObject;
        VisualElement projectInfoRoot = projectInfoUI.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.ProjectInfo, projectInfoRoot);
        subWindowGameObject.Add(SubWindowName.ProjectInfo, projectInfoUI);

        GameObject confirmationAlertUI = UI.transform.Find("ConfirmationAlert").gameObject;
        VisualElement confirmationAlertRoot = confirmationAlertUI.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.ConfirmationAlert, confirmationAlertRoot);
        subWindowGameObject.Add(SubWindowName.ConfirmationAlert, confirmationAlertUI);
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

    public static void AddWindow(WindowName windowName, VisualElement UIDecomentRoot)
    {
        window.Add(windowName, UIDecomentRoot);
    }

    public static void AddSubWindow(SubWindowName subWindowName, VisualElement UIDecomentRoot)
    {
        subWindow.Add(subWindowName, UIDecomentRoot);
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


}