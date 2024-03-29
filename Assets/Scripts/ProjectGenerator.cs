using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectGenerator : MonoBehaviour
{
    private Timer timer;
    private ProjectEventInvoker onProjectGenerated;

    public Timer ProjectTimer { get { return timer; } }
    // Start is called before the first frame update
    private void Awake()
    {
        timer = gameObject.AddComponent<Timer>();
        timer.Init(CalculateNewDuration(), GenerateProject);
    }
    void Start()
    {
       
        timer.Run();

        onProjectGenerated = gameObject.AddComponent<ProjectEventInvoker>();
        EventManager.AddProjectEventInvoker(EventEnum.OnProjectGenerated, onProjectGenerated);
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
        int deadline = 300;
        int price = 50;
        int penalClause = 50;
        int requiredTechnicalSkills = 5000;
        int requiredDesignSkills = 5000;

        Project project = new Project();
        project.Init(name, specialization, deadline, price,
        penalClause, requiredTechnicalSkills, requiredDesignSkills);

        onProjectGenerated.Invoke(project);
        

        timer.Duration = CalculateNewDuration();
        timer.Pause();
    }
}
