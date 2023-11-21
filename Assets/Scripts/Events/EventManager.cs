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


    
}
