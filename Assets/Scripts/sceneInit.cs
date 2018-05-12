using UnityEngine;

public class sceneInit : MonoBehaviour {
    public Transform player;
	void Start () {
        Instantiate(player, Vector3.zero, player.rotation);
	}

}
