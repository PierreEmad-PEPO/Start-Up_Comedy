using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public static class GameManager
{
    static StartUp startUp;

    // Lists
    static List<Project> projects = new List<Project>();
    static List<Employee> hiringEmployees = new List<Employee>();
    static List<Employee> hiredEmployee = new List<Employee>();
    static Dictionary<MarketingEnum, List<float>> marketingPrice = new Dictionary<MarketingEnum, List<float>> ();
    static List<Loan> loans = new List<Loan>();
    static List<Office> offices = new List<Office>();
    static int groundCount = 1;
    //Props

    public static StartUp StartUp { get { return startUp; } }
    public static List<Project> Projects { get { return projects; } }
    public static List<Employee> HiringEmployees { get { return hiringEmployees; } }
    public static List<Employee> HiredEmployee { get { return hiredEmployee; } }
    public static List<Employee> HiredGamesEmployee { get { return hiredEmployee.Where(g => g.Specialization == EmployeeSpecialization.Games).ToList(); } }
    public static List<Employee> HiredWebEmployee { get { return hiredEmployee.Where(w => w.Specialization == EmployeeSpecialization.Web).ToList(); } }
    public static List<Employee> HiredMobileEmployee { get { return hiredEmployee.Where(m => m.Specialization == EmployeeSpecialization.Mobile).ToList(); } }
    public static List<Employee> HiredMarketingEmployee { get { return hiredEmployee.Where(m => m.Specialization == EmployeeSpecialization.Marketing).ToList(); } }
    public static List<Employee> HiredDataAnalysisEmployee { get { return hiredEmployee.Where(g => g.Specialization == EmployeeSpecialization.DataAnalysis).ToList(); } }
    public static List<Employee> HiredHrEmployee { get { return hiredEmployee.Where(g => g.Specialization == EmployeeSpecialization.HR).ToList(); } }
    public static List<Loan> Loans { get { return loans; } }
    public static List<Office> Offices { get { return offices; } }
    public static int TotalHrEmlpoyeesSkills { get { return GetTotalHrEmlpoyeesSkills(); } }
    public static int TotalMarkeintingSkills { get { return GetTotalMarkeitingEmployeesSkills(); } }

    public static int GroundCount { get { return groundCount; } set { groundCount += 1;} }

    //Methodes

    public static void Init(StartUp _startUp)
    {
        startUp = _startUp;
        InitMarketing();
    }

    private static void InitMarketing()
    {
        // 50 and .5 for now
        marketingPrice.Add(MarketingEnum.SocialAD, new List<float>{ 1500f,50f});
        marketingPrice.Add(MarketingEnum.TVAD, new List<float> { 5000f, 150f });
        marketingPrice.Add(MarketingEnum.RadoiAd, new List<float> { 1000f, 30f });
    }
    public static int GetMarketingPrice(MarketingEnum name)
    {
        return (int) marketingPrice[name][0];
    }

    public static float GetMarketingEffect(MarketingEnum name)
    {
        return marketingPrice[name][1] + startUp.TotalMarketingSkills;
    }
    public static List<Employee> GetAssignedEmployees(Project project)
    {
        return hiredEmployee.Where(e => e is ProjectEmployee && (e as ProjectEmployee).AssignedProject == project).ToList();
    }

    public static List<Employee> GetUnAssignedEmployees(Project project)
    {
        return hiredEmployee.Where(e => e is ProjectEmployee && (int)(e as ProjectEmployee).Specialization == (int)project.Specialization && (e as ProjectEmployee).AssignedProject == null && e.IsSet).ToList();
    }

    public static List<Employee> GetWaitingEmployees()
    {
        return hiredEmployee.Where(e => !e.IsSet).ToList();
    }

    public static Office GetOffice(int id)
    {
        return offices.Where(e => e.ID == id).First();
    }

    // Privet Methodes
    private static int GetTotalHrEmlpoyeesSkills()
    {
        int total = 0;
        foreach (HrEmployee Hr in HiredHrEmployee)
            total += Hr.HrSkill;
        return total;
    }

    private static int GetTotalMarkeitingEmployeesSkills()
    {
        int total = 0;
        foreach (MarketingEmployee marketingEmployee in HiredMarketingEmployee)
            total += marketingEmployee.MarketingSkill;
        return total;
    }

    public static float GetTechSkillsAverage(List<Employee> employees)
    {
        float sum = 0;
        foreach (ProjectEmployee employee in employees)
        {
            sum += employee.TechicalSkills;
        }
        if (sum == 0) return 0;
        return sum / employees.Count;
    }

    public static float GetDesignSkillsAverage(List<Employee> employees)
    {
        float sum = 0;
        foreach (ProjectEmployee employee in employees)
        {
            sum += employee.DesignSkills;
        }
        if (sum == 0) return 0;
        return sum / employees.Count;
    }


}