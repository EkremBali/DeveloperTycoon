using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class DataToBeRecorded
{
    //Desk and Hired Workers
    public List<DeskDatasForSave> desks = new List<DeskDatasForSave>();
    //Waiting Workers
    public List<WorkerProperties> waitingWorkers = new List<WorkerProperties>();
    public WorkerCreatorTimeDatasForSave waitingWorkersRemainingTime = new WorkerCreatorTimeDatasForSave();

    //Active Contracts
    public List<ContractDataStructure> activeContracts = new List<ContractDataStructure>();
    public List<ContractDataStructure> waitingContracts = new List<ContractDataStructure>();
    public ContractCreatorTimeDatasForSave waitingContractsRemainingTime = new ContractCreatorTimeDatasForSave();
    public TakeContractIDData ActiveContractID = new TakeContractIDData();

    public GameTime time = new GameTime();

    public Money money = new Money();
    public Money lastMoney = new Money();
    public YearlyProfitDatas yearlyProfitDatas = new YearlyProfitDatas();

    public PrestigeFile prestige = new PrestigeFile();

    public ReputationFile reputation = new ReputationFile();


}

public class SaveGame : MonoBehaviour
{
    public DataToBeRecorded datas;

    public string path;

    public string fileName;

    public Transform desks;

    public WorkerCreator workerCreator;

    public Transform activeContractsInGame;

    public ContractCreator contractCreator;

    public TimeController timeController;

    public MoneyController moneyControl;

    public Prestige prestige;

    public Reputation reputation;

    public string lastClickedSlot;
    public TextMeshProUGUI[] saveSlotTexts;

    private void Awake()
    {
        timeController = GameObject.Find("Time Control").GetComponent<TimeController>();

        desks = GameObject.Find("Desks").transform;
        workerCreator = GameObject.Find("Worker Creator").GetComponent<WorkerCreator>();

        activeContractsInGame = GameObject.Find("Active Contracts").transform;
        contractCreator = GameObject.Find("Contract Creator").GetComponent<ContractCreator>();

        moneyControl = GameObject.Find("Money Panel").GetComponent<MoneyController>();

        prestige = GameObject.Find("Prestige").GetComponent<Prestige>();

        reputation = GameObject.Find("Reputation Panel").GetComponent<Reputation>();
    }

    public void AnySlotIsClicked(string slot)
    {
        lastClickedSlot = slot;

        path = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/";

        path += slot+"/";

        transform.GetChild(1).gameObject.SetActive(true);
    }

    public void NoClose()
    {
        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void YesSaveGame()
    {

        fileName = transform.GetChild(1).GetChild(1).GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text;

        if (fileName.Length == 1)
        {
            Debug.Log("Lütfen Bir Ýsim Giriniz...");
            return;
        }

        var tumDosyalar = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        if (tumDosyalar.Length != 0)
        {
            File.Delete(tumDosyalar[0]);
        }

        char[] fileNameChars = fileName.ToCharArray();

        fileName = "";

        for (int i = 0; i < fileNameChars.Length; i++)
        {
            if(i != fileNameChars.Length - 1)
            {
                fileName += fileNameChars[i];
            }
        }

        if (lastClickedSlot.Equals("Slot1"))
        {
            saveSlotTexts[0].text = fileName.ToUpper();
        }
        else if (lastClickedSlot.Equals("Slot2"))
        {
            saveSlotTexts[1].text = fileName.ToUpper();
        }
        else
        {
            saveSlotTexts[2].text = fileName.ToUpper();
        }
        
        path += fileName+".json";


        //Save Hired Workers
        SaveDeskAndHiredWorkers();
        SaveWaitingWorkers();
        SaveContracts();
        Others();

        string json = JsonUtility.ToJson(datas);
        File.WriteAllText(path, json);

        transform.GetChild(1).gameObject.SetActive(false);
    }

    public void SaveDeskAndHiredWorkers()
    {
        for (int i = 0; i < desks.childCount; i++)
        {
            datas.desks.Add(desks.GetChild(i).GetComponent<Desk>().deskDatas);
        }
    }

    public void SaveWaitingWorkers()
    {
        foreach (var worker in workerCreator.workerList)
        {
            datas.waitingWorkers.Add(worker.workerProperties);
        }

        datas.waitingWorkersRemainingTime.firstMounth = workerCreator.firstMounth;
        datas.waitingWorkersRemainingTime.isFirstChange = workerCreator.isFirstChange;
    }

    public void SaveContracts()
    {
        for (int i = 0; i < activeContractsInGame.childCount; i++)
        {
            datas.activeContracts.Add(activeContractsInGame.GetChild(i).GetComponent<ContractObject>().contract);
        }

        foreach (var contract in contractCreator.contractList)
        {
            datas.waitingContracts.Add(contract.contractDatas);
        }

        datas.waitingContractsRemainingTime.firstMounth = contractCreator.firstMounth;
        datas.waitingContractsRemainingTime.isFirstChange = contractCreator.isFirstChange;
        datas.ActiveContractID.takeContractID = ContractCreator.takeContractID;

    }

    //Time, Money, Prestige, Reputation
    public void Others()
    {
        //Save Time;
        datas.time = timeController.time;

        //Save Money
        datas.money = moneyControl.money;
        datas.lastMoney = moneyControl.lastMoney;
        datas.yearlyProfitDatas = moneyControl.yearlyProfitDatas;

        //Save Prestige
        datas.prestige = prestige.prestige;

        //Save Reputation
        datas.reputation = reputation.repuObject;
    }
}
