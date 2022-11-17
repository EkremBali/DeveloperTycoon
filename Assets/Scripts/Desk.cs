using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using static UnityEngine.InputManagerEntry;

[System.Serializable]
public class DeskDatasForSave
{
    public WorkerProperties workerProperties = new WorkerProperties();
    public bool isEmpty;
    public bool isWorking;
    public int jobID;
    public int progress;
    public int error;
    public bool isProgressDone = false;
    public bool isGameLoadSavedFile = false;
}


public class Desk : MonoBehaviour
{
    //The table's worker object
    public WorkerObject worker;

    //Active Contracts in the game
    Transform activeContracts;

    TimeController timeController;
    int day;

    public ContractObject contractObject;

    public DeskDatasForSave deskDatas;

    void Awake()
    {
        timeController = GameObject.Find("Time Control").GetComponent<TimeController>();
        day = timeController.time.day;

        activeContracts = GameObject.Find("Active Contracts").transform;

        if (!deskDatas.isGameLoadSavedFile)
        {
            if (!deskDatas.isEmpty)
            {
                deskDatas.workerProperties = worker.workerProperties;
            }
        }
        else
        {
            if(deskDatas.jobID != -1)
            {
                for (int i = 0; i < activeContracts.childCount; i++)
                {
                    if(deskDatas.jobID == activeContracts.GetChild(i).GetComponent<ContractObject>().contract.id)
                    {
                        contractObject = activeContracts.GetChild(i).GetComponent<ContractObject>();
                    } 
                }
            }
            if(transform.childCount != 0)
            {
                worker = transform.GetChild(0).GetComponent<WorkerObject>();
            }
        }

    }

    private void Update()
    {
        //If the table is working and the day has changed, enter.
        if (day != timeController.time.day && deskDatas.isWorking)
        {
            day = timeController.time.day;

            //Enter if the contract is not completed.
            //And increase these variables in the calculated progress and error rate. If the workload is complete, indicate that the program is finished and sync the progress to the workload.
            if (!deskDatas.isProgressDone)
            {
                contractObject.contract.progress += deskDatas.progress;
                contractObject.contract.error += deskDatas.error;

                if (contractObject.contract.progress >= contractObject.contract.workload)
                {
                    deskDatas.isProgressDone = true;
                    contractObject.contract.progress = contractObject.contract.workload;
                }
            }
            //If the contract is completed, enter and start to reduce the error, if the error goes down from 0, set it to 0 and stop working, indicating that the contract is completed successfully.
            else
            {
                contractObject.contract.error -= deskDatas.progress;
                if (contractObject.contract.error <= 0)
                {
                    contractObject.contract.error = 0;
                    StopTheWorking();
                }
            }
        }
    }


    //When the Take Contract button is clicked, sync the contract id to the Job id. Indicate that the desk is working. Then calculate the progress and error.
    public void AssignToWork(int id)
    {
        if (!deskDatas.isWorking)
        {
            deskDatas.isWorking = true;
            deskDatas.jobID = id;
            deskDatas.progress = DoThisJob();
            deskDatas.error = (int)Mathf.Ceil(deskDatas.progress / 100f) * Random.Range(1, 4) * worker.workerProperties.ErrorRate;
        }
    }

    //Do changes for Stop Working
    public void StopTheWorking()
    {
        deskDatas.isWorking = false;
        deskDatas.isProgressDone = false;
        deskDatas.jobID = -1;
        deskDatas.progress = 0;
        deskDatas.error = 0;
        contractObject = null;
    }

    //Find the contract object that matches the incoming id. Then calculate the progress and return it by calling the method suitable for this contract's job type.
    public int DoThisJob()
    {
        for (int i = 0; i < activeContracts.childCount; i++)
        {
            contractObject = activeContracts.GetChild(i).GetComponent<ContractObject>();

            if (contractObject.contract.id == deskDatas.jobID && deskDatas.isWorking)
            {
                switch (contractObject.contract.jobType)
                {
                    case "FrontEnd":
                        return CalculateProgressForFrontEnd(contractObject.contract);
                    case "BackEnd":
                        return CalculateProgressForBacktEnd(contractObject.contract);
                    case "Game":
                        return CalculateProgressForGame(contractObject.contract);
                    case "Mobile":
                        return CalculateProgressForMobile(contractObject.contract);
                    case "General":
                        return CalculateProgressForGeneral(contractObject.contract);
                    case "Engineer":
                        return CalculateProgressForEngineer(contractObject.contract);
                }

                break;
            }
        }
        return -1;
    }

    //Methods that find the required programs of the incoming contract and calculate and return the progress values by multiplying the skill and speed of the worker in this program.

    public int CalculateProgressForFrontEnd(ContractDataStructure contract)
    {
        WorkerProperties wp = worker.workerProperties;

        int progress = 0;

        string requiredPrograms = contract.requiredPrograms;

        switch (requiredPrograms)
        {
            case "HTML":
                progress = wp.frontEnd.HTML;
                break;
            case "CSS":
                progress = wp.frontEnd.CSS;
                break;
            case "JS":
                progress = wp.frontEnd.JS;
                break;
            case "HTML-CSS-JS":
                progress = (wp.frontEnd.HTML + wp.frontEnd.CSS + wp.frontEnd.JS) / 3;
                break;
            case "Angular":
                progress = wp.frontEnd.Angular;
                break;
            case "ReactJs":
                progress = wp.frontEnd.ReactJs;
                break;
        }

        progress *= wp.Speed;

        return progress;
    }

    public int CalculateProgressForBacktEnd(ContractDataStructure contract)
    {
        WorkerProperties wp = worker.workerProperties;

        int progress = 0;

        string requiredPrograms;


        requiredPrograms = contract.requiredPrograms;

        switch (requiredPrograms)
        {
            case "MySQL":
                progress = wp.backEnd.MySQL;
                break;
            case "MsSQL":
                progress = wp.backEnd.MsSQL;
                break;
            case "PHP":
                progress = wp.backEnd.PHP;
                break;
            case "DotNet":
                progress = wp.backEnd.DotNet;
                break;
            case "Django":
                progress = wp.backEnd.Django;
                break;
            case "PHP-MySQL":
                progress = (wp.backEnd.PHP + wp.backEnd.MySQL) / 2;
                break;
            case "PHP-MsSQL":
                progress = (wp.backEnd.PHP + wp.backEnd.MsSQL) / 2;
                break;
            case "DotNet-MySQL":
                progress = (wp.backEnd.DotNet + wp.backEnd.MySQL) / 2;
                break;
            case "DotNet-MsSQL":
                progress = (wp.backEnd.DotNet + wp.backEnd.MsSQL) / 2;
                break;
        }

        progress *= wp.Speed;

        return progress;
    }

    public int CalculateProgressForGame(ContractDataStructure contract)
    {
        WorkerProperties wp = worker.workerProperties;

        int progress = 0;

        string requiredPrograms;


        requiredPrograms = contract.requiredPrograms;

        switch (requiredPrograms)
        {
            case "UI_Sprite":
                progress = wp.gameDeveloper.UI_Sprite;
                break;
            case "ThreeD_Model":
                progress = wp.gameDeveloper.ThreeD_Model;
                break;
            case "Unity":
                progress = wp.gameDeveloper.Unity;
                break;
            case "Unreal":
                progress = wp.gameDeveloper.Unreal;
                break;
            case "Unity-UI_Sprite":
                progress = (wp.gameDeveloper.Unity + wp.gameDeveloper.UI_Sprite) / 2;
                break;
            case "Unity-ThreeD_Model":
                progress = (wp.gameDeveloper.Unity + wp.gameDeveloper.ThreeD_Model) / 2;
                break;
            case "Unreal-UI_Sprite":
                progress = (wp.gameDeveloper.Unreal + wp.gameDeveloper.UI_Sprite) / 2;
                break;
            case "Unreal-ThreeD_Model":
                progress = (wp.gameDeveloper.Unreal + wp.gameDeveloper.ThreeD_Model) / 2;
                break;
            case "UI_Sprite-ThreeD_Model":
                progress = (wp.gameDeveloper.UI_Sprite + wp.gameDeveloper.ThreeD_Model) / 2;
                break;
        }

        progress *= wp.Speed;

        return progress;
    }

    public int CalculateProgressForMobile(ContractDataStructure contract)
    {
        WorkerProperties wp = worker.workerProperties;

        int progress = 0;

        string requiredPrograms;


        requiredPrograms = contract.requiredPrograms;

        switch (requiredPrograms)
        {
            case "Android_Stuido":
                progress = wp.mobileDeveloper.Android_Stuido;
                break;
            case "XCode_Swift":
                progress = wp.mobileDeveloper.XCode_Swift;
                break;
            case "React_Native":
                progress = wp.mobileDeveloper.React_Native;
                break;
            case "Flutter":
                progress = wp.mobileDeveloper.Flutter;
                break;
        }

        progress *= wp.Speed;

        return progress;
    }

    public int CalculateProgressForGeneral(ContractDataStructure contract)
    {
        WorkerProperties wp = worker.workerProperties;

        int progress = 0;

        string requiredPrograms;


        requiredPrograms = contract.requiredPrograms;

        switch (requiredPrograms)
        {
            case "Img_P":
                progress = wp.generalDeveloper.Img_P;
                break;
            case "Deep_L":
                progress = wp.generalDeveloper.Deep_L;
                break;
            case "Pic_Arduino":
                progress = wp.generalDeveloper.Pic_Arduino;
                break;
            case "Desktop_App":
                progress = wp.generalDeveloper.Desktop_App;
                break;
            case "Img_P-Deep_L":
                progress = (wp.generalDeveloper.Img_P + wp.generalDeveloper.Deep_L) / 2;
                break;
            case "Img_P-Pic_Arduino":
                progress = (wp.generalDeveloper.Img_P + wp.generalDeveloper.Pic_Arduino) / 2;
                break;
        }

        progress *= wp.Speed;

        return progress;
    }

    public int CalculateProgressForEngineer(ContractDataStructure contract)
    {
        WorkerProperties wp = worker.workerProperties;

        int progress = 0;

        string requiredPrograms;


        requiredPrograms = contract.requiredPrograms;

        switch (requiredPrograms)
        {
            case "DL_AI_Architecture":
                progress = wp.certificatedEngineer.DL_AI_Architecture;
                break;
            case "OS":
                progress = wp.certificatedEngineer.OS;
                break;
            case "PL":
                progress = wp.certificatedEngineer.PL;
                break;
            case "Game_Engine":
                progress = wp.certificatedEngineer.Game_Engine;
                break;
            case "Game_Engine-DL_AI_Architecture":
                progress = (wp.certificatedEngineer.Game_Engine + wp.certificatedEngineer.DL_AI_Architecture) / 2;
                break;
            case "Game_Engine-PL":
                progress = (wp.certificatedEngineer.Game_Engine + wp.certificatedEngineer.PL) / 2;
                break;
        }

        progress *= wp.Speed;

        return progress;
    }
}
