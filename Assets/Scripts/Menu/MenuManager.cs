using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject loadingUI;
    public GameObject gameUI;

    public static MenuManager Instance;

    private void Awake()
    {
        if (Instance != null)
        {
            Debug.Log("warning more than one menu manager instances found");
            return;
        }
        Instance = this;
        SetMainMenu(true);
    }

    public void SetMainMenu(bool status)
    {
        if (status)
        {
            StopTime();
            SetOptionsMenu(false);
            SetLoadingUI(false);
            SetGameUI(false);
        }
        mainMenu.SetActive(status);
    }

    public void SetOptionsMenu(bool status)
    {
        if (status)
        {
            SetMainMenu(false);
        }
        optionsMenu.SetActive(status);
    }

    public void SetLoadingUI(bool status)
    {
        if (status)
        {
            SetMainMenu(false);
            SetOptionsMenu(false);
        }
        loadingUI.SetActive(status);
    }

    public void SetGameUI(bool status)
    {
        if (status)
        {
            SetMainMenu(false);
            SetOptionsMenu(false);
            SetLoadingUI(false);
        }
        gameUI.SetActive(status);
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

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            SetMainMenu(true);
        }
    }

}
