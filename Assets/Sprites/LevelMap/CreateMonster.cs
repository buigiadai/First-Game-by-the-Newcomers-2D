// Tạo quái vật trong game
using System.Collections;
using UnityEngine;

public class CreateMonster : MonoBehaviour
{
    public GameObject monsterPrefab; // Prefab của quái vật
    public Transform player; // Tham chiếu tới người chơi
    private int monsterCount = 0; // Số lượng quái vật hiện tại
    public int maxMonsterCount = 0; // Số lượng quái vật cần tạo
    private float spawnTime = 1.0f; // Thời gian giữa các lần spawn
    public float spawnRadius = 5.0f; // Bán kính spawn

    // Sự kiện khi hoàn tất tạo quái vật
    public delegate void SpawnCompleteHandler();
    public event SpawnCompleteHandler OnSpawnComplete;

    public void StartSpawning(int monsterAmount, float spawnDelay)
    {
        maxMonsterCount = monsterAmount;
        spawnTime = spawnDelay;
        monsterCount = 0;
        StartCoroutine(SpawnMonsters());
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tìm người chơi
    }

    private IEnumerator SpawnMonsters()
    {
        while (monsterCount < maxMonsterCount)
        {
            SpawnMonster();
            yield return new WaitForSeconds(spawnTime);
        }

        // Thông báo khi hoàn thành tạo quái vật
        OnSpawnComplete?.Invoke();
    }

    private void SpawnMonster()
    {
        // Tạo vị trí ngẫu nhiên
        Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;
        spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, -1f);

        // Tạo quái vật
        Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

        monsterCount++;
    }
}
