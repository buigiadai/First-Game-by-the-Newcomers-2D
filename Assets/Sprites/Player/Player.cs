// Bùi Gia Đại
// Chức năng: di chuyển wads, quay mặt nhân vật, chạy animation

//Lỗi: 
//   + Chưa resize được animation theo kích thước mong muốn: Nhân vật to

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float moveSepped = 2f;
    private Rigidbody2D rb;
    private Vector3 moveInput;
    private Animator animator;
    public Weapon weapon;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true; // Khóa rotation theo trục Z để ngăn Player bị xoay
        animator = GetComponent<Animator>();
        weapon = GameObject.FindGameObjectWithTag("Gun").GetComponent<Weapon>();
    }

    public void UpdateWeapon()
    {
        weapon = GameObject.FindGameObjectWithTag("Gun").GetComponent<Weapon>();
    }

    private void Update()
    {
        moveInput.x = Input.GetAxis("Horizontal");
        moveInput.y = Input.GetAxis("Vertical");
        transform.position += moveInput * moveSepped * Time.deltaTime;//tịnh tiến nhân vật

        if (moveInput.x != 0)
        {
            if (moveInput.x > 0)//quay mặt nhân vật
            {
                transform.localScale = new Vector3(1f, 1, 0);
                weapon.trucX = 1;
            }
            else
            {
                transform.localScale = new Vector3(-1f, 1, 0);
                weapon.trucX = -1;
            }
        }

        if (animator != null)
        {
            animator.SetFloat("Speed", moveInput.sqrMagnitude);
        }
    }

}
