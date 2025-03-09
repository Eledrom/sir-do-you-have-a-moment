using UnityEngine;
using UnityEngine.UI;

public class ProjectileScript : MonoBehaviour
{
    private ObjectSpawner objectSpawner;

    private void Awake()
    {
        GameObject spawnerObject = GameObject.Find("spawner");

        if (spawnerObject != null)
        {
            objectSpawner = spawnerObject.GetComponent<ObjectSpawner>();
        }
        else
        {
            Debug.LogError("Spawner GameObject bulunamadý!");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int scoreToAdd = 0;

        objectSpawner.objectCount--;

        if (collision.gameObject.CompareTag("1HP"))
        {
            scoreToAdd = 50;
        }
        else if (collision.gameObject.CompareTag("2HP"))
        {
            scoreToAdd = 100;
        }
        else if (collision.gameObject.CompareTag("5HP"))
        {
            scoreToAdd = 150;
        }
        else if (collision.gameObject.CompareTag("4HP"))
        {
            scoreToAdd = 125;
        }
        else if (collision.gameObject.CompareTag("10HP"))
        {
            scoreToAdd = 100;
        }
        else if (collision.gameObject.CompareTag("14HP"))
        {
            scoreToAdd = 200;
        }

        if (scoreToAdd > 0)
        {
            GameManager.Instance.AddScore(scoreToAdd);
        }

        Destroy(collision.gameObject);
        Destroy(gameObject);
    }
}