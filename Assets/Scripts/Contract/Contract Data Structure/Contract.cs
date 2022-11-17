using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;

[System.Serializable]
public class ContractDataStructure
{
    public int id;
    public string jobType;
    public string requiredPrograms;
    public string aboutJob;
    public int jobTime;
    public int firstJobTime;
    public int workload;
    public int jobMoney;
    public int jobFailedMoney;
    public int progress;
    public int error;
    
}

public abstract class Contract
{
    public ContractDataStructure contractDatas { get; set; }

    public Contract(string jobType)
    {
        contractDatas = new ContractDataStructure();
        contractDatas.jobType = jobType;    
    }

    protected abstract void RandomRequiredProgram();

    protected abstract void GetRandomAboutJob();

    protected void RandomWorkload_JobTime_Money(int prestige)
    {
        //Required veriables for calculate workload and monies
        int maxWorkloadPerDay;
        int workloadPerDay;

        //Min time 5 and max time 30 day for a contract
        contractDatas.jobTime = Random.Range(5, 30);
        contractDatas.firstJobTime = contractDatas.jobTime;

        //As the prestige increases, we increase the workload and profit.
        if (prestige < 2f)
        {
            maxWorkloadPerDay = 40; // iki iþki kapasitesi * ort skill = 10 * ort hýz = 1 --> 1*10*2 = 20; Biraz fazlasýný yazýyorz ki yapýlamayacak kontratta gelsin.
            workloadPerDay = Random.Range(5, maxWorkloadPerDay);
        }
        else if (prestige < 4f)
        {
            maxWorkloadPerDay = 450; // 5 * 30 * 2 = 300
            workloadPerDay = Random.Range(150, maxWorkloadPerDay);
        }
        else if (prestige < 6f)
        {
            maxWorkloadPerDay = 1800; // 10 * 50 * 3 = 1500
            workloadPerDay = Random.Range(600, maxWorkloadPerDay);
        }
        else if (prestige < 8f)
        {
            maxWorkloadPerDay = 5000; // 15 * 70 * 4 = 4200
            workloadPerDay = Random.Range(1200, maxWorkloadPerDay);
        }
        else
        {
            maxWorkloadPerDay = 10000; // 20 * 90 * 5 = 9000
            workloadPerDay = Random.Range(5500, maxWorkloadPerDay);
        }

        //After determining the daily workload, we determine the total workload according to time.
        contractDatas.workload = workloadPerDay * contractDatas.jobTime;

        //We choose a random money based on the workload.
        contractDatas.jobMoney = contractDatas.workload * Random.Range(1, 4);
        contractDatas.jobFailedMoney = contractDatas.jobMoney / 2;
    }

    public void CreateContract(int prestige)
    {
        RandomRequiredProgram();
        GetRandomAboutJob();
        RandomWorkload_JobTime_Money(prestige);
    }

}
