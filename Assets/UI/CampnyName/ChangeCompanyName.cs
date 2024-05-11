using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ChangeCompanyName : MonoBehaviour
{
    // Start is called before the first frame update
    VisualElement root;
    TextField _name;
    void Start()
    {
        if (GameManager.StartUp.CompanyName != "")
        { 
            Destroy(gameObject);
            return;
        }
        root = GetComponent<UIDocument>().rootVisualElement;
        _name = root.Q<TextField>("Name");
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Return))
        {
            if (_name.value.Length > 0)
            {
                GameManager.StartUp.CompanyName = _name.value;
                Time.timeScale = 1;
                Destroy(gameObject);
            }
        }
    }
}
