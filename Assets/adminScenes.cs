using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class adminScenes : MonoBehaviour
{
    public GameObject[] spawnPoint;
    character_movement player_script;
    CameraController camera_script;

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
