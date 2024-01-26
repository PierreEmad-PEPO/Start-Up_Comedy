using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GamePlayManager 
{
    // Lists
    static List<Project> projects = new List<Project>();

    //Props

    public static List<Project> Projects { get { return projects; } }

    //Methodes
    public static void AddProject (Project project)
    {
        projects.Add(project);
    }
    
}