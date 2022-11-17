using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TakeContract : MonoBehaviour
{
    public Contract contract;

    public ContractCreator contractCreator;


    //Prefab and parent objects required for the contract object and contract panel to be created.
    public GameObject jobPanelPrefab;
    public Transform jobPanelContent;

    public GameObject activeContractPrefab;
    public Transform activeContracts;



    private void Start()
    {
        contractCreator = GameObject.Find("Contract Creator").GetComponent<ContractCreator>();

        jobPanelContent = GameObject.Find("Active Jobs").transform.GetChild(0).GetChild(0).GetChild(0).GetChild(0);
        activeContracts = GameObject.Find("Active Contracts").transform;
    }


    //If the take contract button is clicked, first it creates the contract object containing the current contract object.
    //Then the contract panel is created where the contract in this object will be written to the screen.
    //This panel and the object are given to each other as objects so that they can access them when necessary.
    //Finally, the contract is deleted from the list of suggested contracts.
    public void TakeContractClicked()
    {
        GameObject activeContract = Instantiate(activeContractPrefab, activeContracts);
        contract.contractDatas.id = ContractCreator.takeContractID++;
        activeContract.GetComponent<ContractObject>().contract = contract.contractDatas;

        GameObject jopPanel = Instantiate(jobPanelPrefab, jobPanelContent);

        jopPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = contract.contractDatas.id+". Contract Type: " + contract.contractDatas.jobType;
        jopPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "Required Program Skill: " + contract.contractDatas.requiredPrograms;
        jopPanel.transform.GetChild(2).GetComponent<TextMeshProUGUI>().text = "About Contract: " + contract.contractDatas.aboutJob;
        jopPanel.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text = "Workload: " + contract.contractDatas.workload;
        jopPanel.transform.GetChild(4).GetComponent<TextMeshProUGUI>().text = "Time: " + contract.contractDatas.jobTime;
        jopPanel.transform.GetChild(5).GetComponent<TextMeshProUGUI>().text = "Money: " + contract.contractDatas.jobMoney;
        jopPanel.transform.GetChild(6).GetComponent<TextMeshProUGUI>().text = "Failed Money: " + contract.contractDatas.jobFailedMoney;
        jopPanel.transform.GetChild(7).GetComponent<TextMeshProUGUI>().text = "Progress: " + contract.contractDatas.progress;
        jopPanel.transform.GetChild(8).GetComponent<TextMeshProUGUI>().text = "Error: " + contract.contractDatas.error;

        jopPanel.GetComponent<DisplayContractProgress>().activeContractObject = activeContract;
        jopPanel.GetComponent<DisplayContractProgress>().contract = activeContract.GetComponent<ContractObject>().contract;

        activeContract.GetComponent<ContractObject>().activeContractPanel = jopPanel;

        contractCreator.contractList.Remove(contract);
        Destroy(gameObject, .3f);
    }
}
