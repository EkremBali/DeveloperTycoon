using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerObject : MonoBehaviour
{
    public WorkerProperties workerProperties;

    //If this gameobject is the player, get the properties fetched from the menu scene.
    private void Awake()
    {
        if (gameObject.CompareTag("Player"))
        {
            if (GameObject.Find("Player Properties") != null)
            {
                if(!GameObject.Find("Player Properties").GetComponent<PlayerProperties>().player.Name.Equals("Bos"))
                {
                    workerProperties = GameObject.Find("Player Properties").GetComponent<PlayerProperties>().player;
                }   
            }

        }
    }
}
