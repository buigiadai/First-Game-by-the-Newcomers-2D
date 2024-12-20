//Bùi Gia Đại
//Mô tả: Kỹ năng của Boss, ném nhiều Prefab theo hướng cố định
using UnityEngine;

public class BossSkill01 : MonoBehaviour
{
    public GameObject skillPrefab; // Prefab được ném
    public float skillCooldown = 5f; // Thời gian hồi kỹ năng
    public float skillSpeed = 10f; // Tốc độ ném của Prefab
    public int numberOfProjectiles = 8; // Số lượng Prefab được ném
    private float cooldownTimer = 0f; // Bộ đếm thời gian hồi kỹ năng

    void Start()
    {
    }

    void Update()
    {
        // Tăng thời gian cooldown
        cooldownTimer += Time.deltaTime;

        // Kích hoạt kỹ năng khi đủ thời gian
        if (cooldownTimer >= skillCooldown)
        {
            cooldownTimer = 0f;
            ActivateSkill();
        }
    }

    private void ActivateSkill()
    {
        float angleStep = 360f / numberOfProjectiles; // Góc giữa các Prefab
        float angle = 0f;

        for (int i = 0; i < numberOfProjectiles; i++)
        {
            // Tính toán hướng ném dựa trên góc
            float projectileDirX = Mathf.Cos(angle * Mathf.Deg2Rad);
            float projectileDirY = Mathf.Sin(angle * Mathf.Deg2Rad);

            Vector2 projectileDirection = new Vector2(projectileDirX, projectileDirY).normalized;

            // Tạo Prefab tại vị trí của Boss (không cần bossCenter)
            GameObject projectile = Instantiate(skillPrefab, transform.position, Quaternion.identity);

            // Gán vận tốc cho Prefab
            Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.velocity = projectileDirection * skillSpeed;
            }

            // Xoay Prefab theo hướng ném
            float angleToRotate = Mathf.Atan2(projectileDirection.y, projectileDirection.x) * Mathf.Rad2Deg;
            projectile.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angleToRotate));

            // Chuyển sang góc tiếp theo
            angle += angleStep;
        }

        Debug.Log("Boss kích hoạt kỹ năng: Ném " + numberOfProjectiles + " vật thể!");
    }

}
