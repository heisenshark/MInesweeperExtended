using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Slider sizeX;
    public Slider sizeY;
    public Slider bombs;
    public TMPro.TextMeshProUGUI sizexText;
    public TMPro.TextMeshProUGUI sizeyText;
    public TMPro.TextMeshProUGUI bombsText;
    public TMPro.TextMeshProUGUI flags;
    public TMPro.TextMeshProUGUI time;
    public TMPro.TextMeshProUGUI winMessage;
    public GameObject panelTop;
    public GameObject panelBottom;



    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        flags.text = $"{MasterObject.masterObject.flagCount}";
        time.text = $"{Math.Floor(MasterObject.masterObject.timeElapsed)}";
        sizexText.text = $"{Convert.ToInt32(sizeX.value)}";
        sizeyText.text = $"{Convert.ToInt32(sizeY.value)}";
        bombsText.text = $"{Convert.ToInt32(bombs.value)}";
        if (!MasterObject.masterObject.lostGame)
            winMessage.text = ":)";
        else winMessage.text = "8)";

        if(MasterObject.masterObject.wonGame)
            winMessage.text = "B)";
    }

    public void GenerateGridEvent()
    {
        Debug.Log($"{sizeY.value}  {sizeX.value}");
        MasterObject.masterObject.boardWidth = Convert.ToInt32(sizeX.value);
        MasterObject.masterObject.boardHeight = Convert.ToInt32(sizeY.value);
        MasterObject.masterObject.bombsCount = Convert.ToInt32(bombs.value);
        MasterObject.masterObject.GenerateGrid();
        panelBottom.SetActive(false);
    }

    public void OptionsClick()
    {
        MasterObject.masterObject.ClearBoard();
        toggleBottom();
    }

    public void toggleBottom()
    {
        ShowBottom(!panelBottom.activeInHierarchy);
    }
    void ShowBottom(bool active)
    {
        panelBottom.SetActive(active);
    }
    public void ChangeValue(int newValue)
    {
        MasterObject.masterObject.boardWidth = newValue;
    }
}