using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        MenuManager.StartTime();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            MenuManager.Instance.SetMainMenu(true);
        }
    }
}
