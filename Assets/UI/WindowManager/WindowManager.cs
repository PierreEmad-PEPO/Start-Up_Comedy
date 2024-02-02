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
    
    static WindowName crrentWindow;
    static SubWindowName crrentSubWindow;

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

        GameObject negotiationUI = UI.transform.Find("Negotiation").gameObject;
        VisualElement negotiationRoot = negotiationUI.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.Negotation, negotiationRoot);
        subWindowGameObject.Add(SubWindowName.Negotation, negotiationUI);

        GameObject projectInfoUI = UI.transform.Find("ProjectInfo").gameObject;
        VisualElement projectInfoRoot = projectInfoUI.GetComponent<UIDocument>().rootVisualElement;
        subWindow.Add(SubWindowName.ProjectInfo, projectInfoRoot);
        subWindowGameObject.Add(SubWindowName.ProjectInfo, projectInfoUI);

    }

    public static void OpenWindow(WindowName windowName)
    {
        if (window.ContainsKey(windowName))
        {
            window[crrentWindow].style.display = DisplayStyle.None;
            crrentWindow = windowName;
            window[crrentWindow].style.display = DisplayStyle.Flex;
        }
    }

    public static void OpenSubWindow(SubWindowName subWindowName)
    {
        if (subWindow.ContainsKey(subWindowName))
        {
            subWindow[crrentSubWindow].style.display = DisplayStyle.None;
            crrentSubWindow = subWindowName;
            subWindow[crrentSubWindow].style.display = DisplayStyle.Flex;
        }
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