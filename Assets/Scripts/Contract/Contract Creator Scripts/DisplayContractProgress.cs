using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using TMPro;
using UnityEngine;

public class DisplayContractProgress : MonoBehaviour
{
    //The Contract Object this panel is attached to
    public GameObject activeContractObject;
    public ContractDataStructure contract;

    //The data of the contract to be updated.
    TextMeshProUGUI progress;
    TextMeshProUGUI error;
    TextMeshProUGUI timeText;

    DeskMenu deskMenu;
    GameObject[] desks;

    TimeController time;

    //For money changes
    public Money money;

    //For Reputation changes
    public ReputationFile reputation;

    private void Awake()
    {
        timeText = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
        progress = transform.GetChild(7).GetComponent<TextMeshProUGUI>();
        error = transform.GetChild(8).GetComponent<TextMeshProUGUI>();

        deskMenu = GameObject.Find("Desk Menu").GetComponent<DeskMenu>();

        desks = GameObject.FindGameObjectsWithTag("Desk");

        time = GameObject.Find("Time Control").GetComponent<TimeController>();

        money = GameObject.Find("Money Panel").GetComponent<MoneyController>().money;

        reputation = GameObject.Find("Reputation Panel").GetComponent<Reputation>().repuObject;
    }

    private void Update()
    {
        //Updates the data in the panel(text) if the game is not stopped
        if (!time.isGamePaused)
        {
            timeText.text = "Time: " + contract.jobTime + " Day";
            progress.text = "Progress: " + contract.progress;
            error.text = "Error: " + contract.error;
        }

        //The button functions of the contract panel change according to the last clicked desk.

        //If it is a player, a delete contract button is added.
        if (deskMenu.hitDesk.GetChild(0).CompareTag("Player"))
        {
            transform.GetChild(11).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(11).gameObject.SetActive(false);
        }

        //If he has taken the contract before, the contract release button will come.
        if (deskMenu.hitDesk.GetComponent<Desk>().deskDatas.jobID == contract.id)
        {
            transform.GetChild(9).gameObject.SetActive(false);
            transform.GetChild(10).gameObject.SetActive(true);
        }
        //If he has not taken the contract before and left it, the button to get the contract comes.
        else
        {
            transform.GetChild(9).gameObject.SetActive(true);
            transform.GetChild(10).gameObject.SetActive(false);
        }
    }

    //If take contract button is clicked, send contract id to AssignToWork method of last clicked desk.
    public void DoTheJob()
    {
        deskMenu.hitDesk.GetComponent<Desk>().AssignToWork(contract.id);
    }

    //If break contract button is clicked, call StopTheWorking method of last clicked desk.
    public void BreakTheJob()
    {
        deskMenu.hitDesk.GetComponent<Desk>().StopTheWorking();
    }

    //If delete contract button is clicked, Stop all deks working on this contract. Then destroy the contract.
    public void DeleteTheJob()
    {
        money.ContractExpenses += contract.jobFailedMoney;

        foreach (var desk in desks)
        {
            if (desk.GetComponent<Desk>().deskDatas.jobID == contract.id)
            {
                desk.GetComponent<Desk>().StopTheWorking();
            }
        }

        // The base value is 10, the value from CalculateDailyWorkload() adds more multipliers the higher the workload.
        // Decrease reputation
        reputation.currentXP -= activeContractObject.GetComponent<ContractObject>().CalculateDailyWorkload() * 10;

        Destroy(activeContractObject);
        Destroy(gameObject);
    }

}
