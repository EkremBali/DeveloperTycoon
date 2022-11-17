using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ContractObject : MonoBehaviour
{
    //Contract Datas is created TakeContract
    public ContractDataStructure contract;

    //This contractObect's panel object.
    public GameObject activeContractPanel;

    GameObject[] desks;

    TimeController timeController;
    int day;

    //For money changes
    public Money money;

    //For Reputation changes
    public ReputationFile reputation;

    //For Prestige changes
    public PrestigeFile prestige;

    private void Awake()
    {
        desks = GameObject.FindGameObjectsWithTag("Desk");

        money = GameObject.Find("Money Panel").GetComponent<MoneyController>().money;
    }

    private void Start()
    {
        reputation = GameObject.Find("Reputation Panel").GetComponent<Reputation>().repuObject;

        //load game olursa doðru çalýþmaz
        //contract.firstJobTime = contract.jobTime;

        prestige = GameObject.Find("Prestige").GetComponent<Prestige>().prestige;

        timeController = GameObject.Find("Time Control").GetComponent<TimeController>();
        day = timeController.time.day;
    }

    private void Update()
    {
        //If the day has passed, enter.
        //Then reduce the time attribute of the contract. If the time is -1, stop the desks running on this contract and destroy the contract.
        if (day != timeController.time.day)
        {
            day = timeController.time.day;
            contract.jobTime--;

            IsContractSuccessful();

            
        }

    }

    //If the contract is completely over, take the money and hit the contract.
    public void IsContractSuccessful()
    {
        if (contract.progress == contract.workload && contract.error == 0)
        {
            money.ContractIncome += contract.jobMoney;

            // The base value is 10, the value from CalculateDailyWorkload() adds more multipliers the higher the workload.
            reputation.currentXP += CalculateDailyWorkload() * 10;

            // The contract was submitted with successfully. So prestige is increase.
            ControlJobTypeAndChangePrestigeXP(10);

            DestroyActiveConrtact();
        }
        else
        {
            if (contract.jobTime == -1)
            {
                if (contract.progress == contract.workload && contract.error != 0)
                {
                    money.ContractIncome += contract.jobMoney;

                    // The base value is 10, the value from CalculateDailyWorkload() adds more multipliers the higher the workload.
                    reputation.currentXP += CalculateDailyWorkload() * 10;

                    // The contract was submitted with errors. So prestige is decrease
                    ControlJobTypeAndChangePrestigeXP(-10);
                }
                else
                {
                    money.ContractExpenses += contract.jobFailedMoney;

                    // The base value is 10, the value from CalculateDailyWorkload() adds more multipliers the higher the workload.
                    reputation.currentXP -= CalculateDailyWorkload() * 10;

                    //Ýþ teslim edilemedi. Fakat bu iþ türü prestijini az miktarda düþürür.
                    // The job could not be delivered. However, this type of job reduces his prestige slightly.
                    ControlJobTypeAndChangePrestigeXP(-2);
                }

                foreach (var desk in desks)
                {
                    if (desk.GetComponent<Desk>().deskDatas.jobID == contract.id)
                    {
                        desk.GetComponent<Desk>().StopTheWorking();
                    }
                }

                DestroyActiveConrtact();
            }

        }
    }

    //Günlük iþ yükleri hesaplanarak hangi prestij seviyesinde olduklar anlaþlýyor. Bu deðere göre çarpan belirleniyor.
    //Bu günlük iþ yükü sabitleri Contract classýndan incelenebilir.
    public int CalculateDailyWorkload()
    {
        int XP_multiplier = 0;

        int dailyWorkload = contract.workload / contract.firstJobTime;

        if (dailyWorkload <= 40)
        {
            XP_multiplier = 1;
            if(dailyWorkload > 20)
            {
                XP_multiplier = 2;
            }
        }
        else if (dailyWorkload <= 450)
        {
            XP_multiplier = 3;
            if (dailyWorkload > 225)
            {
                XP_multiplier = 4;
            }
        }
        else if (dailyWorkload <= 1800)
        {
            XP_multiplier = 5;
            if (dailyWorkload > 900)
            {
                XP_multiplier = 6;
            }
        }
        else if (dailyWorkload <= 5000)
        {
            XP_multiplier = 7;
            if (dailyWorkload > 2500)
            {
                XP_multiplier = 8;
            }
        }
        else // 10000
        {
            XP_multiplier = 9;
            if (dailyWorkload > 7500)
            {
                XP_multiplier = 10;
            }
        }

        return XP_multiplier;
    }


    //Ýþ türü hangisi ise o iþ türünün prestige'ini gelen baseVale'ya göre arttýrýyor yada azaltýyor.
    public void ControlJobTypeAndChangePrestigeXP(int baseValue)
    {
        if (contract.jobType.Equals("FrontEnd"))
        {
            prestige.fCurrentXP += CalculateDailyWorkload() * baseValue;
        }
        else if(contract.jobType.Equals("BackEnd"))
        {
            prestige.bCurrentXP += CalculateDailyWorkload() * baseValue;
        }
        else if (contract.jobType.Equals("Game"))
        {
            prestige.gaCurrentXP += CalculateDailyWorkload() * baseValue;
        }
        else if (contract.jobType.Equals("Mobile"))
        {
            prestige.mCurrentXP += CalculateDailyWorkload() * baseValue;
        }
        else if (contract.jobType.Equals("General"))
        {
            prestige.geCurrentXP += CalculateDailyWorkload() * baseValue;
        }
        else if (contract.jobType.Equals("Engineer"))
        {
            prestige.eCurrentXP += CalculateDailyWorkload() * baseValue;
        }
    }

    public void DestroyActiveConrtact()
    {
        Destroy(activeContractPanel, .1f);
        Destroy(gameObject, .1f);
    }
}
