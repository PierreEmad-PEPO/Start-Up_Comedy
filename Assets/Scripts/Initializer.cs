using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] StartUp startUp;
    static bool initialized = false;

    [SerializeField] public Camera mainCamera;
    [SerializeField] public LayerMask ground;

    private void Awake()
    {
        if (initialized) return;
        EventManager.Init();
        WindowManager.Init(UI);
        GameManager.Init(startUp);
        InputManager.init(mainCamera, ground);
        initialized = true;
        //DontDestroyOnLoad(gameObject);
    }
}
