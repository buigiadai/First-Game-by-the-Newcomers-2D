using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//hàm dịch chuyển nhân vật

public class Skill02 : MonoBehaviour
{
    public Player playerS;
    public float dashBoost = 10f;
    public float dashTime = 2f;
    private float _dashTime;
    private bool isDashing;
    public SpriteRenderer spriteRenderer;
    public Color originalColor;

}   

