using UnityEngine;

public class SpawnerController : MonoBehaviour {
    public void SpawnPointSphere () {
        Instantiate (interactableSpawns[0], new Vector3 (Random.Range (limitDown.position.x, limitUp.position.x), transform.position.y, Random.Range (limitDown.position.z, limitUp.position.z)), Quaternion.identity);
    }

    [SerializeField] private GameObject[] interactableSpawns = new GameObject[0];
    [SerializeField] private Transform limitDown = null;
    [SerializeField] private Transform limitUp = null;
}