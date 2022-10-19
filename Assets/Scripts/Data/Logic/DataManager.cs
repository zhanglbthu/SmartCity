using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : Singleton<DataManager>
{
    public int economy;
    public Transform fireBuildings;
    public void UpdateEconomy()
    {
        UpdateFireEconomy();
    }
    public void UpdateFireEconomy()
    {
        economy += DataSettings.fireDayEffect * fireBuildings.childCount;
    }
}
