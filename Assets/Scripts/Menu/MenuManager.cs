using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;

    public static MenuManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    public void SetMainMenu(bool status)
    {
        if (status)
        {
            StopTime();
            if (optionsMenu.activeSelf)
                SetOptionsMenu(false);
        }
        mainMenu.SetActive(status);
    }

    public void UnpauseGame()
    {
        SetMainMenu(false);
        SetOptionsMenu(false);
        StartTime();
    }

    public static void StopTime()
    {
        Time.timeScale = 0;
    }

    public static void StartTime()
    {
        Time.timeScale = 1;
    }

    public void SetOptionsMenu(bool status)
    {
        if (status)
        {
            SetMainMenu(false);
        }
        optionsMenu.SetActive(status);
    }

}
