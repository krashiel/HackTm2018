using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cloudsGenerator : MonoBehaviour
{

    public List<Transform> elements;
    public float density;
    Transform ground;

    void Start()
    {
        ground = this.transform;
        float boundsOffset = 10;
        for (int i = 0; i < density; i++)
        {
           Transform el = Instantiate(elements[Random.Range(0, elements.Capacity)], 
                        new Vector3(Random.Range(groundBounds(ground).min.x - boundsOffset, groundBounds(ground).max.x + boundsOffset),
                                    8,
                                    Random.Range(groundBounds(ground).min.z - boundsOffset, groundBounds(ground).max.z + boundsOffset)),
                        Quaternion.Euler(0, 90, 0));
        }
    }

    Bounds groundBounds(Transform ground)
    {
        Bounds bounds = GameObject.FindWithTag("ground").GetComponent<Collider>().bounds;
        return bounds;
    }
}
