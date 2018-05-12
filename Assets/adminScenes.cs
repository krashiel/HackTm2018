using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adminScenes : MonoBehaviour
{
    public GameObject[] spawnPoint;
    character_movement player_script;
    CameraController camera_script;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Keypad1))
        {
            teleport(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) || Input.GetKeyDown(KeyCode.Keypad2))
        {
            teleport(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) || Input.GetKeyDown(KeyCode.Keypad3))
        {
            teleport(2);
        }
    }

    public void teleport(int index)
    {
        player_script = GameObject.FindWithTag("Player").GetComponent<character_movement>();
        camera_script = GameObject.FindWithTag("MainCamera").GetComponent<CameraController>();
        if (player_script)
        {
            player_script.controller.Warp(spawnPoint[index].transform.position);
            player_script.StayOnGround();
            camera_script.changePPD();
        }
    }
}
