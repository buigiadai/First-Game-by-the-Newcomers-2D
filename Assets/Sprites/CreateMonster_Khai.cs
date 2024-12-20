// Bùi Gia Đại + Khải
// Quản lý việc tạo quái vật

//Lỗi: 
//   + Chưa điều khiển được thời gian chờ giữa các lần spawn
//   + Chưa quản lý được thời gian hoạt ảnh quái xuất hiện

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateMonster_Khai : MonoBehaviour
{
    public GameObject monsterPrefab; // Prefab của quái vật
    private Transform player; // Tham chiếu tới người chơi
    public float spawnTime = 1.0f; // Thời gian giữa các lần spawn
    public float spawnRadius = 5.0f; // Bán kính spawn
    public TextMeshProUGUI monsterCountText; // Tham chiếu đến TextMeshPro để hiển thị số lượng quái
    private int monsterCount = 0; // Biến để theo dõi số lượng quái vật
    private int deadMonsterCount = 0; // Biến để theo dõi số lượng quái vật đã chết
    public const int maxMonsterCount = 30; // Giới hạn số lượng quái vật tối đa
    // public GameObject win; // Tham chiếu tới đối tượng win

    void Start()
    {
        InvokeRepeating("SpawnMonster", spawnTime, spawnTime); // Gọi hàm spawn định kỳ
        // UpdateMonsterCountText(); // Cập nhật text hiển thị số lượng quái ban đầu
        player = GameObject.FindGameObjectWithTag("Player").transform; // Tìm người chơi
    }

    void SpawnMonster()
    {
        // Kiểm tra số lượng quái vật hiện có
        if (monsterCount >= maxMonsterCount)
        {
            return; // Không tạo thêm quái vật nếu đã đủ số lượng tối đa
        }
        // Tạo vị trí ngẫu nhiên trong bán kính nhất định
        Vector2 spawnPosition = (Vector2)player.position + Random.insideUnitCircle * spawnRadius;

        // Tạo vị trí ngẫu nhiên trong bán kính nhất định
        // Vector2 spawnPosition = (Vector2)transform.position + Random.insideUnitCircle * spawnRadius;

        // Chuyển đổi vị trí sang 3D với tọa độ z = -1
        spawnPosition = new Vector3(spawnPosition.x, spawnPosition.y, -1f);

        // Tạo quái vật tại vị trí ngẫu nhiên
        GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);

        // Tăng số lượng quái vật   
        monsterCount++;
        // UpdateMonsterCountText(); // Cập nhật text hiển thị số lượng quái
    }

    // public void UpdateMonsterCountText()
    // {
    //     monsterCountText.text = monsterCount.ToString(); // Cập nhật text hiển thị số lượng quái
    // }
}
