using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DataUI : MonoBehaviour
{
    public TextMeshProUGUI dataText;

    private void OnEnable()
    {
        EventHandler.GameDataEvent += OnGameDataEvent;
    }
    private void OnDisable()
    {
        EventHandler.GameDataEvent -= OnGameDataEvent;
    }
    private void OnGameDataEvent()
    {
        DataManager.Instance.UpdateEconomy();
        UpdateEconomyUI();
    }
    void UpdateEconomyUI()
    {
        dataText.text = "" + DataManager.Instance.economy;
    }
}
