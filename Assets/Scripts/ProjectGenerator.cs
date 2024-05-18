using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectGenerator : MonoBehaviour
{
    List<List<string>> projectNames = new List<List<string>>
    { 
    // Games
    new List<string>
        {
            "Nova Blitz Arena",
            "Pixel Quest Legends",
            "Starbound Odyssey",
            "Cyber City Chaos",
            "Elemental Clash Arena",
            "Babi City at Baraka Studio",
            "Ochooooo Game",
            "Caestopia City Game",
            "Save The Forset",
            "GTA El-Zengy",
            "Imbaba War",
            "Sniper Paradise",
            "Crazy House Onigi",
            "IronSight Viper Zeyhro",
            "Overwatch Escort the Payload",
            "PES 6",
            "Football Manager",
            "Metro Simulator",
            "Gartic Drawing",
            "EVE Zimboo Killer",
            "Chess Game",
            "Grandsons of Hitler Game",
            "Pussis Sniper"
        },

    // Web
    new List<string>
        {
            "Market Mojo Hub",
            "Cloud Vault Secure",
            "Social Sphere Connect",
            "E-Commerce Evolution",
            "Event Engine Planner",
            "El-Araby industry",
            "Markaz El-7sab El-3elmy",
            "Cloud Connect",
            "Secure Vault",
            "Health Hub",
            "Smart Scheduler",
            "Market Mapper",
            "Talent Tracker",
            "Faculty Hub",
            "University Hub",
            "XFaculty",
            "Hub El-Hub",
            "Financial Forecast Portal",
            "Customer Engagement Platform",
            "AI Analytics Dashboard",
            "Blockchain Integration Hub",
            "User Experience Toolkit"

        },

    // Mobile
    new List<string>
        {
            "Fitness Fusion Pro",
            "Travel Trekker Pro",
            "Pocket Chef Pro",
            "Finance Friend Pro",
            "Music Mixer Pro",
            "Language Link",
            "Task Tracker",
            "Photo Fusion",
            "Pet Pal",
            "Travel Companion",
            "Health Tracker",
            "Budget Buddy",
            "Music Maker",
            "Recipe Repository",
            "Fitness Fanatic",
            "Learning Lab",
            "Journal Junction",
            "Mindfulness Mentor",
            "Career Coach",
            "Home Helper",
            "Artistic Avenue",
            "Social Synthesis",
            "Memory Maker",
            "Adventure App",
            "Productivity Partner"
        },
    };

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
        EventManager.AddVoidEventListener(EventEnum.OnMarketingDone, UpdateNextProjectTimer);
    }

    private float CalculateNewDuration()
    {
        // Based on Popularity
        int newDur = (GameManager.StartUp.MAX_POPULARITY - GameManager.StartUp.Popularity) * 2;
        if (newDur < 120) newDur = 120;
        if (newDur > 15 * 60) newDur = 15 * 60;
        return newDur;
    }

    private void GenerateProject()
    {
        ProjectSpecialization specialization = (ProjectSpecialization)(RandomGenerator.NextInt(0, 3));
        string name = projectNames[(int)specialization][RandomGenerator.NextInt(0, projectNames[(int)specialization].Count)];
        int deadline = RandomGenerator.NextInt(200, 3650);
        int price = RandomGenerator.NextInt(40, 1505) * 50;
        int penalClause = RandomGenerator.NextInt(20, 1255) * 50;
        int requiredTechnicalSkills = 0;
        int requiredDesignSkills = 0;
        int highValue = 0;
        switch(specialization)
        {
            case ProjectSpecialization.Games:
                requiredTechnicalSkills =
                    (int)(GameManager.GetTechSkillsAverage(GameManager.HiredGamesEmployee) *
                    deadline * GameManager.HiredGamesEmployee.Count * RandomGenerator.NextFloat(.2f, .6f) *
                    RandomGenerator.NextFloat(.3f, 1.7f));
                requiredDesignSkills =
                    (int)(GameManager.GetDesignSkillsAverage(GameManager.HiredGamesEmployee) *
                    deadline * GameManager.HiredGamesEmployee.Count * RandomGenerator.NextFloat(.2f, .6f) *
                    RandomGenerator.NextFloat(.3f, 1.7f));
                highValue = GameManager.HiredGamesEmployee.Count * 200 * deadline;
            break;

            case ProjectSpecialization.Web:
                requiredTechnicalSkills =
                    (int)(GameManager.GetTechSkillsAverage(GameManager.HiredWebEmployee) *
                    deadline * GameManager.HiredWebEmployee.Count * RandomGenerator.NextFloat(.2f, .6f) *
                    RandomGenerator.NextFloat(.3f, 1.7f));
                requiredDesignSkills =
                    (int)(GameManager.GetDesignSkillsAverage(GameManager.HiredWebEmployee) *
                    deadline * GameManager.HiredWebEmployee.Count * RandomGenerator.NextFloat(.2f, .6f) *
                    RandomGenerator.NextFloat(.3f, 1.7f));
                highValue = GameManager.HiredWebEmployee.Count * 200 * deadline;
                break;

            case ProjectSpecialization.Mobile:
                requiredTechnicalSkills =
                    (int)(GameManager.GetTechSkillsAverage(GameManager.HiredMobileEmployee) *
                    deadline * GameManager.HiredMobileEmployee.Count * RandomGenerator.NextFloat(.2f, .6f) *
                    RandomGenerator.NextFloat(.3f, 1.7f));
                requiredDesignSkills =
                    (int)(GameManager.GetDesignSkillsAverage(GameManager.HiredMobileEmployee) *
                    deadline * GameManager.HiredMobileEmployee.Count * RandomGenerator.NextFloat(.2f, .6f) *
                    RandomGenerator.NextFloat(.3f, 1.7f));
                highValue = GameManager.HiredMobileEmployee.Count * 200 * deadline;
                break;
        }

        if (requiredTechnicalSkills == 0)
            requiredTechnicalSkills = 8000;
        if (requiredDesignSkills == 0)
            requiredDesignSkills = 12000;
        if (highValue == 0)
            highValue = 12000;

        Project project = new Project();
        project.Init(name, specialization, deadline, price,
        penalClause, requiredTechnicalSkills, requiredDesignSkills);
        project.highValue = highValue;

        onProjectGenerated.Invoke(project);
        

        timer.Duration = CalculateNewDuration();
        timer.Pause();
    }

    private void UpdateNextProjectTimer()
    {
        float newDur = Mathf.Min(CalculateNewDuration(), timer.Duration/3);
        timer.Duration = newDur;
        Debug.Log(newDur);
    }
}
