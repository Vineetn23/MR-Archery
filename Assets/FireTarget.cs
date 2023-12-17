using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTarget : MonoBehaviour
{

    public GameObject targetPrefab;
    public Transform spawnPoint;
    public float height = 2;

    private GameObject spawned;
    
    public void Spawn()
    {
        spawned = Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity);

        Vector3 targetCamera = Camera.main.transform.position - spawned.transform.position;
        Vector3 lookCamera = Vector3.ProjectOnPlane(targetCamera, Vector3.up);

        spawned.transform.forward = lookCamera.normalized;

        Rigidbody rb = spawned.GetComponent<Rigidbody>();
        rb.velocity = Vector3.up * Mathf.Sqrt(2 * height * -Physics.gravity.y);
    }

    public void Despawn()
    {
        Destroy(spawned);
    }
}
