// Bùi Gia Đại
//Tham số nhận:  maxDame, minDame, playerHealth, popUpDamegaPrefab, animator, isDead, monster, monsterHealth, currentHealth, maxHealth, damagePopupPosition, rotateCircle
// Xử lý tác động của quái vật lên người chơi, ảnh hưởng thanh máu của người chơi

using System.Collections;
using UnityEngine;
using TMPro;

public class MonsterController : MonoBehaviour
{
    private Player playerS;
    public int minDamage;
    public int maxDamage;
    private PlayerHealth playerHealth;
    public GameObject popUpDamegaPrefab; // Prefab hiển thị sát thương
    private Animator animator; // Để quản lý hoạt ảnh
    private bool isDead = false; // Trạng thái chết
    private Monster monster;
    [SerializeField] MonsterHealth monsterHealth;
    public Transform damagePopupPosition; // Vị trí xuất hiện sát thương trên đầu quái vật
    [SerializeField] float currentHealth, maxHealth = 100f;
    public RotateCircle rotateCircle;
    private MenuMonster menuMonster;

    public bool boss;

    private void Start()
    {
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        animator = GetComponent<Animator>();
        monster = GetComponent<Monster>();
        monsterHealth = GetComponent<MonsterHealth>();
        if (monsterHealth == null)
        {
            Debug.LogError("MonsterHealth component is missing on " + gameObject.name);
        }
        currentHealth = maxHealth;
        monsterHealth.SetHealth(currentHealth, maxHealth);
        rotateCircle = GetComponentInChildren<RotateCircle>();
        menuMonster = GameObject.Find("EventSystem").GetComponent<MenuMonster>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!isDead && collision.CompareTag("Player"))
        {
            playerS = collision.gameObject.GetComponent<Player>();
            if (playerS != null)
            {
                InvokeRepeating("DamagePlayer", 0, 1f); // Gây sát thương 1 giây khi va chạm
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            playerS = null;
            CancelInvoke("DamagePlayer");
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return; // Không nhận sát thương nếu đã chết

        // Debug.Log("Monster take damage: " + monsterHealth + ", Current Health: " + monsterHealth.CurrentHealth);
        currentHealth -= damage;
        monsterHealth.SetHealth(currentHealth, maxHealth);

        if (rotateCircle != null)
            rotateCircle.ShowCircleOnDamage();

        // Hiển thị sát thương
        // GameObject instance = Instantiate(popUpDamegaPrefab, damagePopupPosition.position, Quaternion.identity);
        // instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();
        GameObject instance = Instantiate(popUpDamegaPrefab,
                 transform.position + new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), 0.7f, 0),
                 Quaternion.identity);
        instance.GetComponentInChildren<TextMeshProUGUI>().text = damage.ToString();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if (isDead) return;
        isDead = true;
        monsterHealth.RemoveHealthBar(); // Xoá thanh máu
        monster.Die(); // Ngừng di chuyển quái vật
        animator.SetBool("isDead", true); // Kích hoạt trạng thái chết
        CancelInvoke("DamagePlayer"); // Ngừng gây sát thương lên người chơi
        menuMonster.MonsterDie(); // Cập nhật số lượng quái vật đã chết
        Destroy(gameObject, 0.5f); // Xóa đối tượng sau 0.5 giây

        if (boss)
        {
            MenuMap menuMap = GameObject.Find("EventSystem").GetComponent<MenuMap>();
            menuMap.Win();
        }
    }

    void DamagePlayer()
    {
        if (playerHealth != null)
        {
            int damage = Random.Range(minDamage, maxDamage);
            playerHealth.TakeDamage(damage);
        }
    }
}
