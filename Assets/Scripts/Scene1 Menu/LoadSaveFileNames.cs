using System;
using System.IO;
using System.Security.Cryptography;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSaveFileNames : MonoBehaviour
{
    public TextMeshProUGUI button1Text;
    public TextMeshProUGUI button2Text;
    public TextMeshProUGUI button3Text;

    public string loadedSlot;

    void Start()
    {
        string slot1 = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/Slot1/";
        string slot2 = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/Slot2/";
        string slot3 = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/Slot3/";

        LoadButtonText(slot1, button1Text);
        LoadButtonText(slot2, button2Text);
        LoadButtonText(slot3, button3Text);

    }

    public void LoadButtonText(string path, TextMeshProUGUI buttonText)
    {
        var tumDosyalar = Directory.GetFiles(path, "*.*", SearchOption.AllDirectories);

        if (tumDosyalar.Length != 0)
        {
            string[] pathParts = tumDosyalar[0].Split("/");

            string name = pathParts[pathParts.Length - 1];

            name = name.Split(".")[0];

            GameObject.Find("Player Properties").GetComponent<PlayerProperties>().savedSlotNames.Add(name.ToUpper());

            buttonText.text = name.ToUpper();   
        }
    }

    public void SlotIsClicked(int slotNo)
    {
        if(slotNo == 1)
        {
            loadedSlot = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/Slot1/";
        }
        else if(slotNo == 2) 
        {
            loadedSlot = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/Slot2/";
        }
        else
        {
            loadedSlot = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/Slot3/";
        }

        var tumDosyalar = Directory.GetFiles(loadedSlot, "*.*", SearchOption.AllDirectories);

        if(tumDosyalar.Length != 0)
        {

            GameObject.Find("Player Properties").GetComponent<PlayerProperties>().loadPath = tumDosyalar[0];
            SceneManager.LoadScene(1);
        }
        else
        {
            Debug.Log("Boþ Slot");
        }

        

    }

}
