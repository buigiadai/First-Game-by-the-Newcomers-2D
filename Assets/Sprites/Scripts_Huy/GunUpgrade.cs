using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunUpgrade : MonoBehaviour
{
    public float blinkInterval = 0.3f; // Thời gian giữa các lần nhấp nháy
    private SpriteRenderer spriteRenderer;
    private bool isBlinking = false;
    public float rotationSpeed = 200f; // Tốc độ xoay

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(Blink());
    }

    private IEnumerator Blink()
    {
        while (true)
        {
            isBlinking = !isBlinking;
            spriteRenderer.enabled = isBlinking; // Bật/tắt ánh sáng
            yield return new WaitForSeconds(blinkInterval);
        }
    }
    

    void Update()
    {
        // Xoay quanh trục Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    public GameObject newBulletPrefab; // Prefab viên đạn mới

    public GameObject newBulletBomb;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Gun"))
        {
            // Gọi phương thức cập nhật viên đạn
            
            other.GetComponent<Weapon>().UpdateBulletPrefab(newBulletPrefab);
            other.GetComponent<Weapon>().UpdateBulletBomb(newBulletBomb);
            Destroy(gameObject); // Hủy đối tượng nâng cấp
        }
    }
   
}

