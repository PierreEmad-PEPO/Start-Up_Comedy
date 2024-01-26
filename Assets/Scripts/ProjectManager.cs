using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ProjectManager : MonoBehaviour
{
    List<Project> projects;
    private ProjectEventInvoker OnProjectDone;
    private ProjectEventInvoker OnDeadlineEnd;

    // Start is called before the first frame update
    void Start()
    {
        projects = GameManager.Projects;

        // Assign Events
        OnProjectDone = gameObject.AddComponent<ProjectEventInvoker>();
        EventManager.AddProjectEventInvoker(EventEnum.OnProjectDone, OnProjectDone);

        OnDeadlineEnd = gameObject.AddComponent<ProjectEventInvoker>();
        EventManager.AddProjectEventInvoker(EventEnum.OnDeadlineEnd, OnDeadlineEnd);

        StartCoroutine(UpdateProjects());
    }

    IEnumerator UpdateProjects()
    {
        while (true)
        {
            for (int i = 0; i < projects.Count; i++) 
            {
                var project = projects[i];
                project.UpdateProject();

                if (project.IsDone) 
                {
                    projects.Remove(project);
                    OnProjectDone.Invoke(project);
                    continue;
                }
                else if (project.IsDeadlineEnd)
                {
                    projects.Remove(project);
                    OnDeadlineEnd.Invoke(project);
                    continue;
                }

                Debug.Log(project.Deadline);
                Debug.Log(project.RequiredDesignSkills + " " + project.RequiredDesignSkills);
            }
            yield return new WaitForSeconds(1);
        }
    }
}
