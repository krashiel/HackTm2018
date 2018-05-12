using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resourcesGenerator : MonoBehaviour
{

    public List<Transform> elements;
    public float density;
    Transform ground;

    void Start()
    {
        ground = this.transform;
        float boundsOffset = 1;
        for (int i = 0; i < density; i++)
        {
           Transform el = Instantiate(elements[Random.Range(0, elements.Capacity)], 
                        new Vector3(Random.Range(groundBounds(ground).min.x + boundsOffset, groundBounds(ground).max.x - boundsOffset),
                                    10,
                                    Random.Range(groundBounds(ground).min.z + boundsOffset, groundBounds(ground).max.z - boundsOffset)),
                        Quaternion.Euler(0, Random.Range(0,90), 0));
            CheckForHit(el);
        }
    }

    Bounds groundBounds(Transform ground)
    {
        Bounds bounds = GameObject.FindWithTag("ground").GetComponent<Collider>().bounds;
        return bounds;
    }

    void CheckForHit(Transform tree)
    {

        RaycastHit objectHit;

        Vector3 down = tree.transform.TransformDirection(-Vector3.up);
        Debug.DrawRay(tree.transform.position, down * 50, Color.green);
        if (Physics.Raycast(tree.transform.position, down, out objectHit, 50))
        {
            if (objectHit.transform.tag == "ground")
            {
               tree.transform.position = objectHit.point;
               tree.transform.rotation = Quaternion.FromToRotation(transform.up, objectHit.normal) * transform.rotation;
            }
            else
            {
                Destroy(tree.gameObject);
            }
        }
    }
}
