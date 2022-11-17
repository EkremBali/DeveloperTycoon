using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProperties : MonoBehaviour
{
    public static PlayerProperties Instance;

    public WorkerProperties player;
    public string loadPath;
    public List<string> savedSlotNames;

    private void Awake()
    {
        if (Instance == null)
        {
            savedSlotNames = new List<string>();
            Instance = this;
            DontDestroyOnLoad(Instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }


}
