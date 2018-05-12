using System.Collections.Generic;
using UnityEngine;

//use this to store player data
[CreateAssetMenu(fileName = "PlayerInfo", menuName = "Player/PlayerInfo")]
public class PlayerInfo : ScriptableObject
{
    new public string name = "New Item";
    public List<Item> items; 

}