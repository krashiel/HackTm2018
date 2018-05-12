using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuSceneManager : MonoBehaviour
{
    void Awake()
    {
        MenuManager.Instance.SetMainMenu(true);
    }
}
