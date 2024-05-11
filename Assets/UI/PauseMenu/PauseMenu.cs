using UnityEngine;
using UnityEngine.UIElements;
public class PauseMenu : MonoBehaviour
{
    [SerializeField] SettingMenu settingMenu;
    VisualElement root;
    float provesScalTime = 1;

    void Start()
    {
        SetVisualElment();
    }

    void SetVisualElment()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Button>("Resume").clicked += () =>
        {
            Time.timeScale = provesScalTime;
            root.style.display = DisplayStyle.None;
        };

        root.Q<Button>("Setting").clicked += () =>
        {
            settingMenu.Display();
        };
        root.Q<Button>("Exit").clicked += () =>
        {
            Application.Quit();
        };
        root.style.display = DisplayStyle.None;

    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            provesScalTime = Time.timeScale;
            Time.timeScale = 0;
            root.style.display = DisplayStyle.Flex;
        }
    }

}
