using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class Money
{
    public int TotalMoney;
    public int BuildingExpenses;
    public int SalaryExpenses;
    public int TrainingExpenses;
    public int ContractExpenses;
    public int TotalExpense;
    public int ContractIncome;
    public int TotalIncome;
    public int Profit;
}

[System.Serializable]
public class YearlyProfitDatas
{
    public List<int> yearlyProfit = new List<int>();
    public List<int> lastYearProfit = new List<int>();
}


public class MoneyController : MonoBehaviour
{
    //For save last month chart 
    public Money lastMoney;

    public Money money;
    public YearlyProfitDatas yearlyProfitDatas;

    Transform incomeExpenseChartMonthly;
    public List<TextMeshProUGUI> moneyTexts;

    public TimeController timeController;
    public int mounth;
    public int year;

    GameObject[] desks;

    void Awake()
    {
        incomeExpenseChartMonthly = GameObject.Find("Income Expense Chart").transform.GetChild(0);
        CreateMoneyTexts();
        moneyTexts[8].text = "$" + money.TotalMoney;

        desks = GameObject.FindGameObjectsWithTag("Desk");
        UpdateMonthlyChart(lastMoney, true);
        DisplayYearlyPorfitChart(yearlyProfitDatas.lastYearProfit, true);
    }

    //since the time object is read from the saved file, it must be completed before it is read. that's why we get the time object in start.
    private void Start()
    {
        timeController = GameObject.Find("Time Control").GetComponent<TimeController>();
        mounth = timeController.time.mounth;
        year = timeController.time.year;
    }

    public void CreateMoneyTexts()
    {
        moneyTexts = new List<TextMeshProUGUI>();

        //First add expenses
        //Respectively; Building, Salary, Training, Contract, Total
        for (int i = 1; i < 10; i+=2)
        {
            moneyTexts.Add(incomeExpenseChartMonthly.GetChild(1).GetChild(i).GetComponent<TextMeshProUGUI>());
        }

        //Second add incomes, first is contract second is total
        moneyTexts.Add(incomeExpenseChartMonthly.GetChild(2).GetChild(1).GetComponent<TextMeshProUGUI>());
        moneyTexts.Add(incomeExpenseChartMonthly.GetChild(2).GetChild(3).GetComponent<TextMeshProUGUI>());

        //Add Profit Money Text
        moneyTexts.Add(incomeExpenseChartMonthly.GetChild(4).GetComponent<TextMeshProUGUI>());

        //Add Total Money Panel's Text
        moneyTexts.Add(transform.GetChild(0).GetComponent<TextMeshProUGUI>());


    }

    // Update is called once per frame
    void Update()
    {
        if(mounth != timeController.time.mounth)
        {
            mounth = timeController.time.mounth;
            UpdateMonthlyChart(money, false);
        }

        if(year != timeController.time.year)
        {
            year = timeController.time.year;
            DisplayYearlyPorfitChart(yearlyProfitDatas.yearlyProfit, false);
        }
    }

    //Calculate income and expense items, respectively. Then edit the texts. Edit the latest total money.
    public void UpdateMonthlyChart(Money money, bool isAwake)
    {
        moneyTexts[0].text = "" + money.BuildingExpenses;

        //Count the current workers and add up their salaries.
        money.SalaryExpenses = 0;
        foreach (var desk in desks)
        {
            if(desk.transform.childCount != 0)
            {
                money.SalaryExpenses += desk.transform.GetChild(0).GetComponent<WorkerObject>().workerProperties.Salary;
            }
        }
        moneyTexts[1].text = "" + money.SalaryExpenses;

        moneyTexts[2].text = "" + money.TrainingExpenses;
        moneyTexts[3].text = "" + money.ContractExpenses;

        money.TotalExpense = money.BuildingExpenses + money.SalaryExpenses + money.TrainingExpenses + money.ContractExpenses;

        moneyTexts[4].text = "" + money.TotalExpense;

        moneyTexts[5].text = "" + money.ContractIncome;

        money.TotalIncome = money.ContractIncome;

        moneyTexts[6].text = "" + money.TotalIncome;

        money.Profit = money.TotalIncome - money.TotalExpense;

        moneyTexts[7].text = "" + money.Profit;

        money.TotalMoney += money.Profit;

        moneyTexts[8].text = "$" + money.TotalMoney;


        if (!isAwake)
        {
            //Add list every mounth profit, for yearly profit chart
            yearlyProfitDatas.yearlyProfit.Add(money.Profit);
            AssignLastMoneyDatas();
        }
        

        money.ContractExpenses = 0;
        money.TrainingExpenses = 0;

        money.ContractIncome = 0;

        
    }

    public void AssignLastMoneyDatas()
    {
        lastMoney.BuildingExpenses = money.BuildingExpenses;
        lastMoney.ContractExpenses = money.ContractExpenses;
        lastMoney.TrainingExpenses = money.TrainingExpenses;
        lastMoney.SalaryExpenses = money.SalaryExpenses;
        lastMoney.TotalExpense = money.TotalExpense;
        lastMoney.ContractIncome = money.ContractIncome;
        lastMoney.TotalIncome = money.TotalIncome;
        lastMoney.Profit = money.Profit;
    }

    //When Clicked Get Chart Button: !active panel
    public void SetActiveMonthlyChart()
    {
        incomeExpenseChartMonthly.gameObject.SetActive(!incomeExpenseChartMonthly.gameObject.activeSelf);
    }

    //If time pass a year, then display all monthly profits and yearly total profit to Yearly Profit Chart
    public void DisplayYearlyPorfitChart(List<int> yearlyProfitList, bool isAwake)
    {
        int total = 0;

        Transform yearlyMoneyTexts = incomeExpenseChartMonthly.parent.GetChild(1).GetChild(1).GetChild(1);

        if(yearlyProfitList.Count != 0)
        {
            for (int i = 0; i < yearlyMoneyTexts.childCount; i++)
            {
                if (i != yearlyMoneyTexts.childCount - 1)
                {
                    total += yearlyProfitList[i];
                    yearlyMoneyTexts.GetChild(i).GetComponent<TextMeshProUGUI>().text = "" + yearlyProfitList[i];
                }
                else
                {
                    yearlyMoneyTexts.GetChild(i).GetComponent<TextMeshProUGUI>().text = "" + total;
                }
            }
        }

        if (!isAwake)
        {
            incomeExpenseChartMonthly.parent.GetChild(1).gameObject.SetActive(true);

            if (!timeController.isGamePaused)
            {
                timeController.PauseTime();
            }

            yearlyProfitDatas.lastYearProfit.Clear();

            for (int i = 0; i < yearlyProfitDatas.yearlyProfit.Count; i++)
            {
                yearlyProfitDatas.lastYearProfit.Add(yearlyProfitDatas.yearlyProfit[i]);
            }

            yearlyProfitDatas.yearlyProfit.Clear();

        }

    }

    public void SetActiveYearlyChart()
    {
        incomeExpenseChartMonthly.parent.GetChild(1).gameObject.SetActive(!incomeExpenseChartMonthly.parent.GetChild(1).gameObject.activeSelf);
    }
}
