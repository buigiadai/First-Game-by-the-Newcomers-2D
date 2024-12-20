using UnityEngine;

public class BossController : MonoBehaviour
{
    // Các trạng thái của Boss
    public enum BossState { Idle, Moving, Attacking, Dead }
    public BossState currentState = BossState.Idle;

    // Các thông số
    public float moveSpeed = 2.0f; // Tốc độ di chuyển
    public float damage = 10f; // Sát thương gây ra
    public float health = 100f; // Máu của Boss
    public Vector2 moveDirection; // Hướng di chuyển (x, y)

    // Tham chiếu
    public Transform target; // Mục tiêu (người chơi)
    public Animator animator; // Animator để quản lý animation

    void Start()
    {
        if (!animator) animator = GetComponent<Animator>();
    }

    void Update()
    {
        switch (currentState)
        {
            case BossState.Idle:
                Idle();
                break;
            case BossState.Moving:
                Move();
                break;
            case BossState.Attacking:
                Attack();
                break;
            case BossState.Dead:
                Die();
                break;
        }
    }

    private void Idle()
    {
        // Animation Idle
        animator.SetTrigger("Idle");
    }

    private void Move()
    {
        // Animation Moving
        animator.SetTrigger("Move");

        // Di chuyển theo hướng moveDirection
        Vector3 movement = new Vector3(moveDirection.x, moveDirection.y, 0) * moveSpeed * Time.deltaTime;
        transform.position += movement;
    }

    private void Attack()
    {
        // Animation Attacking
        animator.SetTrigger("Attack");

        // Logic gây sát thương
        if (target != null)
        {
            Debug.Log("Boss tấn công mục tiêu, gây sát thương: " + damage);
            // Thêm logic gây sát thương vào người chơi
        }
    }

    public void TakeDamage(float amount)
    {
        health -= amount;
        Debug.Log("Boss bị đánh, còn lại: " + health + " máu.");

        if (health <= 0)
        {
            currentState = BossState.Dead;
        }
    }

    private void Die()
    {
        // Animation Dead
        animator.SetTrigger("Die");
        Debug.Log("Boss đã chết.");

        // Logic xử lý khi Boss chết (ví dụ: phá hủy GameObject)
        Destroy(gameObject, 2.0f); // Phá hủy sau 2 giây
    }

    // Gọi khi Boss muốn chuyển đổi trạng thái
    public void ChangeState(BossState newState)
    {
        currentState = newState;
    }
}
