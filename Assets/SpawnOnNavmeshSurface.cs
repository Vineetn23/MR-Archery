using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using Unity.AI;
using UnityEngine;
using UnityEngine.AI;

public class SpawnOnNavmeshSurface : MonoBehaviour
{
    public OVRSceneManager sceneManager;
    public NavMeshSurface surface;

    public float spawnTime = 3f;
    private float timer;

    public GameObject holePrefab;

    public float minRange;
    public float maxRange;


    private void OnDrawGizmos()
    {
        Vector3 center = Camera.main.transform.position;
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(center, minRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(center, maxRange);
    }

    // Start is called before the first frame update
    void Start()
    {
        sceneManager.SceneModelLoadedSuccessfully += BuildNavMesh;
    }

    public void BuildNavMesh()
    {
        surface.BuildNavMesh();
    }

    // Update is called once per frame
    void Update()
    {
        if (!surface.navMeshData)
        {
            return;
        }

        if(timer > spawnTime) 
        {
            Vector3 randomCircle = Random.onUnitSphere;
            randomCircle.y = 0f;

            Vector3 center = Camera.main.transform.position;
            center.y = 0f;

            Vector3 randomPosition = center + randomCircle * Random.Range(minRange, maxRange);

            bool isOnNavMesh = NavMesh.SamplePosition(randomPosition, out NavMeshHit hit, 0.5f, NavMesh.AllAreas);

            if (isOnNavMesh)
            {
                GameObject spawned = Instantiate(holePrefab);
                spawned.transform.position = hit.position;

                timer = 0;
                Destroy(spawned, 6);
            }
        }

        timer += Time.deltaTime;
    }
}
