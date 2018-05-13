using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_script : MonoBehaviour
{
    public int enemyHealth = 10;
    public bool isDead = false;
    public Transform stump;

    private void Start()
    {

    }
    private void Update()
    {


        if (enemyHealth < 1 && isDead)
        {
            rockExplosion();
            isDead = false;
            Destroy(transform.gameObject);
        }
    }

    void rockExplosion()
    {
        for (int i = 0; i < 10; i++)
        {
            float rVal = Random.Range(-1f, 1f);
            Transform tr = Instantiate(stump, new Vector3(transform.position.x - rVal, transform.position.y - rVal, transform.position.z - rVal), transform.rotation);
            Rigidbody rb = tr.gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            //rb.AddForce(Vector3.up, ForceMode.Impulse);
        }
    }
}
