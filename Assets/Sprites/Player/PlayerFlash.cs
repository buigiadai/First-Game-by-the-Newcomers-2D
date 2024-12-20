// Code: https://noobtuts.com/unity/2d-player-flashing-effect
// Cách sử dụng: Thêm script vào GameObject chứa SpriteRenderer của Player, sau đó gọi hàm Flash() để nhấp nháy

//Bùi Gia Đại
//Tham số nhận: SpriteRenderer, màu gốc, màu nhấp nháy, thời gian nhấp nháy, số lần nhấp nháy

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFlash : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;// Tham chiếu đến SpriteRenderer
    private Color originalColor;// Màu gốc của SpriteRenderer
    public Color flashColor = Color.red; // Màu đỏ khi nhấp nháy
    public float flashDuration = 0.1f; // Thời gian nhấp nháy
    public int flashCount = 2; // Số lần nhấp nháy

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color; // Lưu màu gốc
    }

    public void Flash()
    {
        StartCoroutine(FlashEffect());
    }

    private IEnumerator FlashEffect()
    {
        for (int i = 0; i < flashCount; i++)
        {
            spriteRenderer.color = flashColor; // Đặt màu thành màu nhấp nháy
            yield return new WaitForSeconds(flashDuration); // Chờ thời gian nhấp nháy
            spriteRenderer.color = originalColor; // Đặt lại màu về màu gốc
            yield return new WaitForSeconds(flashDuration); // Chờ trước khi nhấp nháy tiếp
        }
    }
}