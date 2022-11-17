using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class WorkerCreatorTimeDatasForSave
{
    public int firstMounth;
    public bool isFirstChange;
}

public class WorkerCreator : MonoBehaviour
{
    public TextMeshProUGUI reputationText;
    int reputation;

    //Contracts are update per 2 mounth;
    //This variables and TimeController for this
    public TimeController timeController;
    public int firstMounth;
    public bool isFirstChange;

    //For Hire Worker panel
    public Transform content;
    public GameObject workerPanelPrefab;

    public List<Worker> workerList;

    private void Start()
    {
        timeController = GameObject.Find("Time Control").GetComponent<TimeController>();

        if (workerList == null)
        {
            workerList = new List<Worker>();

            isFirstChange = false;
            firstMounth = timeController.time.mounth;

            CreateWorkers();
        }
        else
        {
            DisplayWorkersToContent("new");
        }

    }

    private void Update()
    {
        CheckTwoMounth();
    }

    //If 2 mounth pass then create new workers
    void CheckTwoMounth()
    {
        if (timeController.time.mounth != firstMounth)
        {
            firstMounth = timeController.time.mounth;

            if (!isFirstChange)
            {
                isFirstChange = true;
            }
            else
            {
                isFirstChange = false;
                CreateWorkers();
            }
        }
    }

    //Creates a reputation-based number of workers using the Worker class.
    public void CreateWorkers()
    {
        //Remove old workers from list and game uý
        workerList.Clear();
        DestroyContentChilds();

        reputation = int.Parse(reputationText.text.Split(" ")[2]);

        for (int i = 0; i < reputation/2; i++)
        {
            Worker worker = new Worker();
            worker.CreateProperties(i);
            worker.workerProperties.id = i;
            workerList.Add(worker);
        }

        //Display workers from list to Hire Workers Panel in game
        DisplayWorkersToContent("new");
    }


    //Display workers from list to Hire Workers Panel in game
    public void DisplayWorkersToContent(string typeOfSpecialization)
    {

        foreach (var worker in workerList)
        {
            //If contract are newly create then Instantiate this panels
            if (typeOfSpecialization.Equals("new"))
            {
                CreateDisplayContentPanel(worker.workerProperties);
            }
            else
            {
                for (int i = 0; i < content.childCount; i++)
                {
                    content.GetChild(i).gameObject.SetActive(true);
                    string[] SpecialTypeTextParts = content.GetChild(i).GetChild(1).GetComponent<TextMeshProUGUI>().text.Split(" ");
                    string SpecialType = SpecialTypeTextParts[1] + " " + SpecialTypeTextParts[2];

                    if (!SpecialType.Equals(typeOfSpecialization) && !typeOfSpecialization.Equals("all"))
                    {
                        content.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

    public void CreateDisplayContentPanel(WorkerProperties workerProperties)
    {
        int frontEndAvg = 0;
        int backEndAvg = 0;
        int gameAvg = 0;
        int mobileAvg = 0;
        int generalAvg = 0;
        int engineerAvg = 0;

        frontEndAvg = (workerProperties.frontEnd.HTML + workerProperties.frontEnd.CSS + workerProperties.frontEnd.JS
                    + workerProperties.frontEnd.Angular + workerProperties.frontEnd.ReactJs) / 5;

        backEndAvg = (workerProperties.backEnd.MySQL + workerProperties.backEnd.MsSQL + workerProperties.backEnd.PHP
            + workerProperties.backEnd.DotNet + workerProperties.backEnd.Django) / 5;

        gameAvg = (workerProperties.gameDeveloper.UI_Sprite + workerProperties.gameDeveloper.ThreeD_Model
            + workerProperties.gameDeveloper.Unity + workerProperties.gameDeveloper.Unreal) / 4;

        mobileAvg = (workerProperties.mobileDeveloper.Android_Stuido + workerProperties.mobileDeveloper.XCode_Swift
            + workerProperties.mobileDeveloper.React_Native + workerProperties.mobileDeveloper.Flutter) / 4;

        generalAvg = (workerProperties.generalDeveloper.Img_P + workerProperties.generalDeveloper.Deep_L
            + workerProperties.generalDeveloper.Pic_Arduino + workerProperties.generalDeveloper.Desktop_App) / 4;

        engineerAvg = (workerProperties.certificatedEngineer.DL_AI_Architecture + workerProperties.certificatedEngineer.OS
            + workerProperties.certificatedEngineer.PL + workerProperties.certificatedEngineer.Game_Engine) / 4;

        GameObject childWorker = Instantiate(workerPanelPrefab, content);
        childWorker.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Name: " + workerProperties.Name;
        childWorker.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Specialization: " + workerProperties.Specialization;
        childWorker.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "FrontEnd Average: " + frontEndAvg;
        childWorker.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "BackEnd Average: " + backEndAvg;
        childWorker.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Game Average: " + gameAvg;
        childWorker.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Mobile Average: " + mobileAvg;
        childWorker.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "General Average: " + generalAvg;
        childWorker.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Engineer Average: " + engineerAvg;
        childWorker.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = "Speed: " + workerProperties.Speed;
        childWorker.transform.GetChild(9).GetComponent<TextMeshProUGUI>().text = "Error Rate: " + workerProperties.ErrorRate;
        childWorker.transform.GetChild(10).GetComponent<TextMeshProUGUI>().text = "Salary: " + workerProperties.Salary;
        childWorker.transform.GetChild(11).GetComponent<DetailsWorkerButtonAction>().id = workerProperties.id;
    }

    //When new workers are to be created, the hire workers panel is emptied.
    void DestroyContentChilds()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }





}
