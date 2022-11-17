using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class ReputationFile
{
    public int reputation;
    public int currentXP;
    public int targetXP;
}

public class Reputation : MonoBehaviour
{
    public ReputationFile repuObject;
    int firstRepu;

    void Awake()
    {
        firstRepu = repuObject.reputation;
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Company Reputation: " + repuObject.reputation;
    }


    void Update()
    {
        if (repuObject.currentXP >= repuObject.targetXP)
        {
            if(repuObject.reputation != 100)
            {
                repuObject.reputation += 1;
                repuObject.currentXP = 0;
                repuObject.targetXP += 100;
            }
            else
            {
                repuObject.currentXP = repuObject.targetXP - 100;
            }
        }
        else if (repuObject.currentXP < 0)
        {
            if (repuObject.reputation == 0)
            {
                repuObject.currentXP = 0;
            }
            else
            {
                repuObject.reputation -= 1;
                repuObject.currentXP = 100 + repuObject.currentXP;
                repuObject.targetXP -= 100;
            }
        }

        if (firstRepu != repuObject.reputation)
        {
            firstRepu = repuObject.reputation;
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = "Company Reputation: " + repuObject.reputation;
        }


    }
}
