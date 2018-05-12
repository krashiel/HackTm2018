using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public static bool IsGamePaused = false;
    public GameObject LoadingScreen;
    public Slider LoadingSlider;
    public TMP_Text LoadingPercentText;

    public void LoadGameScene()
    {
        StartCoroutine(LoadLevelAsync(1));
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }

    public static void ResumeGame()
    {
        Time.timeScale = 1f;
        IsGamePaused = false;
    }

    public static void PauseGame()
    {
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    //this is not used but it may be...
    private void LoadGame()
    {
        SceneManager.SetActiveScene(SceneManager.GetSceneByName("LevelScene"));
    }

    IEnumerator LoadLevelAsync(int sceneIndex)
    {
        //LoadingScreen.SetActive(true);
        MenuManager.Instance.SetGameUI(true);
        yield return null;
        //var scene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);

        //this.gameObject.SetActive(false);
        //while (!scene.isDone)
        //{
        //    float progress = Mathf.Clamp01(scene.progress / .9f) * 100;
        //    LoadingSlider.value = progress;
        //    LoadingPercentText.text = "Loading " + progress;
        //    yield return null;
        //}
    }

}
