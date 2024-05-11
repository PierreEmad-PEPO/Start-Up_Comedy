using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SettingMenu : MonoBehaviour
{
    [SerializeField] AudioSource sound;
    VisualElement root;
    SliderInt soundSlid;
    // Start is called before the first frame update
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        soundSlid = root.Q<SliderInt>("Sound");
        soundSlid.RegisterValueChangedCallback<int>(e => { sound.volume = (float)e.newValue / 100; });
        root.Q<Toggle>("Mute").RegisterValueChangedCallback<bool>(e => { sound.mute = e.newValue; });
        root.Q<Button>("Exit").clicked += () => { root.style.display = DisplayStyle.None; };
        root.style.display = DisplayStyle.None;
    }

    public void Display()
    {
        soundSlid.value = (int)(sound.volume * 100f);
        root.style.display = DisplayStyle.Flex;
    }
    // Update is called once per frame
}
