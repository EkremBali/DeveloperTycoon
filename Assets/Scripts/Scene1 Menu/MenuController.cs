using System.IO;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour
{
    //Menu Panels
    public GameObject FirstPanel;
    public GameObject SecondPanel;
    public GameObject ThirdPanel;

    //Second Menu Inputs
    public TextMeshProUGUI nameInput;
    public TextMeshProUGUI degreeInput;
    public TextMeshProUGUI specializationInput;
    public GameObject SpecializatedParent;

    //Panel 3 Character Stats Texts
    public TextMeshProUGUI panel3NameText;
    public TextMeshProUGUI panel3SpeedText;
    public TextMeshProUGUI panel3ErrorRateText;
    public TextMeshProUGUI[] panel3FrontEnd;
    public TextMeshProUGUI[] panel3BackEnd;
    public TextMeshProUGUI[] panel3GameDev;
    public TextMeshProUGUI[] panel3MobileDev;
    public TextMeshProUGUI[] panel3GeneralDev;
    public TextMeshProUGUI[] panel3Engineer;

    //Load Game Panels
    public GameObject LoadGameSlots;


    //Player Properties
    public string Name { get; private set; }
    public string Degree { get; private set; }
    public string Specialization { get; private set; }
    public string Speed { get; private set; }
    public string ErrorRate { get; private set; }


    //First Menu ------------------------------------------------------------------------------------------------------------------------------------------------------------------
    public void StartGame()
    {
        FirstPanel.SetActive(false);
        SecondPanel.SetActive(true);
    }

    public void LoadGame()
    {
        LoadGameSlots.SetActive(!LoadGameSlots.activeSelf);
    }

    public void Settings()
    {

    }

    public void ExitGame()
    {
        #if UNITY_EDITOR
            EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    //Second Menu -----------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Called every time the degree changes
    public void ChangeOnDegree()
    {
        if (degreeInput.text.Equals("Master's Degree"))
        {
            SpecializatedParent.SetActive(true);
        }
        else
        {
            SpecializatedParent.SetActive(false);   
        }
    }

    //Called every time Click Next Button
    public void Next()
    {
        Name = nameInput.text;
        Degree = degreeInput.text;
        Specialization = specializationInput.text;

        //foreach (var chr in Name) { Debug.Log(System.Convert.ToInt32(chr)); }

        //IF name input is null
        if (Name.Length != 1)
        {
            SecondPanel.SetActive(false);
            ThirdPanel.SetActive(true);

            WorkerProperties player = new WorkerProperties();

            //Create Player Abilities
            if (Degree.Equals("Master's Degree"))
            {
                CreateAbilities(player, 1);
            }
            else if (Degree.Equals("Bachelor's Degree"))
            {
                CreateAbilities(player, 2);
            }
            else if (Degree.Equals("Associate Degree "))
            {
                CreateAbilities(player, 3);
            }
            else
            {
                CreateAbilities(player, 4);
            }

            //Save Player Abilities
            SavePlayerAbilities(player);

            //Load Player Abilities to Panel 3
            LoadAbilities();
        }
        else
        {
            Debug.Log("Lütfen bir isim giriniz.");
        }
        

    }

    //Create Player Abilities
    void CreateAbilities(WorkerProperties player, int abilityLevel)
    {
        player.Name = Name;
        player.Degree = Degree;
        player.Specialization = Specialization;
        player.Salary = 0;

        if(abilityLevel == 1)
        {
            player.Speed = 4;
            player.ErrorRate = 2;

            if (player.Specialization.Equals("Front End"))
            {
                player.frontEnd.HTML = 100;
                player.frontEnd.CSS = 100;
                player.frontEnd.JS = 100;
                player.frontEnd.ReactJs = 100;
                player.frontEnd.Angular = 100;
            }
            else
            {
                player.frontEnd.HTML = 60;
                player.frontEnd.CSS = 60;
                player.frontEnd.JS = 60;
                player.frontEnd.ReactJs = 30;
                player.frontEnd.Angular = 60;
            }

            if (player.Specialization.Equals("Back End"))
            {
                player.backEnd.Django = 100;
                player.backEnd.PHP = 100;
                player.backEnd.DotNet = 100;
                player.backEnd.MySQL = 100;
                player.backEnd.MsSQL = 100;
            }
            else
            {
                player.backEnd.Django = 30;
                player.backEnd.PHP = 60;
                player.backEnd.DotNet = 30;
                player.backEnd.MySQL = 60;
                player.backEnd.MsSQL = 30;
            }

            if (player.Specialization.Equals("Game Development"))
            {
                player.gameDeveloper.UI_Sprite = 100;
                player.gameDeveloper.ThreeD_Model = 100;
                player.gameDeveloper.Unity = 100;
                player.gameDeveloper.Unreal = 100;
            }
            else
            {
                player.gameDeveloper.UI_Sprite = 30;
                player.gameDeveloper.ThreeD_Model = 30;
                player.gameDeveloper.Unity = 60;
                player.gameDeveloper.Unreal = 60;
            }

            if (player.Specialization.Equals("Mobile Development"))
            {
                player.mobileDeveloper.Android_Stuido = 100;
                player.mobileDeveloper.XCode_Swift = 100;
                player.mobileDeveloper.React_Native = 100;
                player.mobileDeveloper.Flutter = 100;
            }
            else
            {
                player.mobileDeveloper.Android_Stuido = 60;
                player.mobileDeveloper.XCode_Swift = 60;
                player.mobileDeveloper.React_Native = 60;
                player.mobileDeveloper.Flutter = 30;
            }

            if (player.Specialization.Equals("General Programming"))
            {
                player.generalDeveloper.Img_P = 100;
                player.generalDeveloper.Deep_L = 100;
                player.generalDeveloper.Pic_Arduino = 100;
                player.generalDeveloper.Desktop_App = 100;
            }
            else
            {
                player.generalDeveloper.Img_P = 50;
                player.generalDeveloper.Deep_L = 50;
                player.generalDeveloper.Pic_Arduino = 50;
                player.generalDeveloper.Desktop_App = 50;
            }

            if (player.Specialization.Equals("Certificated Engineer"))
            {
                player.certificatedEngineer.DL_AI_Architecture = 100;
                player.certificatedEngineer.OS = 100;
                player.certificatedEngineer.PL = 100;
                player.certificatedEngineer.Game_Engine = 100;
            }
            else
            {
                player.certificatedEngineer.DL_AI_Architecture = 50;
                player.certificatedEngineer.OS = 50;
                player.certificatedEngineer.PL = 50;
                player.certificatedEngineer.Game_Engine = 50;
            }
        }
        else if(abilityLevel == 2)
        {
            player.Speed = 3;
            player.ErrorRate = 3;

            player.frontEnd.HTML = 50;
            player.frontEnd.CSS = 50;
            player.frontEnd.JS = 50;
            player.frontEnd.ReactJs = 20;
            player.frontEnd.Angular = 50;

            player.backEnd.Django = 20;
            player.backEnd.PHP = 50;
            player.backEnd.DotNet = 20;
            player.backEnd.MySQL = 50;
            player.backEnd.MsSQL = 20;

            player.gameDeveloper.UI_Sprite = 20;
            player.gameDeveloper.ThreeD_Model = 40;
            player.gameDeveloper.Unity = 40;
            player.gameDeveloper.Unreal = 20;

            player.mobileDeveloper.Android_Stuido = 40;
            player.mobileDeveloper.XCode_Swift = 40;
            player.mobileDeveloper.React_Native = 20;
            player.mobileDeveloper.Flutter = 20;

            player.generalDeveloper.Img_P = 40;
            player.generalDeveloper.Deep_L = 40;
            player.generalDeveloper.Pic_Arduino = 40;
            player.generalDeveloper.Desktop_App = 40;

            player.certificatedEngineer.DL_AI_Architecture = 35;
            player.certificatedEngineer.OS = 35;
            player.certificatedEngineer.PL = 35;
            player.certificatedEngineer.Game_Engine = 35;
        }
        else if (abilityLevel == 3)
        {
            player.Speed = 2;
            player.ErrorRate = 4;

            player.frontEnd.HTML = 30;
            player.frontEnd.CSS = 30;
            player.frontEnd.JS = 30;
            player.frontEnd.ReactJs = 0;
            player.frontEnd.Angular = 30;

            player.backEnd.Django = 0;
            player.backEnd.PHP = 30;
            player.backEnd.DotNet = 0;
            player.backEnd.MySQL = 30;
            player.backEnd.MsSQL = 0;

            player.gameDeveloper.UI_Sprite = 0;
            player.gameDeveloper.ThreeD_Model = 0;
            player.gameDeveloper.Unity = 10;
            player.gameDeveloper.Unreal = 0;

            player.mobileDeveloper.Android_Stuido = 10;
            player.mobileDeveloper.XCode_Swift = 10;
            player.mobileDeveloper.React_Native = 0;
            player.mobileDeveloper.Flutter = 0;

            player.generalDeveloper.Img_P = 0;
            player.generalDeveloper.Deep_L = 0;
            player.generalDeveloper.Pic_Arduino = 0;
            player.generalDeveloper.Desktop_App = 10;

            player.certificatedEngineer.DL_AI_Architecture = 0;
            player.certificatedEngineer.OS = 0;
            player.certificatedEngineer.PL = 0;
            player.certificatedEngineer.Game_Engine = 0;
        }
        else
        {
            player.Speed = 1;
            player.ErrorRate = 5;

            player.frontEnd.HTML = 10;
            player.frontEnd.CSS = 10;
            player.frontEnd.JS = 10;
            player.frontEnd.ReactJs = 0;
            player.frontEnd.Angular = 0;

            player.backEnd.Django = 0;
            player.backEnd.PHP = 10;
            player.backEnd.DotNet = 0;
            player.backEnd.MySQL = 10;
            player.backEnd.MsSQL = 0;

            player.gameDeveloper.UI_Sprite = 0;
            player.gameDeveloper.ThreeD_Model = 0;
            player.gameDeveloper.Unity = 0;
            player.gameDeveloper.Unreal = 0;

            player.mobileDeveloper.Android_Stuido = 10;
            player.mobileDeveloper.XCode_Swift = 0;
            player.mobileDeveloper.React_Native = 0;
            player.mobileDeveloper.Flutter = 0;

            player.generalDeveloper.Img_P = 0;
            player.generalDeveloper.Deep_L = 0;
            player.generalDeveloper.Pic_Arduino = 0;
            player.generalDeveloper.Desktop_App = 0;

            player.certificatedEngineer.DL_AI_Architecture = 0;
            player.certificatedEngineer.OS = 0;
            player.certificatedEngineer.PL = 0;
            player.certificatedEngineer.Game_Engine = 0;
        }

        //With this object, the player properties are carried to the next scene.
        GameObject.Find("Player Properties").GetComponent<PlayerProperties>().player = player;
    }

    //Save Player Abilities
    public void SavePlayerAbilities(WorkerProperties player)
    {
        string path = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/playerProperties.json";
        string json = JsonUtility.ToJson(player);
        File.WriteAllText(path, json);

    }

    //Load Abilities
    void LoadAbilities()
    {
        string path = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/playerProperties.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            WorkerProperties wpData = JsonUtility.FromJson<WorkerProperties>(json);

            panel3NameText.text = wpData.Name;
            panel3SpeedText.text = "Speed: " + wpData.Speed;
            panel3ErrorRateText.text = "Error Rate: " + wpData.ErrorRate;

            panel3FrontEnd[0].text = panel3FrontEnd[0].text.Split(":")[0] + " : " + wpData.frontEnd.HTML;
            panel3FrontEnd[1].text = panel3FrontEnd[1].text.Split(":")[0] + " : " + wpData.frontEnd.CSS;
            panel3FrontEnd[2].text = panel3FrontEnd[2].text.Split(":")[0] + " : " + wpData.frontEnd.JS;
            panel3FrontEnd[3].text = panel3FrontEnd[3].text.Split(":")[0] + " : " + wpData.frontEnd.Angular;
            panel3FrontEnd[4].text = panel3FrontEnd[4].text.Split(":")[0] + " : " + wpData.frontEnd.ReactJs;

            panel3BackEnd[0].text = panel3BackEnd[0].text.Split(":")[0] + " : " + wpData.backEnd.MySQL;
            panel3BackEnd[1].text = panel3BackEnd[1].text.Split(":")[0] + " : " + wpData.backEnd.MsSQL;
            panel3BackEnd[2].text = panel3BackEnd[2].text.Split(":")[0] + " : " + wpData.backEnd.PHP;
            panel3BackEnd[3].text = panel3BackEnd[3].text.Split(":")[0] + " : " + wpData.backEnd.DotNet;
            panel3BackEnd[4].text = panel3BackEnd[4].text.Split(":")[0] + " : " + wpData.backEnd.Django;

            panel3GameDev[0].text = panel3GameDev[0].text.Split(":")[0] + " : " + wpData.gameDeveloper.UI_Sprite;
            panel3GameDev[1].text = panel3GameDev[1].text.Split(":")[0] + " : " + wpData.gameDeveloper.ThreeD_Model;
            panel3GameDev[2].text = panel3GameDev[2].text.Split(":")[0] + " : " + wpData.gameDeveloper.Unity;
            panel3GameDev[3].text = panel3GameDev[3].text.Split(":")[0] + " : " + wpData.gameDeveloper.Unreal;

            panel3MobileDev[0].text = panel3MobileDev[0].text.Split(":")[0] + " : " + wpData.mobileDeveloper.Android_Stuido;
            panel3MobileDev[1].text = panel3MobileDev[1].text.Split(":")[0] + " : " + wpData.mobileDeveloper.XCode_Swift;
            panel3MobileDev[2].text = panel3MobileDev[2].text.Split(":")[0] + " : " + wpData.mobileDeveloper.React_Native;
            panel3MobileDev[3].text = panel3MobileDev[3].text.Split(":")[0] + " : " + wpData.mobileDeveloper.Flutter;

            panel3GeneralDev[0].text = panel3GeneralDev[0].text.Split(":")[0] + " : " + wpData.generalDeveloper.Img_P;
            panel3GeneralDev[1].text = panel3GeneralDev[1].text.Split(":")[0] + " : " + wpData.generalDeveloper.Deep_L;
            panel3GeneralDev[2].text = panel3GeneralDev[2].text.Split(":")[0] + " : " + wpData.generalDeveloper.Pic_Arduino;
            panel3GeneralDev[3].text = panel3GeneralDev[3].text.Split(":")[0] + " : " + wpData.generalDeveloper.Desktop_App;

            panel3Engineer[0].text = panel3Engineer[0].text.Split(":")[0] + " : " + wpData.certificatedEngineer.DL_AI_Architecture;
            panel3Engineer[1].text = panel3Engineer[1].text.Split(":")[0] + " : " + wpData.certificatedEngineer.OS;
            panel3Engineer[2].text = panel3Engineer[2].text.Split(":")[0] + " : " + wpData.certificatedEngineer.PL;
            panel3Engineer[3].text = panel3Engineer[3].text.Split(":")[0] + " : " + wpData.certificatedEngineer.Game_Engine;
        }
    }


    //Third Menu-------------------------------------------------------------------------------------------------------------------------------------------------------------------

    //Back Button: Go to Second Panel
    public void BackToChoose()
    {
        SecondPanel.SetActive(true);
        ThirdPanel.SetActive(false);
    }


    //Start Button: Go to New Game
    public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

}
