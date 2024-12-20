//Bùi Gia Đại
//Hiển thị súng hiện tại đang được sử dụng

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponDisplay : MonoBehaviour
{
    public Image weaponImage; // UI Image hiển thị vũ khí

    public void ChangeSprite(GameObject newWeaponPrefab)
    {
        if (newWeaponPrefab == null)
        {
            Debug.LogError("New weapon prefab is null!");
            return;
        }

        // Lấy SpriteRenderer từ prefab
        SpriteRenderer spriteRenderer = newWeaponPrefab.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("New weapon prefab không có SpriteRenderer!");
            return;
        }

        // Gán sprite vào UI Image
        if (weaponImage != null)
        {
            weaponImage.sprite = spriteRenderer.sprite;

            // In thông tin sau khi thay đổi UI Image
            if (weaponImage.sprite != null)
            {
                Debug.Log("Weapon UI sprite changed to: " + weaponImage.sprite.name);
            }
            else
            {
                Debug.LogError("UI Image không được cập nhật sprite!");
            }
        }
        else
        {
            Debug.LogError("Weapon image UI component is not assigned!");
        }
    }
}
