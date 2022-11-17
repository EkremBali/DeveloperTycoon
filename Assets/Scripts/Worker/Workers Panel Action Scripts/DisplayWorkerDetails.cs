using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DisplayWorkerDetails : MonoBehaviour
{
    public GameObject workerPrefab;
    public Transform hireWorkersContent;

    WorkerCreator workerCreator;
    WorkerProperties wpData;

    private void Start()
    {
        workerCreator = GameObject.Find("Worker Creator").GetComponent<WorkerCreator>();
    }

    //Print the incoming worker properties on the detail page.
    public void Display(Worker workerData)
    {
        wpData = workerData.workerProperties;
        transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = wpData.Name;
        transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Speed: " + wpData.Speed;
        transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Error Rate: " + wpData.ErrorRate;
        transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Salary: " + wpData.Salary;

        transform.GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = "HTML: "+ wpData.frontEnd.HTML;
        transform.GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>().text = "CSS: " + wpData.frontEnd.CSS;
        transform.GetChild(1).GetChild(3).GetComponent<TextMeshProUGUI>().text = "JavaScript: " + wpData.frontEnd.JS;
        transform.GetChild(1).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Angular " + wpData.frontEnd.Angular;
        transform.GetChild(1).GetChild(5).GetComponent<TextMeshProUGUI>().text = "ReactJs " + wpData.frontEnd.ReactJs;

        transform.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>().text = "MySQL: " + wpData.backEnd.MySQL;
        transform.GetChild(2).GetChild(2).GetComponent<TextMeshProUGUI>().text = "MsSQL: " + wpData.backEnd.MsSQL;
        transform.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>().text = "PHP: " + wpData.backEnd.PHP;
        transform.GetChild(2).GetChild(4).GetComponent<TextMeshProUGUI>().text = "DotNet " + wpData.backEnd.DotNet;
        transform.GetChild(2).GetChild(5).GetComponent<TextMeshProUGUI>().text = "Django " + wpData.backEnd.Django;

        transform.GetChild(3).GetChild(1).GetComponent<TextMeshProUGUI>().text = "UI_Sprite: " + wpData.gameDeveloper.UI_Sprite;
        transform.GetChild(3).GetChild(2).GetComponent<TextMeshProUGUI>().text = "3D Model: " + wpData.gameDeveloper.ThreeD_Model;
        transform.GetChild(3).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Unity: " + wpData.gameDeveloper.Unity;
        transform.GetChild(3).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Unreal " + wpData.gameDeveloper.Unreal;

        transform.GetChild(4).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Android Stuido: " + wpData.mobileDeveloper.Android_Stuido;
        transform.GetChild(4).GetChild(2).GetComponent<TextMeshProUGUI>().text = "Swift: " + wpData.mobileDeveloper.XCode_Swift;
        transform.GetChild(4).GetChild(3).GetComponent<TextMeshProUGUI>().text = "React Native: " + wpData.mobileDeveloper.React_Native;
        transform.GetChild(4).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Flutter " + wpData.mobileDeveloper.Flutter;

        transform.GetChild(5).GetChild(1).GetComponent<TextMeshProUGUI>().text = "Image Process: " + wpData.generalDeveloper.Img_P;
        transform.GetChild(5).GetChild(2).GetComponent<TextMeshProUGUI>().text = "DL&AI: " + wpData.generalDeveloper.Deep_L;
        transform.GetChild(5).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Development Cards: " + wpData.generalDeveloper.Pic_Arduino;
        transform.GetChild(5).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Desktopp App: " + wpData.generalDeveloper.Desktop_App;

        transform.GetChild(6).GetChild(1).GetComponent<TextMeshProUGUI>().text = "DL&AI Architecture: " + wpData.certificatedEngineer.DL_AI_Architecture;
        transform.GetChild(6).GetChild(2).GetComponent<TextMeshProUGUI>().text = "OS Programming: " + wpData.certificatedEngineer.OS;
        transform.GetChild(6).GetChild(3).GetComponent<TextMeshProUGUI>().text = "Prog. Lang. Prog: " + wpData.certificatedEngineer.PL;
        transform.GetChild(6).GetChild(4).GetComponent<TextMeshProUGUI>().text = "Game Engine: " + wpData.certificatedEngineer.Game_Engine;
    }

    //If the back button is clicked on the detail page, close the page
    public void BackToHirePage(bool isActive)
    {
        gameObject.SetActive(!gameObject.activeSelf);

        if (!isActive) 
        {
            DetailsWorkerButtonAction[] hireWorkerPanels = hireWorkersContent.GetComponentsInChildren<DetailsWorkerButtonAction>();

            foreach (var hireWorkerPanel in hireWorkerPanels)
            {
                if(hireWorkerPanel.isDetailsClick == true)
                {
                    hireWorkerPanel.isDetailsClick = false;
                    break;
                }
            }
        }
        else
        {
            GameObject.Find("Desk Menu").GetComponent<DeskMenu>().isAnyButtonClick = false;
            GameObject.Find("Desk Menu").GetComponent<DeskMenu>().isDeskClicked = false;
        }

        
    }

    //If the worker hire button is clicked on the detail page, create that worker then delete it from the list and UI panel.
    public void HireWorker()
    {
        DetailsWorkerButtonAction[] hireWorkerPanels = hireWorkersContent.GetComponentsInChildren<DetailsWorkerButtonAction>();

        GameObject[] desks = GameObject.FindGameObjectsWithTag("Desk");

        foreach (var desk in desks)
        {
            if (desk.GetComponent<Desk>().deskDatas.isEmpty)
            {
                GameObject createdWorker = Instantiate(workerPrefab, desk.transform);
                desk.GetComponent<Desk>().deskDatas.isEmpty = false;
                createdWorker.GetComponent<WorkerObject>().workerProperties = wpData;
                desk.GetComponent<Desk>().deskDatas.workerProperties = wpData;

                desk.GetComponent<Desk>().worker = desk.transform.GetChild(0).GetComponent<WorkerObject>();

                foreach (var worker in workerCreator.workerList)
                {
                   if(worker.workerProperties.id == wpData.id)
                    {
                        workerCreator.workerList.Remove(worker);
                        break;
                    }
                }


                foreach (var hireWorkerPanel in hireWorkerPanels)
                {
                    if (hireWorkerPanel.isDetailsClick == true)
                    {
                        hireWorkerPanel.isHired = true;
                        break;
                    }
                }

                gameObject.SetActive(!gameObject.activeSelf);

                break;
            }
        }

        GameObject.Find("Desk Menu").GetComponent<DeskMenu>().isDeskClicked = false;
    }
}
