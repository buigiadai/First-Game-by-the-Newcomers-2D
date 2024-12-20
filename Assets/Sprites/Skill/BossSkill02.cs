//Bùi Gia Đại
//Mô tả: Gọi quái con ra từ boss
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSkill02 : MonoBehaviour
{
    public GameObject minionPrefab; // Prefab của quái con
    public int minionCount = 3; // Số lượng quái con sẽ tạo ra mỗi lần dùng skill
    public float spawnRadius = 3f; // Bán kính tạo quái con xung quanh boss
    public float skillCooldown = 10f; // Thời gian hồi chiêu của skill

    private void Start()
    {
        // Tự động bắt đầu vòng lặp skill
        StartCoroutine(AutoUseSkill());
    }

    private IEnumerator AutoUseSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(skillCooldown); // Chờ thời gian hồi chiêu
            StartCoroutine(SpawnMinions());
        }
    }

    private IEnumerator SpawnMinions()
    {
        for (int i = 0; i < minionCount; i++)
        {
            // Tạo vị trí ngẫu nhiên trong bán kính xung quanh boss
            Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

            // Instantiate quái con tại vị trí spawnPosition
            Instantiate(minionPrefab, spawnPosition, Quaternion.identity);

            yield return new WaitForSeconds(0.2f); // Thời gian delay giữa các lần tạo
        }
    }

}
