using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class GameMenuController : MonoBehaviour
{
    public GameObject JobsPanel;
    public GameObject WorkerPanel;
    public GameObject PrestigePanel;
    public GameObject GetContractPanel;
    public GameObject HireWorkerPanel;
    public GameObject GetMonthlyChartPanel;
    public GameObject GetYearlyChartPanel;
    public GameObject SaveGamePanel;

    //When a panel is opened, it is kept in the list so that others can be closed.
    public List<GameObject> smallPanels;
    public List<GameObject> largePanels;

    GameObject deskMenuPanel;
    DeskMenu deskMenu;

    TimeController timeController;

    private void Awake()
    {
        timeController = GameObject.Find("Time Control").GetComponent<TimeController>();
        deskMenuPanel = GameObject.Find("Desk Menu").transform.GetChild(0).gameObject;
        deskMenu = GameObject.Find("Desk Menu").GetComponent<DeskMenu>();

        CreatePanelsList();
        
    }

    //Create small and large panels list.
    public void CreatePanelsList()
    {
        smallPanels = new List<GameObject>();
        largePanels = new List<GameObject>();

        smallPanels.Add(JobsPanel);
        smallPanels.Add(WorkerPanel);
        smallPanels.Add(PrestigePanel);
        smallPanels.Add(deskMenuPanel);

        largePanels.Add(GetContractPanel);
        largePanels.Add(HireWorkerPanel);
        largePanels.Add(GetMonthlyChartPanel);
        largePanels.Add(GetYearlyChartPanel);
        largePanels.Add(SaveGamePanel);

    }

    private void Update()
    {
        if (deskMenuPanel.activeSelf)
        {
            CloseOtherSmallPanels(deskMenuPanel);
        }
    }


    public void CloseOtherLargePanels(GameObject openPanel)
    {
        foreach (var panel in largePanels)
        {
            if (panel != openPanel)
            {
                panel.SetActive(false);
            }
        }
    }

    public void CloseOtherSmallPanels(GameObject openPanel)
    {
        foreach (var panel in smallPanels)
        {
            if (panel != openPanel)
            {
                panel.SetActive(false);
            }
        }
    }

    //Clicked Jobd Button
    public void JobsClicked()
    {
        JobsPanel.SetActive(!JobsPanel.activeSelf);
        deskMenu.isDeskClicked = false;
        CloseOtherSmallPanels(JobsPanel);
    }

    //Clicked Jobd Button
    public void WorkerClicked()
    {
        WorkerPanel.SetActive(!WorkerPanel.activeSelf);
        deskMenu.isDeskClicked = false;
        CloseOtherSmallPanels(WorkerPanel);
    }

    //Clicked Prestige Button
    public void PrestigeClicked()
    {
        //Update Prestige Texts 
        Prestige prestige = GameObject.Find("Prestige").GetComponent<Prestige>();
        prestige.UpdateText();

        PrestigePanel.SetActive(!PrestigePanel.activeSelf);
        deskMenu.isDeskClicked = false;
        CloseOtherSmallPanels(PrestigePanel);
    }

    //Clicked Get Contract Button
    public void GetContractClicked()
    {
        if (!GetContractPanel.activeSelf)
        {
            //DeskMenu must be not clickable
            deskMenu.isAnyButtonClick = true;
            deskMenu.isDeskClicked = false;
            GetContractPanel.SetActive(true);
            CloseOtherSmallPanels(GetContractPanel);
            CloseOtherLargePanels(GetContractPanel);
        }
        else
        {
            //DeskMenu must be clickable
            deskMenu.isAnyButtonClick = false;
            GetContractPanel.SetActive(false);
        }
    }

    //Clicked Hire Worker Button
    public void HireWorkerClicked()
    {
        if (!HireWorkerPanel.activeSelf)
        {
            //DeskMenu must be not clickable
            deskMenu.isAnyButtonClick = true;
            deskMenu.isDeskClicked = false;
            HireWorkerPanel.SetActive(true);
            CloseOtherLargePanels(HireWorkerPanel);
            CloseOtherSmallPanels(HireWorkerPanel);
        }
        else
        {
            //DeskMenu must be clickable
            deskMenu.isAnyButtonClick = false;
            HireWorkerPanel.SetActive(false);
        }
    }

    //Clicked Get Monthly Chart Button
    public void GetMonthlyClicked()
    {
        if (!GetMonthlyChartPanel.activeSelf)
        {
            deskMenu.isAnyButtonClick = true;
            deskMenu.isDeskClicked = false;
            GetMonthlyChartPanel.SetActive(true);
            CloseOtherLargePanels(GetMonthlyChartPanel);
            CloseOtherSmallPanels(GetMonthlyChartPanel);
        }
        else
        {
            deskMenu.isAnyButtonClick = false;
            GetMonthlyChartPanel.SetActive(false);
        }
    }

    //Clicked Get Yearly Chart Button
    public void GetYearlyClicked()
    {
        if (!GetYearlyChartPanel.activeSelf)
        {
            deskMenu.isAnyButtonClick = true;
            deskMenu.isDeskClicked = false;
            GetYearlyChartPanel.SetActive(true);
            CloseOtherLargePanels(GetYearlyChartPanel);
            CloseOtherSmallPanels(GetYearlyChartPanel);
        }
        else
        {
            deskMenu.isAnyButtonClick = false;
            GetYearlyChartPanel.SetActive(false);
        }
    }

    //Clicked Save Game Button
    public void SaveGameClicked()
    {
        //GameTime gt = new GameTime();

        //gt.year = time.year;
        //gt.mounth = time.mounth;
        //gt.week = time.week;
        //gt.day = time.day;

        //string path = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/lastTime.json";
        //string json = JsonUtility.ToJson(gt);
        //File.WriteAllText(path, json);

        if (!timeController.isGamePaused)
        {
            timeController.PauseTime();
        }
        

        if (!SaveGamePanel.activeSelf)
        {
            deskMenu.isAnyButtonClick = true;
            deskMenu.isDeskClicked = false;
            SaveGamePanel.SetActive(true);
            CloseOtherLargePanels(SaveGamePanel);
            CloseOtherSmallPanels(SaveGamePanel);
        }
        else
        {
            deskMenu.isAnyButtonClick = false;
            SaveGamePanel.SetActive(false);
        }
    }


}
