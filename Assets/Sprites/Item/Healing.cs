//Bùi Gia Đại
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public HealBar healBar;
    public float rotationSpeed = 200f; // Tốc độ xoay
    public PlayerHealth playerHealth;

    public int healValue = 10;
    void Start()
    {
        healBar = GameObject.Find("HealthBar").GetComponent<HealBar>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
    }

    void Update()
    {
        // Xoay quanh trục Y
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerHealth.Heal(healValue);
            Debug.Log("Hồi máu!");
            Destroy(gameObject);
        }
    }
}
