using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public static class EventManager
{
    public static void Init()
    {
        foreach (int i in Enum.GetValues(typeof(EventEnum)))
        {
            voidEventInvokers.Add((EventEnum)i, new List<VoidEventInvoker>());
            voidEventListeners.Add((EventEnum)i, new List<UnityAction>());

            intEventInvokers.Add((EventEnum)i, new List<IntEventInvoker>());
            intEventListeners.Add((EventEnum)i, new List<UnityAction<int>>());

            gameObjectEventInvokers.Add((EventEnum)i, new List<GameObjectEventInvoker>());
            gameObjectEventListeners.Add((EventEnum)i, new List<UnityAction<GameObject>>());

            employeeEventInvokers.Add((EventEnum)i, new List<EmployeeEventInvoker>());
            employeeEventListeners.Add((EventEnum)i, new List<UnityAction<Employee>>());

            projectEventInvokers.Add((EventEnum)i, new List<ProjectEventInvoker>());
            projectEventListeners.Add((EventEnum)i, new List<UnityAction<Project>>());

        }
    }

    // Void Event Handling
    private static Dictionary<EventEnum, List<VoidEventInvoker>> voidEventInvokers = 
        new Dictionary<EventEnum, List<VoidEventInvoker>>();
    private static Dictionary<EventEnum, List<UnityAction>> voidEventListeners =
        new Dictionary<EventEnum, List<UnityAction>>();
    public static void AddVoidEventInvoker(EventEnum eventEnum, VoidEventInvoker eventInvoker)
    {
        voidEventInvokers[eventEnum].Add(eventInvoker);

        foreach (UnityAction listener in voidEventListeners[eventEnum])
        {
            eventInvoker.AddListener(listener);
        }
    }
    public static void AddVoidEventListener(EventEnum eventEnum, UnityAction listener) 
    {
        voidEventListeners[eventEnum].Add(listener);

        foreach (VoidEventInvoker voidEventInvoker in voidEventInvokers[eventEnum])
        {
            voidEventInvoker.AddListener(listener);
        }
    }
    //-------------------------------------------------------------


    // Int Event Handling
    private static Dictionary<EventEnum, List<IntEventInvoker>> intEventInvokers =
        new Dictionary<EventEnum, List<IntEventInvoker>>();
    private static Dictionary<EventEnum, List<UnityAction<int>>> intEventListeners =
        new Dictionary<EventEnum, List<UnityAction<int>>>();
    public static void AddIntEventInvoker(EventEnum eventEnum, IntEventInvoker eventInvoker)
    {
        intEventInvokers[eventEnum].Add(eventInvoker);

        foreach (UnityAction<int> listener in intEventListeners[eventEnum])
        {
            eventInvoker.AddListener(listener);
        }
    }
    public static void AddIntEventListener(EventEnum eventEnum, UnityAction<int> listener)
    {
        intEventListeners[eventEnum].Add(listener);

        foreach (IntEventInvoker intEventInvoker in intEventInvokers[eventEnum])
        {
            intEventInvoker.AddListener(listener);
        }
    }
    //-------------------------------------------------------------


    // GameObject Event Handling
    private static Dictionary<EventEnum, List<GameObjectEventInvoker>> gameObjectEventInvokers =
        new Dictionary<EventEnum, List<GameObjectEventInvoker>>();
    private static Dictionary<EventEnum, List<UnityAction<GameObject>>> gameObjectEventListeners =
        new Dictionary<EventEnum, List<UnityAction<GameObject>>>();
    public static void AddGameObjectEventInvoker(EventEnum eventEnum, GameObjectEventInvoker eventInvoker)
    {
        gameObjectEventInvokers[eventEnum].Add(eventInvoker);

        foreach (UnityAction<GameObject> listener in gameObjectEventListeners[eventEnum])
        {
            eventInvoker.AddListener(listener);
        }
    }
    public static void AddGameObjectEventListener(EventEnum eventEnum, UnityAction<GameObject> listener)
    {
        gameObjectEventListeners[eventEnum].Add(listener);

        foreach (GameObjectEventInvoker projectEventInvoker in gameObjectEventInvokers[eventEnum])
        {
            projectEventInvoker.AddListener(listener);
        }
    }
    //-------------------------------------------------------------

    // Employee Event Handling
    private static Dictionary<EventEnum, List<EmployeeEventInvoker>> employeeEventInvokers =
        new Dictionary<EventEnum, List<EmployeeEventInvoker>>();
    private static Dictionary<EventEnum, List<UnityAction<Employee>>> employeeEventListeners =
        new Dictionary<EventEnum, List<UnityAction<Employee>>>();
    public static void AddEmployeeEventInvoker(EventEnum eventEnum, EmployeeEventInvoker eventInvoker)
    {
        employeeEventInvokers[eventEnum].Add(eventInvoker);

        foreach (UnityAction<Employee> listener in employeeEventListeners[eventEnum])
        {
            eventInvoker.AddListener(listener);
        }
    }
    public static void AddEmployeeEventListener(EventEnum eventEnum, UnityAction<Employee> listener)
    {
        employeeEventListeners[eventEnum].Add(listener);

        foreach (EmployeeEventInvoker employeeEventInvoker in employeeEventInvokers[eventEnum])
        {
            employeeEventInvoker.AddListener(listener);
        }
    }
    //-------------------------------------------------------------


    // Project Event Handling
    private static Dictionary<EventEnum, List<ProjectEventInvoker>> projectEventInvokers =
        new Dictionary<EventEnum, List<ProjectEventInvoker>>();
    private static Dictionary<EventEnum, List<UnityAction<Project>>> projectEventListeners =
        new Dictionary<EventEnum, List<UnityAction<Project>>>();
    public static void AddProjectEventInvoker(EventEnum eventEnum, ProjectEventInvoker eventInvoker)
    {
        projectEventInvokers[eventEnum].Add(eventInvoker);

        foreach (UnityAction<Project> listener in projectEventListeners[eventEnum])
        {
            eventInvoker.AddListener(listener);
        }
    }
    public static void AddProjectEventListener(EventEnum eventEnum, UnityAction<Project> listener)
    {
        projectEventListeners[eventEnum].Add(listener);

        foreach (ProjectEventInvoker projectEventInvoker in projectEventInvokers[eventEnum])
        {
            projectEventInvoker.AddListener(listener);
        }
    }
    //-------------------------------------------------------------

}
