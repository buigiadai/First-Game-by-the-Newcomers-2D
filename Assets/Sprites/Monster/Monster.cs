// Khải +Bùi Gia Đại  
//Tham số nhận: tốc độ di chuyển
//Chức năng: Quái vật di chuyển theo hướng người chơi

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public float speed = 2.0f; // Tốc độ di chuyển
    private Transform player; // Tham chiếu tới người chơi
    public bool canMove = false; // Kiểm tra nếu quái vật có thể di chuyển

    void Start()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");// Tìm đối tượng có Tag là "Player" và gán nó vào player

        if (playerObject != null)
        {
            player = playerObject.transform; // Gán Transform của người chơi
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng nào có tag 'Player'!");
        }
    }

    public void Die()
    {
        canMove = false; // Dừng quái vật di chuyển
    }

    public void OnAppearAnimationEnd()// Hàm được gọi khi kết thúc hoạt ảnh xuất hiện
    {
        canMove = true; // Bắt đầu cho phép quái vật di chuyển
        // Debug.Log("Quái vật đã xuất hiện và có thể di chuyển.");
    }

    void Update()
    {
        if (canMove && player != null)
        {
            // Tính hướng đi đến người chơi
            Vector2 direction = (player.position - transform.position).normalized;

            // Di chuyển quái vật mượt mà về phía người chơi
            transform.position += (Vector3)direction * speed * Time.deltaTime;

            // Quay huong quái vật về phía người chơi
            // float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg; // Tính góc quay
            // Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90)); // Tạo Quaternion với góc quay
            // transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * speed); // Quay mặt quái vật

            // Quay mặt quái vật về phía người chơi bằng cách thay đổi localScale
            if (direction.x > 0) // Nếu quái vật đang di chuyển sang phải
            {
                transform.localScale = new Vector3(1f, 1f, 1f); // Quay mặt sang phải
            }
            else if (direction.x < 0) // Nếu quái vật đang di chuyển sang trái
            {
                transform.localScale = new Vector3(-1f, 1f, 1f); // Quay mặt sang trái
            }
        }
    }

}
