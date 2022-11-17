using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
public class Front_End
{
    public int HTML;
    public int CSS;
    public int JS;
    public int Angular;
    public int ReactJs;
}

[System.Serializable]
public class Back_End
{
    public int MySQL;
    public int MsSQL;
    public int PHP;
    public int DotNet;
    public int Django;
}


[System.Serializable]
public class Game_Dev
{
    public int UI_Sprite;
    public int ThreeD_Model;
    public int Unity;
    public int Unreal;
}


[System.Serializable]
public class Mobile_Dev
{
    public int Android_Stuido;
    public int XCode_Swift;
    public int React_Native;
    public int Flutter;
}


[System.Serializable]
public class General_Dev
{
    public int Img_P;
    public int Deep_L;
    public int Pic_Arduino;
    public int Desktop_App;
}


[System.Serializable]
public class Engineer
{
    public int DL_AI_Architecture;
    public int OS;
    public int PL;
    public int Game_Engine;
}


[System.Serializable]
public class WorkerProperties
{
    public int id;
    public string Name;
    public string Degree;
    public string Specialization;
    public int Speed;
    public int ErrorRate;
    public int Salary;
    public Front_End frontEnd = new Front_End();
    public Back_End backEnd = new Back_End();
    public Game_Dev gameDeveloper = new Game_Dev();
    public Mobile_Dev mobileDeveloper = new Mobile_Dev();
    public General_Dev generalDeveloper = new General_Dev();
    public Engineer certificatedEngineer = new Engineer();

}

