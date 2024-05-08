using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class MainMenu : MonoBehaviour
{
    VisualElement root;
    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Play").clicked += () =>
        {
            SceneManager.LoadScene("GamePlay");
        };
        root.Q<Button>("Exit").clicked -= () => { Application.Quit(); };
    }
}
