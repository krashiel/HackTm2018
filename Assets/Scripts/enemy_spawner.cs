using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_spawner : MonoBehaviour {
    Transform ground;
    public Transform spawnPoint;
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

    void CheckForHit()
    {

        RaycastHit objectHit;

        Vector3 down = transform.TransformDirection(-Vector3.up);
        Debug.DrawRay(transform.position, down * 50, Color.green);
        if (Physics.Raycast(transform.position, down, out objectHit, 50))
        {
            if (objectHit.transform.tag == "ground")
            {
                transform.position = objectHit.point;
                transform.rotation = Quaternion.FromToRotation(transform.up, objectHit.normal) * transform.rotation;
            }
            else
            {
                Destroy(transform.gameObject);
            }
        }
    }

    private void spawn()
    {
        float boundsOffset = 1;
        Transform enemy = Instantiate(enemyPrefab,
                         new Vector3(Random.Range(groundBounds(ground).min.x + boundsOffset, groundBounds(ground).max.x - boundsOffset),
                                     spawnPoint.position.y,
                                     Random.Range(groundBounds(ground).min.z + boundsOffset, groundBounds(ground).max.z - boundsOffset)),
                         Quaternion.Euler(0, Random.Range(0, 90), 0));
        CheckForHit();
    }
}
