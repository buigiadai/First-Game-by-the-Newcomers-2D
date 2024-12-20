// Bùi Gia Đại
// Mô tả: Kỹ năng tăng tốc độ di chuyển trong 1 khoảng thời gian nhất định
using System.Collections;
using UnityEngine;

public class BossSkillSpeedBoost : MonoBehaviour
{
    public float speedBoostMultiplier = 1.5f; // Hệ số tăng tốc độ di chuyển
    public float boostDuration = 5f; // Thời gian duy trì tốc độ tăng cường
    public float skillCooldown = 15f; // Thời gian hồi chiêu

    private bool isBoosted = false; // Trạng thái tăng tốc
    private Monster monsterScript; // Tham chiếu đến script Monster
    private Animator animator; // Tham chiếu đến Animator

    private void Start()
    {
        monsterScript = GameObject.Find("Boss02").GetComponent<Monster>();
        if (monsterScript == null)
        {
            Debug.LogError("Boss is missing the Monster script.");
            return;
        }
        animator = GameObject.Find("Boss02").GetComponent<Animator>();
        // Tự động bắt đầu kỹ năng
        StartCoroutine(AutoUseSkill());
    }

    private IEnumerator AutoUseSkill()
    {
        while (true)
        {
            yield return new WaitForSeconds(skillCooldown); // Chờ thời gian hồi chiêu
            if (!isBoosted)
            {
                StartCoroutine(ActivateSpeedBoost());
            }
        }
    }

    private IEnumerator ActivateSpeedBoost()
    {
        isBoosted = true;
        // Kích hoạt trạng thái trong Animator
        animator.SetBool("SkillTangToc", true);

        float originalSpeed = monsterScript.speed; // Lưu lại tốc độ ban đầu
        monsterScript.speed *= speedBoostMultiplier; // Tăng tốc độ di chuyển

        yield return new WaitForSeconds(boostDuration); // Chờ thời gian tăng tốc

        // Tắt trạng thái trong Animator
        animator.SetBool("SkillTangToc", false);

        monsterScript.speed = originalSpeed; // Khôi phục tốc độ ban đầu
        isBoosted = false;
    }
}
