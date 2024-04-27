using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class OfficeWindo : MonoBehaviour
{
    [SerializeField] VisualTreeAsset waitingCard;
    [SerializeField] PlacementSystem placement;
    [SerializeField] GameObject[] employeeModles;
    VisualElement root;
    VisualElement data;
    VisualElement waiting;
    Label employeeName;
    ListView watingEmployees;
    Employee employee;
    List<Employee> employees;
    GameObject office;

    // Start is called before the first frame update
    void Start()
    {
        SetVisualElement();
        EventManager.AddEmployeeEventListener(EventEnum.OnEmployeeFired, DestoyCharacter);
    }

    void SetVisualElement()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        data = root.Q<VisualElement>("Data");
        waiting = root.Q<VisualElement>("Waiting");
        employeeName = root.Q<Label>("Name");
        watingEmployees = root.Q<ListView>("EmployeeList");
        InitWaitingList();
        root.Q<Button>("Replace").clicked+= () => 
        {
            root.style.display = DisplayStyle.None;
            placement.AssignActiveObject(office);
        };

        root.Q<Button>("Exit").clicked += () => { root.style.display = DisplayStyle.None; };

        root.style.display = DisplayStyle.None;
    }

    void InitWaitingList()
    {
        watingEmployees.makeItem = () =>
        {

            var temp = waitingCard.Instantiate();
            temp.RegisterCallback<ClickEvent>(e => {
                office.GetComponent<Office>().setEmployee(temp.userData as Employee);
                GameObject model = Instantiate(employeeModles[0]);
                model.transform.parent = office.transform.GetChild(0);
                model.transform.localPosition = Vector3.zero;
                model.transform.localRotation = Quaternion.identity;
                model.transform.localScale = Vector3.one;
                ViewData(temp.userData as Employee,office);
            });
            return temp;

        };

        watingEmployees.bindItem = (item, index) =>
        {
            item.userData = employees[index];
            item.Q<Label>("Name").text = employees[index].Name;
            Debug.Log(employees[0].Name);
        };
        watingEmployees.fixedItemHeight = 110;
        watingEmployees.itemsSource = employees;

    }

    public void ViewData(Employee _employee, GameObject office) 
    {
        employee = _employee;
        employeeName.text = _employee.Name;
        this.office = office;
        data.style.display = DisplayStyle.Flex;
        waiting.style.display = DisplayStyle.None;
    }

    public void ViewLWaitingList(GameObject office)
    {
        this.office = office;
        employees = GameManager.GetWaitingEmployees();
        watingEmployees.itemsSource = employees;
        data.style.display = DisplayStyle.None;
        waiting.style.display = DisplayStyle.Flex;
        watingEmployees.Rebuild();
    }

    public void DestoyCharacter(Employee employee)
    {
        if (employee.IsSet)
        {
            GameManager.GetOffice(employee.SetID).UnsetEmployee();
        }
    }
}
