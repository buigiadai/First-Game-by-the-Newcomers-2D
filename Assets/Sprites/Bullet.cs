//Bùi Gia Đại + Huy

//Quản lý viên đạn: xử lý va chạm viên đạn

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Bullet : MonoBehaviour
{
    public int damage;
    public bool typeBullet;//Đạn của quái hoặc người chơi! => Tái sử dụng code
                           //Đạn người chơi: true
                           //Đạn quái: false
    private MapSaveData mapSaveData;

    public GameObject hitEffect; // Hiệu ứng khi đạn va chạm
    // public GameObject popUpDamegaPrefab; // Prefab hiển thị sát thương
    // public TMP_Text popUpText; // Hiển thị sát thương

    public float lifeTime = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        // Hủy viên đạn sau khoảng thời gian nhất định
        Destroy(gameObject, lifeTime);
        mapSaveData = GameObject.Find("SaveLoadManager").GetComponent<MapSaveData>();
        damage = mapSaveData.getDame();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !typeBullet) //Trúng player + đạn từ quái
        {
            Debug.Log("Player bị trúng đạn quái");
            // int dame = Random.Range(minDame, maxDame);
            // collision.GetComponent<playerHealth>().TakeDame(dame);
            Destroy(gameObject);
        }
        else
        if (collision.CompareTag("Monster") && typeBullet) //Đạn từ player trúng quái
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); // Hiển thị hiệu ứng khi va chạm đạn
            Destroy(effect, 0.5f); // Hiệu ứng biến mất sau 0.5 giây
            Destroy(gameObject);

            collision.GetComponent<MonsterController>().TakeDamage(damage);// gây đam đến quái 
        }
        else
        if (collision.CompareTag("Obstacle")) // Trúng tường
        {
            GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity); // Hiển thị hiệu ứng khi va chạm đạn
            Destroy(effect, 0.5f); // Hiệu ứng biến mất sau 0.5 giây
            Destroy(gameObject);
        }
    }

}
