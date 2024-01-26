using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void Start()
    {
        EventManager.AddProjectEventListener(EventEnum.OnProjectDone, ProjectDone);
        EventManager.AddProjectEventListener(EventEnum.OnDeadlineEnd, ProjectDead);
    }

    private void ProjectDone(Project project)
    {
        Debug.Log(project.Name + " " + project.RequiredTechnicalSkills + " Done");
    }

    private void ProjectDead(Project project)
    {
        Debug.Log(project.Name + " " + project.RequiredTechnicalSkills + " Dead");
    }
}
