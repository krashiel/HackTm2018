using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_script : MonoBehaviour
{
    public int treeHealth = 10;
    public bool isDead = false;
    public Item item;
    public Transform stump;

    private void Update()
    {
        if (treeHealth <= 0 && isDead)
        {
            Rigidbody rb = transform.parent.gameObject.AddComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.AddForce(new Vector3(Random.Range(0,1f), 0, Random.Range(0, 1f)), ForceMode.Impulse);
            StartCoroutine(destroyTree(5.0f));
            Instantiate(stump, rb.position, rb.rotation);
            isDead = false;
        }
    }

    private IEnumerator destroyTree(float sec) {
        Inventory.instance.Add(item);
        yield return new WaitForSeconds(sec);
        Destroy(transform.parent.gameObject);
    }
}
