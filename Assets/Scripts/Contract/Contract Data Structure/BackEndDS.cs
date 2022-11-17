using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BackEndDS : Contract
{
    static string[] programs = { "MySQL", "MsSQL", "PHP", "DotNet", "Django", "PHP-MySQL", "PHP-MsSQL", "DotNet-MySQL", "DotNet-MsSQL" };
    string path = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/backendAbouts.txt";

    public BackEndDS(string jobType) : base(jobType) {}

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
                if (i == randomAboutFromList)
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
