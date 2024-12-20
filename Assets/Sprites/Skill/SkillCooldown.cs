//Youtube:https://www.youtube.com/watch?v=1fBKVWie8ew
//Kỹ thuật: Making a Cooldown UI

//Bùi Gia Đại
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillCooldown : MonoBehaviour
{
    [SerializeField] private Image imageCooldown;        // Hình tròn hiệu ứng hồi chiêu
    [SerializeField] private TMP_Text textCooldownText;  // Text hiển thị thời gian còn lại
    private bool isCooldown = false;                     // Kiểm tra kỹ năng đang hồi chiêu
    private float cooldownTime;                          // Thời gian hồi chiêu
    private float cooldownTimer = 0f;                    // Bộ đếm thời gian hồi chiêu

    void Start()
    {
        textCooldownText.gameObject.SetActive(false);    // Ẩn text ban đầu
        imageCooldown.fillAmount = 0f;                   // Đặt hiệu ứng hồi chiêu ban đầu
    }

    void Update()
    {
        if (isCooldown)
        {
            ApplyCooldown();
        }
    }

    // Áp dụng hiệu ứng hồi chiêu
    void ApplyCooldown()
    {
        cooldownTimer -= Time.deltaTime;
        if (cooldownTimer <= 0)
        {
            isCooldown = false;
            textCooldownText.gameObject.SetActive(false);
            imageCooldown.fillAmount = 0f;
        }
        else
        {
            imageCooldown.fillAmount = cooldownTimer / cooldownTime;
            textCooldownText.text = Mathf.Ceil(cooldownTimer).ToString(); // Làm tròn lên
        }
    }

    // Kích hoạt kỹ năng và đặt thời gian hồi chiêu động
    public void UseSpell(float customCooldownTime)
    {
        if (isCooldown)
        {
            // Nếu đang hồi chiêu, không cho sử dụng kỹ năng
            return;
        }
        else
        {
            isCooldown = true;
            cooldownTime = customCooldownTime;          // Gán thời gian hồi chiêu động
            cooldownTimer = cooldownTime;              // Đặt bộ đếm bằng thời gian hồi chiêu
            textCooldownText.gameObject.SetActive(true);
        }
    }
}
