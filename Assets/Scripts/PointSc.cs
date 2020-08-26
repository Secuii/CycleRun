using UnityEngine;

public class PointSc : MonoBehaviour
{
    private void Start()
    {
        spawnCtr = GameObject.FindGameObjectWithTag("Spawn").GetComponent<SpawnerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("wall"))
        {
            spawnCtr.SpawnPointSphere();
            Destroy(gameObject);
        }
    }

    private SpawnerController spawnCtr;
}
