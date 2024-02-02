using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class MarketingUI : MonoBehaviour
{
    VisualElement root;

    void Start()
    {
        SetVisualElement();
    }

    void SetVisualElement()
    { 
        root = GetComponent<UIDocument>().rootVisualElement;
        root.Q<Label>("SocialValue").text = GameManager.GetMarketingPrice(MarketingEnum.SocialAD).ToString();
        root.Q<Label>("TvValue").text = GameManager.GetMarketingPrice(MarketingEnum.TVAD).ToString();
        root.Q<Label>("RadoiValue").text = GameManager.GetMarketingPrice(MarketingEnum.RadoiAd).ToString();

        root.Q<Button>("SocialAd").clicked += () =>
        {
            GameManager.StartUp.DoAD(MarketingEnum.SocialAD);
        };
        root.Q<Button>("TvAd").clicked += () =>
        {
            GameManager.StartUp.DoAD(MarketingEnum.TVAD);
        };
        root.Q<Button>("RadoiAd").clicked += () =>
        {
            GameManager.StartUp.DoAD(MarketingEnum.RadoiAd);
        };

        root.Q<Button>("Exit").clicked += () =>
        {
            root.style.display = DisplayStyle.None;
        };
        root.style.display = DisplayStyle.None;

    }

    
}
