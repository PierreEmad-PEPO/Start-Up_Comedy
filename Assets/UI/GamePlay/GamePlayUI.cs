using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GamePlayUI : MonoBehaviour
{
    [SerializeField] UIDocument acceptedProjectList;
    [SerializeField] UIDocument hitingList;
    VisualElement root;
    Button projectsButton;
    Button hrButton;

    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        projectsButton = root.Q<Button>("ProjectsButton");
        hrButton = root.Q<Button>("HrButton");
        projectsButton.clicked += () =>
        {
            acceptedProjectList.rootVisualElement.style.display = DisplayStyle.Flex;
        };

        hrButton.clicked += () =>
        {
            hitingList.rootVisualElement.style.display = DisplayStyle.Flex;
        };



    }
}

