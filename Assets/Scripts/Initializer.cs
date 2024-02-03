using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] GameObject UI;
    [SerializeField] StartUp startUp;
    private void Awake()
    {
        EventManager.Init();
        WindowManager.Init(UI);
        GameManager.Init(startUp);
    }
}
