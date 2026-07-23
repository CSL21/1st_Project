using UnityEngine;
using System.Collections;

public class SpawnManager : MonoBehaviour
{
    [Header("Spawn Settings")]
    [SerializeField] private string enemyPoolName = "Enemy_Goblin";
    [SerializeField] private float spawnInterval = 3f;
    [SerializeField] private int maxEnemyCount = 10;

    [Header("Spawn Position")]
    [SerializeField] private float spawnXPosition = 15f;
    [SerializeField] private float fixedYPosition = -4.2f;
    [SerializeField] private float maxSpawnPlayerX = 80f;

    private Transform playerTransform;
    private bool isSpawning = false;

    private void Start()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            playerTransform = player.transform;
        }
        StartSpawn();
    }

    public void StartSpawn()
    {
        if (!isSpawning)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    public void StopSpawn()
    {
        isSpawning = false;
        StopAllCoroutines();
    }

    private IEnumerator SpawnRoutine()
    {
        isSpawning = true;

        while (isSpawning)
        {
            yield return new WaitForSeconds(spawnInterval);

            if (playerTransform == null) continue;

            if (playerTransform.position.x >= maxSpawnPlayerX)
            {
                Debug.Log($"플레이어가 맵 끝 부근({playerTransform.position.x})에 도달하여 스폰을 중단합니다.");
                continue;
            }


            int currentEnemyCount = CountAliveEnemies();

            if (currentEnemyCount >= maxEnemyCount)
            {
                Debug.Log($"필드에 몹이 {currentEnemyCount}마리 쌓여있어 스폰을 임시 제한합니다.");
                continue;
            }

            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        if (playerTransform == null) return;

        GameObject enemy = ObjectPoolManager.instance.GetObject(enemyPoolName);
        if (enemy != null)
        {
            float calculatedX = playerTransform.position.x + spawnXPosition;

            Vector3 spawnPos = new Vector3(calculatedX, fixedYPosition, 0f);

            enemy.transform.position = spawnPos;
        }
    }

    private int CountAliveEnemies()
    {
        int enemyLayer = LayerMask.NameToLayer("Enemy");

        if (enemyLayer == -1)
        {
            return GameObject.FindGameObjectsWithTag("Enemy").Length;
        }

        EnemyController[] currentEnemies = Object.FindObjectsByType<EnemyController>(FindObjectsSortMode.None);
        return currentEnemies.Length;
    }
}