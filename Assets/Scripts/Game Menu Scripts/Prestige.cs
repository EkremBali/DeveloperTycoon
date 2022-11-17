using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

[System.Serializable]
 public class PrestigeFile
{
    public int frontendPrestige;
    public int fCurrentXP;
    public int fTargetXP;
    public int backendPrestige;
    public int bCurrentXP;
    public int bTargetXP;
    public int gamePrestige;
    public int gaCurrentXP;
    public int gaTargetXP;
    public int mobilePrestige;
    public int mCurrentXP;
    public int mTargetXP;
    public int generalPrestige;
    public int geCurrentXP;
    public int geTargetXP;
    public int engineerPrestige;
    public int eCurrentXP;
    public int eTargetXP;
}

public class Prestige : MonoBehaviour
{
    public PrestigeFile prestige;

    //Abbility types prestige texts
    public TextMeshProUGUI frontendPrestigeText;
    public TextMeshProUGUI backendPrestigeText;
    public TextMeshProUGUI gamePrestigeText;
    public TextMeshProUGUI mobilePrestigeText;
    public TextMeshProUGUI generalPrestigeText;
    public TextMeshProUGUI engineerPrestigeText;

    public int maxXPIncrease;

    private void Awake()
    {
        UpdateText();
    }

    private void Update()
    {
        XPControl();
        UpdateText();
    }

    public void XPControl()
    {
        //Front End Prestige XP Control
        if (prestige.fCurrentXP >= prestige.fTargetXP)
        {
            if(prestige.frontendPrestige != 100)
            {
                prestige.frontendPrestige += 1;
                prestige.fCurrentXP = 0;
                prestige.fTargetXP += maxXPIncrease;
            }
            else
            {
                prestige.fCurrentXP = prestige.fTargetXP - 50;
            }    
        }
        else if (prestige.fCurrentXP < 0)
        {
            if (prestige.frontendPrestige == 0)
            {
                prestige.fCurrentXP = 0;
            }
            else
            {
                prestige.frontendPrestige -= 1;
                prestige.fCurrentXP = maxXPIncrease + prestige.fCurrentXP;
                prestige.fTargetXP -= maxXPIncrease;
            }
        }

        //Back End Prestige XP Control
        if (prestige.bCurrentXP >= prestige.bTargetXP)
        {
            if(prestige.backendPrestige != 100)
            {
                prestige.backendPrestige += 1;
                prestige.bCurrentXP = 0;
                prestige.bTargetXP += maxXPIncrease;
            }
            else
            {
                prestige.bCurrentXP = prestige.bTargetXP- 50;
            }
            
        }
        else if (prestige.bCurrentXP < 0)
        {
            if (prestige.backendPrestige == 0)
            {
                prestige.bCurrentXP = 0;
            }
            else
            {
                prestige.backendPrestige -= 1;
                prestige.bCurrentXP = maxXPIncrease + prestige.fCurrentXP;
                prestige.bTargetXP -= maxXPIncrease;
            }
        }

        //Game Prestige XP Control
        if (prestige.gaCurrentXP >= prestige.gaTargetXP)
        {
            if(prestige.gamePrestige != 100)
            {
                prestige.gamePrestige += 1;
                prestige.gaCurrentXP = 0;
                prestige.gaTargetXP += maxXPIncrease;
            }
            else
            {
                prestige.gaCurrentXP = prestige.gaTargetXP- 50;
            }
            
        }
        else if (prestige.gaCurrentXP < 0)
        {
            if (prestige.gamePrestige == 0)
            {
                prestige.gaCurrentXP = 0;
            }
            else
            {
                prestige.gamePrestige -= 1;
                prestige.gaCurrentXP = maxXPIncrease + prestige.gaCurrentXP;
                prestige.gaTargetXP -= maxXPIncrease;
            }
        }

        //Mobile Prestige XP Control
        if (prestige.mCurrentXP >= prestige.mTargetXP)
        {
            if(prestige.mobilePrestige != 100)
            {
                prestige.mobilePrestige += 1;
                prestige.mCurrentXP = 0;
                prestige.mTargetXP += maxXPIncrease;
            }
            else
            {
                prestige.mCurrentXP = prestige.mTargetXP- 50;
            }
            
        }
        else if (prestige.mCurrentXP < 0)
        {
            if (prestige.mobilePrestige == 0)
            {
                prestige.mCurrentXP = 0;
            }
            else
            {
                prestige.mobilePrestige -= 1;
                prestige.mCurrentXP = maxXPIncrease + prestige.mCurrentXP;
                prestige.mTargetXP -= maxXPIncrease;
            }
        }

        //General Prestige XP Control
        if (prestige.geCurrentXP >= prestige.geTargetXP)
        {
            if(prestige.generalPrestige != 100)
            {
                prestige.generalPrestige += 1;
                prestige.geCurrentXP = 0;
                prestige.geTargetXP += maxXPIncrease;
            }
            else
            {
                prestige.geCurrentXP = prestige.geTargetXP- 50;
            }
            
        }
        else if (prestige.geCurrentXP < 0)
        {
            if (prestige.generalPrestige == 0)
            {
                prestige.geCurrentXP = 0;
            }
            else
            {
                prestige.generalPrestige -= 1;
                prestige.geCurrentXP = maxXPIncrease + prestige.geCurrentXP;
                prestige.geTargetXP -= maxXPIncrease;
            }
        }

        //Engineer Prestige XP Control
        if (prestige.eCurrentXP >= prestige.eTargetXP)
        {
            if(prestige.engineerPrestige != 100)
            {
                prestige.engineerPrestige += 1;
                prestige.eCurrentXP = 0;
                prestige.eTargetXP += maxXPIncrease;
            }
            else
            {
                prestige.eCurrentXP = prestige.eTargetXP- 50;
            }
            
        }
        else if (prestige.eCurrentXP < 0)
        {
            if (prestige.engineerPrestige == 0)
            {
                prestige.eCurrentXP = 0;
            }
            else
            {
                prestige.engineerPrestige -= 1;
                prestige.eCurrentXP = maxXPIncrease + prestige.eCurrentXP;
                prestige.eTargetXP -= maxXPIncrease;
            }
        }
    }

    public void UpdateText()
    {
        frontendPrestigeText.text = "Front End : " + prestige.frontendPrestige;
        backendPrestigeText.text = "Back End : " + prestige.backendPrestige;
        gamePrestigeText.text = "Game Dev. : " + prestige.gamePrestige;
        mobilePrestigeText.text = "Mobile Dev. : " + prestige.mobilePrestige;
        generalPrestigeText.text = "General : " + prestige.generalPrestige;
        engineerPrestigeText.text = "Engineer : " + prestige.engineerPrestige;
    }

}
