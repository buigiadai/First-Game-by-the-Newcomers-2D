// Chatgpt: https://chatgpt.com/c/673e3535-8470-800f-822d-b62ff88967b5
// Bùi Gia Đại


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro; // Đảm bảo thư viện này có mặt

public class ChestController : MonoBehaviour
{
    public SpriteRenderer chestSpriteRenderer; // Gắn SpriteRenderer của rương
    public Sprite[] chestSprites; // Danh sách ảnh: [0] bình thường, [1] trúng đạn, [2] mở rương
    public GameObject timerTextObject; // Đối tượng chứa TextMeshPro
    public TextMeshPro timerText; // Đối tượng TextMeshPro 3D để hiển thị bộ đếm thời gian
    public float shakeIntensity = 0.1f; // Độ rung
    public float shakeDuration = 0.5f;  // Thời gian rung
    private Vector3 originalPosition;   // Lưu vị trí ban đầu
    private int bulletCount = 0;         // Đếm số đạn trúng rương
    public int maxBullets = 5;          // Số đạn cần để mở rương
    private bool isChestOpen = false;   // Trạng thái xem rương đã mở hay chưa
    private float resetTime = 60f;      // Thời gian reset sau khi rương mở (1 phút)

    private bool isResetting = false; // Để tránh reset khi đang trong quá trình
    private float shakeTime = 0f; // Thời gian còn lại để rung

    public ItemSpawner itemSpawner;

    private void Start()
    {
        // Lưu trạng thái ban đầu
        originalPosition = transform.position;

        // Đặt ảnh mặc định là ảnh bình thường
        if (chestSprites.Length > 0)
        {
            chestSpriteRenderer.sprite = chestSprites[0];
        }

        // Kiểm tra xem đã gán đầy đủ các đối tượng chưa
        if (timerText == null)
        {
            Debug.LogError("timerText chưa được gán trong Inspector!");
        }

        if (timerTextObject == null)
        {
            Debug.LogError("timerTextObject chưa được gán trong Inspector!");
        }

        // Ẩn bộ đếm thời gian ban đầu
        timerTextObject.SetActive(false);

        chestSpriteRenderer.sprite = chestSprites[0];
    }

    private void Update()
    {
        // Nếu rương đã mở, bắt đầu đếm ngược thời gian
        if (isChestOpen)
        {
            resetTime -= Time.deltaTime;
            UpdateTimerDisplay();

            // Nếu hết 1 phút, reset rương và cho phép tác động vào nó nữa
            if (resetTime <= 0)
            {
                ResetChestState();
            }
        }

        // Kiểm tra nếu rương cần rung
        if (shakeTime > 0)
        {
            transform.position = originalPosition + (Vector3)Random.insideUnitCircle * shakeIntensity;
            shakeTime -= Time.deltaTime;
        }
        else
        {
            transform.position = originalPosition; // Đảm bảo rương trở lại vị trí ban đầu
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet_Dan")) // Kiểm tra va chạm với đạn
        {
            if (!isChestOpen) // Chỉ thực hiện khi rương chưa mở
            {
                // Thay ảnh rương thành ảnh bị trúng đạn
                chestSpriteRenderer.sprite = chestSprites[1];

                // Gây hiệu ứng rung
                shakeTime = shakeDuration;

                // Đếm số lần trúng đạn
                bulletCount++;

                // Kiểm tra nếu đủ số đạn để mở rương
                if (bulletCount >= maxBullets && !isChestOpen)
                {
                    OpenChest();
                }
            }
        }
    }

    private void OpenChest()
    {
        // Đổi ảnh thành ảnh mở rương
        chestSpriteRenderer.sprite = chestSprites[2];

        // Đánh dấu rương đã mở
        isChestOpen = true;

        // Thay đổi tag của rương sau khi mở để tránh tác động tiếp
        gameObject.tag = "Untagged";

        // Hiển thị bộ đếm thời gian
        timerTextObject.SetActive(true);

        // Đặt lại thời gian đếm ngược
        resetTime = 60f;

        // Random vật phẩm từ ItemSpawner
        if (itemSpawner != null)
        {
            // Vị trí vật phẩm rơi ra cao hơn rương một chút
            Vector3 spawnPosition = transform.position + new Vector3(0, 1f, 0); // 1f là độ cao tăng thêm
            itemSpawner.SpawnRandomItem(spawnPosition);
        }

    }

    private void UpdateTimerDisplay()
    {
        if (timerText != null) // Kiểm tra xem có gán đối tượng TextMeshPro chưa
        {
            int minutes = Mathf.FloorToInt(resetTime / 60);
            int seconds = Mathf.FloorToInt(resetTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else
        {
            Debug.LogError("timerText không được gán trong UpdateTimerDisplay!");
        }
    }

    private void ResetChestState()
    {
        // Đặt lại trạng thái của rương
        isChestOpen = false;
        bulletCount = 0;

        // Ẩn bộ đếm thời gian
        timerTextObject.SetActive(false);

        // Đặt lại ảnh ban đầu cho rương
        chestSpriteRenderer.sprite = chestSprites[0];

        // Đặt lại thời gian reset
        resetTime = 60f;

        gameObject.tag = "Obstacle"; // Đặt lại tag của rương để tránh tác động tiếp

        // Debug.Log("Rương đã reset và có thể tác động lại!");
    }
}
