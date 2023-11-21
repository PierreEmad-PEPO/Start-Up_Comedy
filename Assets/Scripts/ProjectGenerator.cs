using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;

public class ProjectGenerator : MonoBehaviour
{
    private Timer timer;
    private GameObjectEventInvoker onProjectGenerated;


    // Start is called before the first frame update
    void Start()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Init(CalculateNewDuration(), GenerateProject);
        timer.Run();

        onProjectGenerated = gameObject.AddComponent<GameObjectEventInvoker>();
        EventManager.AddGameObjectEventInvoker(EventEnum.OnProjectGenerated, onProjectGenerated);
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

        GameObject project = new GameObject("Project");
        project.AddComponent<Project>().Init(name, specialization, deadline, price,
        penalClause, requiredTechnicalSkills, requiredDesignSkills);

        onProjectGenerated.Invoke(project);

        timer.Duration = CalculateNewDuration();
        timer.Run();
    }

    public void AddOnProjectGeneratedListener(UnityAction<GameObject> unityAction)
    {
        onProjectGenerated.AddListener(unityAction);
    }
}
