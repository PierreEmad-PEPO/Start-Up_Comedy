using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initializer : MonoBehaviour
{
    [SerializeField] GameObject UI;
    private void Awake()
    {
        EventManager.Init();
        WindowManager.Init(UI);
        GameManager.Init();
    }
}
