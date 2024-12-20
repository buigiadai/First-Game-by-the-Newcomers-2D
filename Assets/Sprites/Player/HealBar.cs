//Bùi Gia Đại
//Tham số nhận: hình ảnh thanh máu, giá trị txt thanh máu,img heath tốt, img heath bình thường, img heath thấp, 
// giá trị biến trên, giá trị biến dưới

//Chức năng: cập nhập hình ảnh và chữ của healthBar

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealBar : MonoBehaviour
{
    public Image fillBar;//thanh máu
    public TextMeshProUGUI valueText;//giá trị txt thanh máu
    public Sprite goodHealthSprite; // hình ảnh thanh máu khi máu tốt
    public Sprite normalHealthSprite; // hình ảnh thanh máu khi máu bình thường
    public Sprite lowHealthSprite;// hình ảnh thanh máu khi máu thấp
    public int giaTriBienTren = 60;//giá trị biến trên
    public int giaTriBienDuoi = 30;//giá trị biến dưới

    public void UpdateBar(int currentValue, int maxValue)
    {
        fillBar.fillAmount = (float)currentValue / (float)maxValue; //cập nhật thanh máu
        valueText.text = currentValue.ToString() + "/" + maxValue.ToString();
        // Debug.Log("Health: " + currentValue);

        if (currentValue < giaTriBienDuoi)
            fillBar.sprite = lowHealthSprite;
        else
        {
            if (currentValue <= giaTriBienTren)
                fillBar.sprite = normalHealthSprite;
            else
                fillBar.sprite = goodHealthSprite;
        }


    }
}
