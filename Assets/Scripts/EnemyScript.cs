using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public GameObject[] enemies;
    public Vector2[] targetPositions;
    public float enemySpeed = 2f;
    public float moveRangeX = 0.5f;
    public float moveRangeY = 0.5f;
    public float moveSpeed = 1f;

    private GameObject[] spawnedEnemies;
    private bool[] reachedTarget;
    private Vector2[] startPositions;

    private void Start()
    {
        SpawnEnemies();
    }

    public void SpawnEnemies()
    {
        if (enemies.Length != targetPositions.Length)
        {
            Debug.LogError("Enemies ve Target Positions dizileri ayný uzunlukta olmalý!");
            return;
        }

        spawnedEnemies = new GameObject[enemies.Length];
        reachedTarget = new bool[enemies.Length];
        startPositions = new Vector2[enemies.Length];

        for (int i = 0; i < enemies.Length; i++)
        {
            if (ShouldSpawnEnemy(i))
            {
                Vector3 spawnPosition = new Vector3(targetPositions[i].x + Random.Range(-1f, 1f), targetPositions[i].y + Random.Range(-1f, 1f), 0);
                spawnedEnemies[i] = Instantiate(enemies[i], spawnPosition, Quaternion.identity);
                startPositions[i] = spawnPosition;
                reachedTarget[i] = false;
            }
        }
    }

    private void Update()
    {
        for (int i = 0; i < spawnedEnemies.Length; i++)
        {
            if (spawnedEnemies[i] != null)
            {
                if (!reachedTarget[i] && spawnedEnemies[i] != null)
                {
                    Vector3 target = new Vector3(targetPositions[i].x, targetPositions[i].y, spawnedEnemies[i].transform.position.z);
                    spawnedEnemies[i].transform.position = Vector3.MoveTowards(spawnedEnemies[i].transform.position, target, enemySpeed * Time.deltaTime);

                    if (Vector3.Distance(spawnedEnemies[i].transform.position, target) < 0.1f)
                    {
                        reachedTarget[i] = true;
                    }
                }
                else
                {
                    float newX = startPositions[i].x + Mathf.PingPong(Time.time * moveSpeed, moveRangeX * 2) - moveRangeX;
                    float newY = startPositions[i].y + Mathf.PingPong(Time.time * moveSpeed * 0.8f, moveRangeY * 2) - moveRangeY;
                    spawnedEnemies[i].transform.position = new Vector3(newX, newY, spawnedEnemies[i].transform.position.z);
                }
            }
        }
    }

    private bool ShouldSpawnEnemy(int index)
    {
        if ((index == 0 || index == 1) && GameManager.Instance.isStage1)
            return true;
        if ((index == 2 || index == 3) && GameManager.Instance.isStage2)
            return true;
        if ((index == 4 || index == 5) && GameManager.Instance.isStage3)
            return true;

        return false;
    }
}
