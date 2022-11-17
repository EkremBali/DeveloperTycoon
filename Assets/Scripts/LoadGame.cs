using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using TMPro;
using UnityEngine;

public class LoadGame : MonoBehaviour
{
    public DataToBeRecorded datas;

    public TimeController timeController;
    public MoneyController moneyController;
    public Prestige prestige;
    public Reputation repu;

    public ContractCreator contractCreator;

    //Prefab and parent objects required for the contract object and contract panel to be created.
    public GameObject jobPanelPrefab;
    public Transform jobPanelContent;

    public GameObject activeContractPrefab;
    public Transform activeContracts;

    public WorkerCreator workerCreator;

    public Transform Desks;
    public GameObject workerPrefab;

    public GameObject gameMenu;
    public GameObject deskMenu;

    public GameObject Menus;

    public TextMeshProUGUI[] saveSlotTexts;

    public string path;

    PlayerProperties loadedPlayerProperties;

    void Start()
    {
        
        if (GameObject.Find("Player Properties") != null)
        {
            loadedPlayerProperties = GameObject.Find("Player Properties").GetComponent<PlayerProperties>();

            if (loadedPlayerProperties.player.Name.Equals("Bos"))
            {
                for (int i = 0; i < loadedPlayerProperties.savedSlotNames.Count; i++)
                {
                    saveSlotTexts[i].text = loadedPlayerProperties.savedSlotNames[i];
                }

                path = loadedPlayerProperties.loadPath;

                GetSave();
            } 
        }


        Menus.SetActive(true);
    }

    public void GetSave()
    {
        string json = File.ReadAllText(path);   
        JsonUtility.FromJsonOverwrite(json, datas);

        timeController.time = datas.time;

        moneyController.money = datas.money;
        moneyController.lastMoney = datas.lastMoney;
        moneyController.yearlyProfitDatas = datas.yearlyProfitDatas;

        prestige.prestige = datas.prestige;

        repu.repuObject = datas.reputation;

        contractCreator.contractList = CreateWaitingContractList();
        contractCreator.firstMounth = datas.waitingContractsRemainingTime.firstMounth;
        contractCreator.isFirstChange = datas.waitingContractsRemainingTime.isFirstChange;
        ContractCreator.takeContractID = datas.ActiveContractID.takeContractID;

        CreateActiveContracts();

        workerCreator.workerList = CreateWaitingWorkerList();
        workerCreator.firstMounth = datas.waitingWorkersRemainingTime.firstMounth;
        workerCreator.isFirstChange = datas.waitingWorkersRemainingTime.isFirstChange;

        CreateDeskAndActiveWorkers();
    }

    public List<Contract> CreateWaitingContractList()
    {
        Contract waiting = null;

        List<Contract> list = new List<Contract>();

        foreach (var contract in datas.waitingContracts)
        {
            switch (contract.jobType)
            {
                case "FrontEnd":
                    waiting = new FrontEndDS(contract.jobType);
                    waiting.contractDatas = contract;
                    break;
                case "BackEnd":
                    waiting = new BackEndDS(contract.jobType);
                    waiting.contractDatas = contract;
                    break;
                case "Game":
                    waiting = new GameDS(contract.jobType);
                    waiting.contractDatas = contract;
                    break;
                case "Mobile":
                    waiting = new MobileDS(contract.jobType);
                    waiting.contractDatas = contract;
                    break;
                case "General":
                    waiting = new GeneralDS(contract.jobType);
                    waiting.contractDatas = contract;
                    break;
                case "Engineer":
                    waiting = new EngineerDS(contract.jobType);
                    waiting.contractDatas = contract;
                    break;
            }

            list.Add(waiting);  
        }

        return list;
    }

    public void CreateActiveContracts()
    {
        foreach (var contract in datas.activeContracts)
        {
            GameObject activeContract = Instantiate(activeContractPrefab, activeContracts);
            activeContract.GetComponent<ContractObject>().contract = contract;

            GameObject jopPanel = Instantiate(jobPanelPrefab, jobPanelContent);

            jopPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = contract.id + ". Contract Type: " + contract.jobType;
            jopPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Required Program Skill: " + contract.requiredPrograms;
            jopPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "About Contract: " + contract.aboutJob;
            jopPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Workload: " + contract.workload;
            jopPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Time: " + contract.jobTime;
            jopPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Money: " + contract.jobMoney;
            jopPanel.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Failed Money: " + contract.jobFailedMoney;
            jopPanel.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Progress: " + contract.progress;
            jopPanel.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = "Error: " + contract.error;

            jopPanel.GetComponent<DisplayContractProgress>().activeContractObject = activeContract;
            jopPanel.GetComponent<DisplayContractProgress>().contract = activeContract.GetComponent<ContractObject>().contract;

            activeContract.GetComponent<ContractObject>().activeContractPanel = jopPanel;
        } 
    }

    public List<Worker> CreateWaitingWorkerList()
    {
        Worker waiting = null;

        List<Worker> list = new List<Worker>();

        foreach (var worker in datas.waitingWorkers)
        {
            waiting = new Worker();
            waiting.workerProperties = worker;
            list.Add(waiting);
        }

        return list;
    }
    
    public void CreateDeskAndActiveWorkers()
    {
        Desk desk;

        for (int i = 0; i < Desks.childCount; i++)
        {
            desk = Desks.GetChild(i).GetComponent<Desk>();
            desk.deskDatas = datas.desks[i];
            desk.deskDatas.isGameLoadSavedFile = true;

            if (!datas.desks[i].isEmpty && desk.transform.childCount == 0)
            {
                GameObject createdWorker = Instantiate(workerPrefab, desk.transform);
                createdWorker.GetComponent<WorkerObject>().workerProperties = desk.deskDatas.workerProperties;
            }
            
        }
    }

}
