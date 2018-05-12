using UnityEngine;

public class MenuSceneManager : MonoBehaviour
{
    void Awake()
    {
        MenuManager.Instance.SetMainMenu(true);
    }
}
