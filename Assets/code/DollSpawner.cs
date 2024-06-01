

using System.Collections;
using UnityEngine;

public class DollSpawner : MonoBehaviour
{
    // 스폰할 프리팹을 지정합니다.
    public GameObject prefab;

    // 스폰할 개수를 지정합니다.
    public int numberOfPrefabs = 100;
    public float spawnMinX = -0.3f;
    public float spawnMinY = -0.3f;
    public float spawnMinZ = -0.3f;
    public float spawnMaxX = 0.3f;
    public float spawnMaxY = 0.3f;
    public float spawnMaxZ = 0.3f;
    // 스폰할 위치의 범위를 지정합니다.



    public float spawnDelay = 0.5f;
    public float startDelay = 2.0f;

    void Start()
    {
        // 코루틴을 시작합니다.
        StartCoroutine(SpawnPrefabsWithDelay());
    }

    IEnumerator SpawnPrefabsWithDelay()
    {
        yield return new WaitForSeconds(startDelay);

        for (int i = 0; i < numberOfPrefabs; i++)
        {
            SpawnPrefab();
            // 스폰 간의 딜레이 시간을 기다립니다.
            yield return new WaitForSeconds(spawnDelay);
        }
    }


    void SpawnPrefab()
    {
        // 스폰할 위치를 랜덤하게 결정합니다.
        float x = Random.Range(spawnMinX, spawnMaxX);
        float y = Random.Range(spawnMinY, spawnMaxY);
        float z = Random.Range(spawnMinZ, spawnMaxZ);
        Vector3 spawnPosition = new Vector3(x, y, z);

        // 프리팹을 스폰합니다.
        Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}