using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objects;
    public float spawnRatio;
    public float minThreshold;
    public float maxThreshold;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            var threshold = Random.Range(minThreshold, maxThreshold);
            var position = new Vector3(threshold, transform.position.y);
            GameObject gameObject = Instantiate(objects[Random.Range(0, objects.Length)], position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRatio);
            Destroy(gameObject, 5f);
        }
    }
}
