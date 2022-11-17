using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ContractCreatorTimeDatasForSave
{
    public int firstMounth;
    public bool isFirstChange;
}

[System.Serializable]
public class TakeContractIDData
{
    public int takeContractID;
}

public class ContractCreator : MonoBehaviour
{
    //To ID received contracts
    public static int takeContractID;

    //More prestige more contract
    PrestigeFile prestige;

    //Contracts are update per 2 mounth;
    //This variables and TimeController for this
    public TimeController timeController;
    public int firstMounth;
    public bool isFirstChange;
    

    public List<Contract> contractList;

    //For Contract panel
    public Transform content;
    public GameObject contractPrefab;

    private void Start()
    {
        prestige = GameObject.Find("Prestige").GetComponent<Prestige>().prestige;

        timeController = GameObject.Find("Time Control").GetComponent<TimeController>();

        if (contractList == null)
        {
            takeContractID = 0;

            firstMounth = timeController.time.mounth;
            isFirstChange = false;

            contractList = new List<Contract>();
            CreateContract();
        }
        else
        {
            DisplayContractToContent("new");
        }
  
    }

    void Update()
    {
        CheckTwoMounth();
    }

    //If 2 mounth pass then create new contracts
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
                CreateContract();
            }
        }
    }

    //Create new Contracts
    void CreateContract()
    {

        //The list is emptied when new contracts are to be created.
        contractList.Clear();
        DestroyContentChilds();

        int frontEndCount = prestige.frontendPrestige / 10;

        for (int i = 0; i <= frontEndCount; i++)
        {
            Contract forntend = new FrontEndDS("FrontEnd");
            forntend.CreateContract(frontEndCount);
            contractList.Add(forntend);
        }

        int backEndCount = prestige.backendPrestige / 10;

        for (int i = 0; i <= backEndCount; i++)
        {
            Contract backend = new BackEndDS("BackEnd");
            backend.CreateContract(frontEndCount);
            contractList.Add(backend);
        }

        int gameCount = prestige.gamePrestige / 10;

        for (int i = 0; i <= gameCount; i++)
        {
            Contract game = new GameDS("Game");
            game.CreateContract(gameCount);
            contractList.Add(game);
        }

        int mobileCount = prestige.mobilePrestige / 10;

        for (int i = 0; i <= mobileCount; i++)
        {
            Contract mobile = new MobileDS("Mobile");
            mobile.CreateContract(mobileCount);
            contractList.Add(mobile);
        }

        int generalCount = prestige.generalPrestige / 10;

        for (int i = 0; i <= generalCount; i++)
        {
            Contract general = new GeneralDS("General");
            general.CreateContract(generalCount);
            contractList.Add(general);
        }

        int engineerCount = prestige.engineerPrestige / 10;

        for (int i = 0; i <= engineerCount; i++)
        {   
            Contract engineer = new EngineerDS("Engineer");
            engineer.CreateContract(engineerCount);
            contractList.Add(engineer);
        }

        DisplayContractToContent("new");

    }


    //When new contracts are to be created, the contract panel is emptied.
    void DestroyContentChilds()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            Destroy(content.GetChild(i).gameObject);
        }
    }

    //New contracts are displayed in the contract panel.
    public void DisplayContractToContent(string typeOfJob)
    {
        
        foreach (var contract in contractList)
        {

            
            //If contract are newly create then Instantiate this panels
            if (typeOfJob.Equals("new"))
            {
                GameObject childContract = Instantiate(contractPrefab, content);
                childContract.GetComponent<TakeContract>().contract = contract;
                childContract.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Contract Type: " + contract.contractDatas.jobType;
                childContract.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Required Program Skill: " + contract.contractDatas.requiredPrograms;
                childContract.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "About Contract: " + contract.contractDatas.aboutJob;
                childContract.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Workload: " + contract.contractDatas.workload;
                childContract.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Time: " + contract.contractDatas.jobTime;
                childContract.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Money: " + contract.contractDatas.jobMoney;
                childContract.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Failed Money: " + contract.contractDatas.jobFailedMoney;
            }
            //If list buttons are clicked then list equals job type.
            else
            {
                for (int i = 0; i < content.childCount; i++)
                {
                    content.GetChild(i).gameObject.SetActive(true);
                    string type = content.GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text.Split(" ")[2];

                    if (!type.Contains(typeOfJob) && !typeOfJob.Equals("all"))
                    {
                        content.GetChild(i).gameObject.SetActive(false);
                    }
                }
            }
        }
    }

}
