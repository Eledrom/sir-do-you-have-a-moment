using System.Collections;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public GameObject[] objects;
    public float spawnRatio;
    public float minThreshold;
    public float maxThreshold;

    public float moveSpeedX = 1f; // X ekseninde hareket h�z�
    public float moveSpeedY = 1f; // Y ekseninde hareket h�z�
    public float moveRangeX = 5f; // X eksenindeki hareket aral���
    public float moveRangeY = 3f; // Y eksenindeki hareket aral���

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

            // Ba�lang�� konumunu sakla
            Vector3 startPosition = gameObject.transform.position;

            // Objelerin hareketini her frame kontrol et
            StartCoroutine(MoveObject(gameObject, startPosition));

            // Objeyi 5 saniye sonra yok et
            Destroy(gameObject, 5f);

            // Yeni obje spawnlanmadan �nce bekleme s�resi
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
}