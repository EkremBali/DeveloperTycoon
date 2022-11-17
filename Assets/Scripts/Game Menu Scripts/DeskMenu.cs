using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class DeskMenu : MonoBehaviour
{
    GameMenuController gmc;

    GameObject displayWorkerDetails;
    GameObject activeJobs;

    Ray ray;
    RaycastHit hit;
    public Transform hitDesk;

    public bool isAnyButtonClick = false;
    public bool isDeskClicked = false;

    void Start()
    {
        displayWorkerDetails = GameObject.Find("Worker Details");
        activeJobs = GameObject.Find("Active Jobs");

        gmc = GameObject.Find("Game Menu").GetComponent<GameMenuController>();
        gmc.largePanels.Add(activeJobs.transform.GetChild(0).gameObject);
    }


    void Update()
    {
        //If the mouse is clicked and no DeskMenu button has been clicked before, it enters here.
        //Then a ray is sent to the game world from where the mouse is clicked.
        if (Input.GetMouseButtonDown(0) && !isAnyButtonClick)
        {
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 1000))
            {
                //If the ray hit a desk and this desk has a worker, take the hit table and open the deskmenu panel.
                if (hit.transform.CompareTag("Desk") && hit.transform.childCount != 0)
                {
                    //Eðer ki týklanan masa daha önceki ile ayný ise DeskMenu Panelini kapat, farklý ise yeniden paneli aç.
                    if (hit.transform != hitDesk || !isDeskClicked)
                    {
                        hitDesk = hit.transform;
                        transform.GetChild(0).gameObject.SetActive(true);
                        transform.position = new Vector3(hit.transform.position.x - 1, transform.position.y, hit.transform.position.z - 4f);
                        isDeskClicked = true;
                    }
                    else
                    {
                        transform.GetChild(0).gameObject.SetActive(false);
                        isDeskClicked = false;
                    }   
                }
            }
        }
        
    }


    //DESK MENU BUTTONS ACTÝON

    //If worker button is clicked
    //The worker of the clicked table is reached, then the Worker Properties object of this worker is sent to the DisplayWorkerDetails component of the required panel to be written on the screen.
    public void WorkerClicked()
    {
        isAnyButtonClick = true;
        Worker a = new Worker();
        a.workerProperties = hitDesk.GetChild(0).GetComponent<WorkerObject>().workerProperties;

        transform.GetChild(0).gameObject.SetActive(false);

        displayWorkerDetails.transform.GetChild(1).GetComponent<DisplayWorkerDetails>().Display(a);
        displayWorkerDetails.transform.GetChild(1).gameObject.SetActive(true);
    }

    //If Fire Worker button is clicked.
    //If the worker is not a player then stop the table running and set the table as empty. Then destroy the worker.
    public void FireWorker()
    {
        if (!hitDesk.GetChild(0).CompareTag("Player"))
        {
            hitDesk.GetComponent<Desk>().deskDatas.isEmpty = true;
            hitDesk.GetComponent<Desk>().StopTheWorking();
            displayWorkerDetails.transform.GetChild(1).gameObject.SetActive(false);
            Destroy(hitDesk.GetChild(0).gameObject);
        }
        else
        {
            Debug.Log("You can't fire player");
        }
        isAnyButtonClick = false;
        
    }

    //If JOBS button is clicked. Active Jobs panel set active true.
    public void JobsClicked()
    {
        isAnyButtonClick = !isAnyButtonClick;
        activeJobs.transform.GetChild(0).gameObject.SetActive(!activeJobs.transform.GetChild(0).gameObject.activeSelf);
        transform.GetChild(0).gameObject.SetActive(false);
        isDeskClicked = false;
    }
}
