// Bùi Gia Đại

using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public List<GameObject> itemPrefabs; // Danh sách các vật phẩm
    public GameObject itemGroup; // Empty group chứa vật phẩm

    public void SpawnRandomItem(Vector3 spawnPosition)
    {
        if (itemPrefabs.Count == 0)
        {
            Debug.LogError("Danh sách vật phẩm trống!");
            return;
        }

        // Random vật phẩm
        // Random vật phẩm từ danh sách
        int randomIndex = Random.Range(0, itemPrefabs.Count);
        GameObject prefabToSpawn = itemPrefabs[randomIndex];

        // Tạo instance từ Prefab
        GameObject spawnedItem = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);

        // Đặt vật phẩm vào group quản lý (nếu có)
        if (itemGroup != null)
        {
            spawnedItem.transform.SetParent(itemGroup.transform, true); // `true` để giữ nguyên vị trí
        }

        Debug.Log($"Đã mở rương và kích hoạt vật phẩm: {spawnedItem.name}");
    }
}
