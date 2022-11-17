using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MobileDS : Contract
{
    string[] programs = { "Android_Stuido", "XCode_Swift", "React_Native", "Flutter" };
    string path = "C:/Users/Ekrem/Desktop/DevTycoonSaveFiles/mobileAbouts.txt";

    public MobileDS(string jobType) : base(jobType) {}

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
