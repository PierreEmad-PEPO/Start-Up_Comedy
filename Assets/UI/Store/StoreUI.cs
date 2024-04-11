using UnityEngine;
using UnityEngine.UIElements;

public class StoreWindow : MonoBehaviour
{
    private VisualElement categoryContainer;
    private VisualElement itemContainer;

    private void Start()
    {
        var visualElement = GetComponent<UIDocument>().rootVisualElement.Q<VisualElement>("store-window");

        categoryContainer = visualElement.Q<VisualElement>("category-container");
        itemContainer = visualElement.Q<VisualElement>("item-container");

        // Add categories
        AddCategory("Category 1");
        AddCategory("Category 2");
        AddCategory("Category 3");

        this.GetComponent<UIDocument>().rootVisualElement.Add(visualElement);
    }

    private void AddCategory(string categoryName)
    {
        var categoryLabel = new Label(categoryName);
        categoryLabel.AddToClassList("category-label");
        categoryLabel.RegisterCallback<MouseUpEvent>(evt => ShowItemsForCategory(categoryName));
        categoryContainer.Add(categoryLabel);
    }

    private void ShowItemsForCategory(string categoryName)
    {
        // Clear existing items
        itemContainer.Clear();

        // Simulate fetching items from a database based on the selected category
        // Here you would actually fetch items related to the selected category
        // For demonstration purposes, let's just add some dummy items
        for (int i = 1; i <= 5; i++)
        {
            var itemLabel = new Label($"Item {i} - {categoryName}");
            itemLabel.AddToClassList("item-label");
            itemContainer.Add(itemLabel);
        }
    }
}
