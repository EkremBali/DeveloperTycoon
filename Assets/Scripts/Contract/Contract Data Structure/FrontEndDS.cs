using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Jobs.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.Rendering;

public class FrontEndDS : Contract
{
    static string[] programs = { "HTML", "CSS", "JS", "HTML-CSS-JS", "Angular", "ReactJs" };
    string path = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/frontendAbouts.txt";

    public FrontEndDS(string jobType) : base(jobType) {}

    protected override void RandomRequiredProgram()
    {
        int random = Random.Range(0, programs.Length);
        contractDatas.requiredPrograms = programs[random];
    }

    protected override void GetRandomAboutJob()
    {
        List<string> sameProgramAbouts = new List<string>();

        if (File.Exists(path))
        {
            string[] abouts = File.ReadAllLines(path);

            foreach (var about in abouts)
            {
                string program = about.Split("*")[0];

                if (program.Equals(contractDatas.requiredPrograms))
                {
                    sameProgramAbouts.Add(about.Split("*")[1]);
                }
            }

            int i = 0;
            int randomAboutFromList = Random.Range(0, sameProgramAbouts.Count);

            foreach (var about in sameProgramAbouts)
            {
                if(i == randomAboutFromList)
                {
                    contractDatas.aboutJob = about;
                    break;
                }
                i++;
            }
        }
        else
        {
            Debug.Log("File Not Exist");
        }


    }



}
