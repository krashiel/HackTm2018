using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour {
    Transform ground;
    public Transform enemyPrefab;
    private List<Transform> enemyList;
    public int maxEnemyOnMap = 5;
	void Start () {
        ground = GameObject.FindWithTag("ground").transform;
            StartCoroutine(spawnOne(3.0f));
	}

    private IEnumerator spawnOne(float sec)
    {
        yield return new WaitForSeconds(sec);
        spawn();
    }

    Bounds groundBounds(Transform ground)
    {
        Bounds bounds = GameObject.FindWithTag("ground").GetComponent<Collider>().bounds;
        return bounds;
    }

    private void spawn()
    {
        float boundsOffset = 1;
        Transform enemy = Instantiate(enemyPrefab,
                         new Vector3(Random.Range(groundBounds(ground).min.x + boundsOffset, groundBounds(ground).max.x - boundsOffset),
                                     0,
                                     Random.Range(groundBounds(ground).min.z + boundsOffset, groundBounds(ground).max.z - boundsOffset)),
                         Quaternion.Euler(0, Random.Range(0, 90), 0));
    }
}
