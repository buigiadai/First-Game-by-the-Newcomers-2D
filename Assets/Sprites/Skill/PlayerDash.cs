//Chatgpt: https://chatgpt.com/c/67421fc2-d89c-800f-b58c-789552db9e47
//Các video đa số sẽ chỉ tạo animation cho nhân vật, điều này khá tốn thời gian và công sức. 
//Tuy nhiên, hiệu ứng bóng mờ (after image) cho nhân vật một cách dễ dàng hơn bằng cách tạo một prefab AfterImageEffect.
//Việc này tối đa hóa sự linh hoạt và giảm thời gian cần thiết để tạo hiệu ứng bóng mờ cho nhân vật.

//Bùi Gia Đại
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    [Header("Dash Settings")]
    public float dashSpeed = 5f;// Tốc độ dash
    public float dashTime = 0.2f; // Thời gian dash
    public float dashCooldown = 1f; // Thời gian cooldown giữa các lần dash

    [Header("After Image Settings")]
    public GameObject afterImagePrefab; // Prefab bóng mờ
    public float afterImageSpawnRate = 0.01f; // Tốc độ tạo bóng mờ

    private Rigidbody2D rb;
    private Vector2 dashDirection;
    private bool isDashing = false;
    private float dashCooldownTimer = 0f;
    private float afterImageSpawnTimer = 0f;
    private SkillCooldown skillCooldown;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        skillCooldown = GameObject.Find("SkillDash").GetComponent<SkillCooldown>();
    }

    void Update()
    {
        if (dashCooldownTimer > 0)
        {
            dashCooldownTimer -= Time.deltaTime;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && dashCooldownTimer <= 0 && !isDashing)
        {
            StartDash();
            skillCooldown.UseSpell(dashCooldown);
        }

        // Tạo bóng mờ khi đang dash
        if (isDashing)
        {
            afterImageSpawnTimer -= Time.deltaTime;
            if (afterImageSpawnTimer <= 0)
            {
                SpawnAfterImage();
                afterImageSpawnTimer = afterImageSpawnRate;
            }
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            rb.velocity = dashDirection * dashSpeed;
        }
    }

    private void StartDash()
    {
        isDashing = true;
        dashCooldownTimer = dashCooldown;

        dashDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        if (dashDirection == Vector2.zero)
        {
            dashDirection = Vector2.right;
        }

        Invoke(nameof(StopDash), dashTime);
    }

    private void StopDash()
    {
        isDashing = false;
        rb.velocity = Vector2.zero;
    }

    private void SpawnAfterImage()
    {
        GameObject afterImage = Instantiate(afterImagePrefab, transform.position, Quaternion.identity);
        AfterImageEffect effect = afterImage.GetComponent<AfterImageEffect>();

        if (effect != null)
        {
            // Lấy sprite từ nhân vật
            SpriteRenderer playerSprite = GetComponent<SpriteRenderer>();
            effect.sprite = playerSprite.sprite;

            effect.startColor = Color.white;
        }
    }
}
