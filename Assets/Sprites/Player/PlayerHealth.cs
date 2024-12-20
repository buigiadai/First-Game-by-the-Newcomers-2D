//Bùi Gia Đại
//Tham số nhận: giá trị tối đa, thanh máu, thời gian bất tử
//Liên kêt với: HealBar.cs

//Mô tả: Script xử lý máu của nhân vật chính
//Thanh máu được cập nhật theo máu hiện tại và máu tối đa
//Khi nhận sát thương, máu giảm và cập nhật thanh máu
//Khi máu = 0, nhân vật chết
//Có thời gian bất tử sau khi nhận sát thương


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerHealth : MonoBehaviour
{
    private PlayerFlash playerFlash; // Tham chiếu đến script PlayerFlash

    [SerializeField] public int maxHealth;// máu tối đa được tryền vào
    public int currentHealth;// txt máu hiện tại
    public HealBar healBar;// thanh máu
    public float safeTime = 2f;// thời gian giảm sốc giữa các damage
    float _saveTimeCooldown;//Biến đếm thời gian bất tử
    public UnityEvent OnDeath;// Event khi chết
    private Animator animator; // Để quản lý hoạt ảnh

    public MenuMap menuMap; // Tham chiếu đến script MenuMap

    void Start()
    {
        currentHealth = maxHealth;// khởi tạo máu
        healBar.UpdateBar(currentHealth, maxHealth);// cập nhật thanh máu
        playerFlash = GetComponent<PlayerFlash>();
        animator = GetComponent<Animator>();
        menuMap = GameObject.Find("EventSystem").GetComponent<MenuMap>();
        // Debug.Log(menuMap);
    }

    private void OnEnable()// sự kiện khi kích hoạt
    {
        // thêm sự kiện khi chết
        OnDeath.AddListener(Death);
    }

    public void Death()// chết
    {
        Debug.Log("Player chết. PlayerHealth.cs");
        // animator.SetBool("isDead", true); // Kích hoạt trạng thái chết
        // gameObject.SetActive(false);
        Time.timeScale = 0;
        menuMap.Lose();
    }

    public void TakeDamage(int damage)// nhận sát thương
    {
        if (_saveTimeCooldown <= 0)// kiểm tra thời gian bất tử
        {
            currentHealth -= damage;
            playerFlash.Flash();// nhấp nháy khi nhận sát thương
            if (currentHealth <= 0)
            {
                currentHealth = 0;
                OnDeath.Invoke();
            }
            healBar.UpdateBar(currentHealth, maxHealth);
            _saveTimeCooldown = safeTime;
        }
    }

    public void Heal(int heal)// hồi máu
    {
        currentHealth += heal;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        healBar.UpdateBar(currentHealth, maxHealth);
    }

    private void Update()
    {
        _saveTimeCooldown -= Time.deltaTime;// đếm thời gian
        // test thử sát thương
        // if (Input.GetKeyDown(KeyCode.Space))
        // {
        //     TakeDamage(20);
        // }
    }

}
