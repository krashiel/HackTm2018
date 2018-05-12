using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour {
    #region Singleton
    public static PlayerManager instance;

    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject player;

    private void Update()
    {
        if(player == null)
        player = GameObject.FindWithTag("Player");
    }
}
