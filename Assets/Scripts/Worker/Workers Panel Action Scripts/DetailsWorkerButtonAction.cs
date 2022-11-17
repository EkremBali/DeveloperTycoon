using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DetailsWorkerButtonAction : MonoBehaviour
{
    public int id;

    //This veriables for destroy hired workers from panel
    public bool isHired = false;
    public bool isDetailsClick = false;

    WorkerCreator workerCreator;
    Worker workerData;
    GameObject workerDetailsPanel;

    void Start()
    {
        
        workerCreator = GameObject.Find("Worker Creator").GetComponent<WorkerCreator>();
        workerDetailsPanel = GameObject.Find("Worker Details").transform.GetChild(0).gameObject;

        //Get the worker with the button id.
        foreach (var worker in workerCreator.workerList)
        {
            if (id == worker.workerProperties.id)
            {
                workerData = worker;
                break;
            }
        }
    }

    //If details button are clicked then Get Display with workerData
    public void WorkerDetailsClicked()
    {
        workerDetailsPanel.GetComponent<DisplayWorkerDetails>().Display(workerData);
        workerDetailsPanel.SetActive(true);
        isDetailsClick = true;
    }


    //If worker is hired then destroy this worker panel from hired workers panel.
    private void Update()
    {
        if (isHired)
        {
            Destroy(transform.parent.gameObject);
        }
    }

}
