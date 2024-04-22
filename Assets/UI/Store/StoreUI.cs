using System;
using UnityEngine;
using UnityEngine.UIElements;

public class StoreWindow : MonoBehaviour
{
    ObjectsDatabaseSO database;
    [SerializeField] VisualTreeAsset itemCard;
    UIDocument uIDocument;
    VisualElement root;
    ScrollView scrollView;

    
    void Start()
    {
        database = GameObject.Find("PlacementSystem").GetComponent<PlacementSystem>().DatabaseSO;
        SetVisualElement();
    }
 

    void SetVisualElement()
    {
        uIDocument = GetComponent<UIDocument>();
        root = uIDocument.rootVisualElement;
        root.Q<Button>("Exit").clicked += () => { root.style.display = DisplayStyle.None; };
        scrollView = root.Q<ScrollView>("Content");

        // Create a container for the grid
        var gridContainer = new VisualElement();
        gridContainer.style.flexDirection = FlexDirection.Row;
        gridContainer.style.flexWrap = Wrap.Wrap;
        gridContainer.style.width = Length.Percent(100); // Full width
        gridContainer.style.height = Length.Percent(100); // Full height

        // Create buttons and add them to the grid
        for (int index = 0; index < database.items.Count; index++)
        {
            VisualElement item = itemCard.CloneTree();
            item.style.width = Length.Percent(30);
            item.style.height = Length.Percent(40);
            item.style.marginBottom = item.style.marginTop =
            item.style.marginLeft = item.style.marginRight = Length.Percent(1);
            item.userData = (int)index;
            item.Q<VisualElement>("ItemImage").style.backgroundImage =
            new StyleBackground(database.items[index].img);
            item.Q<Label>("ItemName").text = database.items[index].name;
            item.Q<Label>("ItemCost").text = database.items[index].cost + "$";
            item.RegisterCallback((ClickEvent evt) =>
            {
                WindowManager.ShowConfirmationAlert("Are you sure ?!", () =>
                {
                    GameManager.StartUp.BuyFromStore(database.items[(int)item.userData]);
                    root.style.display = DisplayStyle.None;
                });
            });
            gridContainer.Add(item);
        }

        scrollView.Add(gridContainer);

    }
}
