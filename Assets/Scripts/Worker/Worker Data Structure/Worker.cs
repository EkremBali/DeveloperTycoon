using UnityEngine;

public class Worker
{
    public WorkerProperties workerProperties = new WorkerProperties();
    static string[] names = { "Ekrem", "Erol", "Özlem", "Esma", "Hüseyin" };
    static string[] degrees = { "Master's Degree", "Bachelor's Degree", "Associate Degree", "Zero Developer"};
    static string[] specializations = { "Front End", "Back End", "Game Development", "Mobile Development", "General Programming", "Certificated Engineer" };


    //Generates workers with random properties.
    public void CreateProperties(int id)
    {
        workerProperties.Name = names[Random.Range(0, names.Length)];
        workerProperties.Degree = degrees[Random.Range(0, degrees.Length)];
        workerProperties.id = id;

        if(workerProperties.Degree.Equals("Master's Degree"))
        {
            workerProperties.Specialization = specializations[Random.Range(0, specializations.Length)];
        }
        else
        {
            workerProperties.Specialization = "Not Have";
        }


        if (workerProperties.Degree.Equals("Master's Degree"))
        {
            workerProperties.Speed = 4;
            workerProperties.ErrorRate = 2;

            if (workerProperties.Specialization.Equals("Front End"))
            {
                workerProperties.Salary = Random.Range(15000, 20000);
                workerProperties.frontEnd.HTML = 100;
                workerProperties.frontEnd.CSS = 100;
                workerProperties.frontEnd.JS = 100;
                workerProperties.frontEnd.ReactJs = 100;
                workerProperties.frontEnd.Angular = 100;
            }
            else
            {
                workerProperties.frontEnd.HTML = Random.Range(20,70);
                workerProperties.frontEnd.CSS = Random.Range(20, 70); ;
                workerProperties.frontEnd.JS = Random.Range(20, 70); ;
                workerProperties.frontEnd.ReactJs = Random.Range(20, 70); ;
                workerProperties.frontEnd.Angular = Random.Range(20, 70); ;
            }

            if (workerProperties.Specialization.Equals("Back End"))
            {
                workerProperties.Salary = Random.Range(17000, 20000);
                workerProperties.backEnd.Django = 100;
                workerProperties.backEnd.PHP = 100;
                workerProperties.backEnd.DotNet = 100;
                workerProperties.backEnd.MySQL = 100;
                workerProperties.backEnd.MsSQL = 100;
            }
            else
            {
                workerProperties.backEnd.Django = Random.Range(20, 70); ;
                workerProperties.backEnd.PHP = Random.Range(20, 70); ;
                workerProperties.backEnd.DotNet = Random.Range(20, 70); ;
                workerProperties.backEnd.MySQL = Random.Range(20, 70); ;
                workerProperties.backEnd.MsSQL = Random.Range(20, 70); ;
            }

            if (workerProperties.Specialization.Equals("Game Development"))
            {
                workerProperties.Salary = Random.Range(17000, 25000);
                workerProperties.gameDeveloper.UI_Sprite = 100;
                workerProperties.gameDeveloper.ThreeD_Model = 100;
                workerProperties.gameDeveloper.Unity = 100;
                workerProperties.gameDeveloper.Unreal = 100;
            }
            else
            {
                workerProperties.gameDeveloper.UI_Sprite = Random.Range(20, 70); ;
                workerProperties.gameDeveloper.ThreeD_Model = Random.Range(20, 70); ;
                workerProperties.gameDeveloper.Unity = Random.Range(20, 70); ;
                workerProperties.gameDeveloper.Unreal = Random.Range(20, 70); ;
            }

            if (workerProperties.Specialization.Equals("Mobile Development"))
            {
                workerProperties.Salary = Random.Range(16000, 21000);
                workerProperties.mobileDeveloper.Android_Stuido = 100;
                workerProperties.mobileDeveloper.XCode_Swift = 100;
                workerProperties.mobileDeveloper.React_Native = 100;
                workerProperties.mobileDeveloper.Flutter = 100;
            }
            else
            {
                workerProperties.mobileDeveloper.Android_Stuido = Random.Range(20, 70); ;
                workerProperties.mobileDeveloper.XCode_Swift = Random.Range(20, 70); ;
                workerProperties.mobileDeveloper.React_Native = Random.Range(20, 70); ;
                workerProperties.mobileDeveloper.Flutter = Random.Range(20, 70); ;
            }

            if (workerProperties.Specialization.Equals("General Programming"))
            {
                workerProperties.Salary = Random.Range(20000, 30000);
                workerProperties.generalDeveloper.Img_P = 100;
                workerProperties.generalDeveloper.Deep_L = 100;
                workerProperties.generalDeveloper.Pic_Arduino = 100;
                workerProperties.generalDeveloper.Desktop_App = 100;
            }
            else
            {
                workerProperties.generalDeveloper.Img_P = Random.Range(20, 70); ;
                workerProperties.generalDeveloper.Deep_L = Random.Range(20, 70); ;
                workerProperties.generalDeveloper.Pic_Arduino = Random.Range(20, 70); ;
                workerProperties.generalDeveloper.Desktop_App = Random.Range(20, 70); ;
            }

            if (workerProperties.Specialization.Equals("Certificated Engineer"))
            {
                workerProperties.Salary = Random.Range(26000, 40000);
                workerProperties.certificatedEngineer.DL_AI_Architecture = 100;
                workerProperties.certificatedEngineer.OS = 100;
                workerProperties.certificatedEngineer.PL = 100;
                workerProperties.certificatedEngineer.Game_Engine = 100;
            }
            else
            {
                workerProperties.certificatedEngineer.DL_AI_Architecture = Random.Range(20, 70); ;
                workerProperties.certificatedEngineer.OS = Random.Range(20, 70); ;
                workerProperties.certificatedEngineer.PL = Random.Range(20, 70); ;
                workerProperties.certificatedEngineer.Game_Engine = Random.Range(20, 70); ;
            }
        }
        else if (workerProperties.Degree.Equals("Bachelor's Degree"))
        {
            workerProperties.Speed = 3;
            workerProperties.ErrorRate = 3;
            workerProperties.Salary = Random.Range(10000, 20000);

            workerProperties.frontEnd.HTML = Random.Range(30, 70); ;
            workerProperties.frontEnd.CSS = Random.Range(30, 70); ;
            workerProperties.frontEnd.JS = Random.Range(30, 70); ;
            workerProperties.frontEnd.ReactJs = Random.Range(30, 70); ;
            workerProperties.frontEnd.Angular = Random.Range(30, 70); ;

            workerProperties.backEnd.Django = Random.Range(30, 70); ;
            workerProperties.backEnd.PHP = Random.Range(30, 70); ;
            workerProperties.backEnd.DotNet = Random.Range(30, 70); ;
            workerProperties.backEnd.MySQL = Random.Range(30, 70); ;
            workerProperties.backEnd.MsSQL = Random.Range(30, 70); ;

            workerProperties.gameDeveloper.UI_Sprite = Random.Range(30, 70); ;
            workerProperties.gameDeveloper.ThreeD_Model = Random.Range(30, 70); ;
            workerProperties.gameDeveloper.Unity = Random.Range(30, 70); ;
            workerProperties.gameDeveloper.Unreal = Random.Range(30, 70); ;

            workerProperties.mobileDeveloper.Android_Stuido = Random.Range(30, 70); ;
            workerProperties.mobileDeveloper.XCode_Swift = Random.Range(30, 70); ;
            workerProperties.mobileDeveloper.React_Native = Random.Range(30, 70); ;
            workerProperties.mobileDeveloper.Flutter = Random.Range(30, 70); ;

            workerProperties.generalDeveloper.Img_P = Random.Range(20, 50); ;
            workerProperties.generalDeveloper.Deep_L = Random.Range(20, 50); ;
            workerProperties.generalDeveloper.Pic_Arduino = Random.Range(20, 50); ;
            workerProperties.generalDeveloper.Desktop_App = Random.Range(20, 50); ;

            workerProperties.certificatedEngineer.DL_AI_Architecture = Random.Range(20, 40); ;
            workerProperties.certificatedEngineer.OS = Random.Range(20, 40);
            workerProperties.certificatedEngineer.PL = Random.Range(20, 40); ;
            workerProperties.certificatedEngineer.Game_Engine = Random.Range(20, 40); ;
        }
        else if (workerProperties.Degree.Equals("Associate Degree"))
        {
            workerProperties.Speed = 2;
            workerProperties.ErrorRate = 4;

            workerProperties.Salary = Random.Range(5000, 9000);

            workerProperties.frontEnd.HTML = Random.Range(20, 50);
            workerProperties.frontEnd.CSS = Random.Range(20, 50);
            workerProperties.frontEnd.JS = Random.Range(20, 50);
            workerProperties.frontEnd.ReactJs = Random.Range(20, 50);
            workerProperties.frontEnd.Angular = Random.Range(20, 50);

            workerProperties.backEnd.Django = Random.Range(20, 50);
            workerProperties.backEnd.PHP = Random.Range(20, 50);
            workerProperties.backEnd.DotNet = Random.Range(20, 50);
            workerProperties.backEnd.MySQL = Random.Range(20, 50);
            workerProperties.backEnd.MsSQL = Random.Range(20, 50);

            workerProperties.gameDeveloper.UI_Sprite = Random.Range(20, 30);
            workerProperties.gameDeveloper.ThreeD_Model = Random.Range(20, 30);
            workerProperties.gameDeveloper.Unity = Random.Range(20, 30);
            workerProperties.gameDeveloper.Unreal = Random.Range(20, 30);

            workerProperties.mobileDeveloper.Android_Stuido = Random.Range(20, 30);
            workerProperties.mobileDeveloper.XCode_Swift = Random.Range(20, 30);
            workerProperties.mobileDeveloper.React_Native = Random.Range(20, 30);
            workerProperties.mobileDeveloper.Flutter = Random.Range(20, 30);

            workerProperties.generalDeveloper.Img_P = Random.Range(0, 20);
            workerProperties.generalDeveloper.Deep_L = Random.Range(0, 20);
            workerProperties.generalDeveloper.Pic_Arduino = Random.Range(0, 20);
            workerProperties.generalDeveloper.Desktop_App = Random.Range(0, 20);

            workerProperties.certificatedEngineer.DL_AI_Architecture = 0;
            workerProperties.certificatedEngineer.OS = 0;
            workerProperties.certificatedEngineer.PL =0;
            workerProperties.certificatedEngineer.Game_Engine = 0;
        }
        else
        {
            workerProperties.Speed = 1;
            workerProperties.ErrorRate = 5;
            workerProperties.Salary = Random.Range(3000, 5000);

            workerProperties.frontEnd.HTML = Random.Range(0, 20);
            workerProperties.frontEnd.CSS = Random.Range(0, 20);
            workerProperties.frontEnd.JS = Random.Range(0, 20);
            workerProperties.frontEnd.ReactJs = Random.Range(0, 20);
            workerProperties.frontEnd.Angular = Random.Range(0, 20);

            workerProperties.backEnd.Django = Random.Range(0, 20);
            workerProperties.backEnd.PHP = Random.Range(0, 20);
            workerProperties.backEnd.DotNet = Random.Range(0, 20);
            workerProperties.backEnd.MySQL = Random.Range(0, 20);
            workerProperties.backEnd.MsSQL = Random.Range(0, 20);

            workerProperties.gameDeveloper.UI_Sprite = Random.Range(0, 20);
            workerProperties.gameDeveloper.ThreeD_Model = Random.Range(0, 20);
            workerProperties.gameDeveloper.Unity = Random.Range(0, 20);
            workerProperties.gameDeveloper.Unreal = Random.Range(0, 20);

            workerProperties.mobileDeveloper.Android_Stuido = Random.Range(0, 20);
            workerProperties.mobileDeveloper.XCode_Swift = Random.Range(0, 20);
            workerProperties.mobileDeveloper.React_Native = Random.Range(0, 20);
            workerProperties.mobileDeveloper.Flutter = Random.Range(0, 20);

            workerProperties.generalDeveloper.Img_P = 0;
            workerProperties.generalDeveloper.Deep_L = 0;
            workerProperties.generalDeveloper.Pic_Arduino = 0;
            workerProperties.generalDeveloper.Desktop_App = 0;

            workerProperties.certificatedEngineer.DL_AI_Architecture = 0;
            workerProperties.certificatedEngineer.OS = 0;
            workerProperties.certificatedEngineer.PL = 0;
            workerProperties.certificatedEngineer.Game_Engine = 0;
        }

    }
}
