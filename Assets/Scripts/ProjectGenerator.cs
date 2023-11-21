using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class ProjectGenerator : MonoBehaviour
{
    private Timer timer;
    private UnityEvent<Project> OnProjectGenerated;


    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Init(CalculateNewDuration(), GenerateProject);
        timer.Run();

        OnProjectGenerated = new UnityEvent<Project>();
    }

    private float CalculateNewDuration()
    {
        // Based on Popularity
        return 5f; // For new
    }

    private void GenerateProject()
    {
        string name = "lol";
        ProjectSpecialization specialization = ProjectSpecialization.Games;
        float deadline = 50;
        int price = 50;
        int penalClause = 50;
        float requiredTechnicalSkills = 50;
        float requiredDesignSkills = 50;

        Project project = new Project();
        project.Init(name, specialization, deadline,
        price, penalClause, requiredTechnicalSkills, requiredDesignSkills);

        OnProjectGenerated.Invoke(project);

        timer.Duration = CalculateNewDuration();
        timer.Run();
    }

    public void AddOnProjectGeneratedListener(UnityAction<Project> unityAction)
    {
        OnProjectGenerated.AddListener(unityAction);
    }
}
