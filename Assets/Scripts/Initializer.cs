using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] StartUp startUp;

    [SerializeField] public Camera mainCamera;
    [SerializeField] public LayerMask ground;

    private void Awake()
    {
        EventManager.Init();
        WindowManager.Init(UI);
        GameManager.Init(startUp);
        InputManager.init(mainCamera, ground);
        
    }
}
