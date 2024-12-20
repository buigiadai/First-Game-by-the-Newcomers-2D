//Bùi Gia Đại
//Tham số nhận: giá trị tăng tốc, thời gian tăng tốc
//Chức năng: tăng tốc độ di chuyển của nhân vật trong một khoảng thời gian

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill01 : MonoBehaviour
{
    public float skillSpeed = 10f;
    public float skillTime = 2f;
    private float _skillTime;
    private bool isDashing;
    public Player playerS;
    private SkillCooldown skillCooldown;
    void Start()
    {
        playerS = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        skillCooldown = GameObject.Find("SkillPlayer").GetComponent<SkillCooldown>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _skillTime <= 0 && !isDashing)
        {
            Debug.Log("Kỹ năng tăng tốc chạy");
            playerS.moveSepped += skillSpeed;
            _skillTime = skillTime;
            isDashing = true;
            skillCooldown.UseSpell(skillTime);
        }
        if (_skillTime <= 0 && isDashing)
        {
            playerS.moveSepped -= skillSpeed;
            isDashing = false;
        }
        else
        {
            _skillTime -= Time.deltaTime;
        }
    }
}
