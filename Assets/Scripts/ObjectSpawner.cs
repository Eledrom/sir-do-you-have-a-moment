using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objects;
    public float spawnRatio;
    public float minThreshold;
    public float maxThreshold;

    public float moveSpeedX = 1f;
    public float moveSpeedY = 1f;
    public float moveRangeX = 5f;
    public float moveRangeY = 3f;

    public int objectCount;

    public GameObject gameOver;

    public ParticleSystem ParticleSystem;

    private void Start()
    {
        StartCoroutine(Spawner());
    }

    private void Update()
    {
        spawnRatio -= Time.deltaTime / 15;

        Debug.Log(objectCount);

        GameOver();
    }

    IEnumerator Spawner()
    {
        while (true)
        {
            var threshold = Random.Range(minThreshold, maxThreshold);
            var position = new Vector3(threshold, transform.position.y);
            GameObject gameObject = Instantiate(objects[Random.Range(0, objects.Length)], position, Quaternion.identity);

            Vector3 startPosition = gameObject.transform.position;

            StartCoroutine(MoveObject(gameObject, startPosition));

            objectCount++;

            yield return new WaitForSeconds(spawnRatio);
        }
    }

    IEnumerator MoveObject(GameObject gameObject, Vector3 startPosition)
    {
        while (gameObject != null)
        {
            float newX = startPosition.x + Mathf.PingPong(Time.time * moveSpeedX, moveRangeX * 2) - moveRangeX;
            float newY = startPosition.y + Mathf.PingPong(Time.time * moveSpeedY * 0.8f, moveRangeY * 2) - moveRangeY;
            gameObject.transform.position = new Vector3(newX, newY, gameObject.transform.position.z);

            yield return null;
        }
    }

    public void GameOver()
    {
        if (objectCount > 6)
        {
            gameOver.SetActive(true);
        }
    }

    public void TryAgain()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}