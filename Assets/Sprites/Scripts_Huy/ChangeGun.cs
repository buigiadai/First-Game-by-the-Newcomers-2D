
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ChangeGun : MonoBehaviour
// {   public GameObject newWeaponPrefab; // Đối tượng súng mới
//     private GameObject currentWeapon;   // Đối tượng súng hiện tại
//     public float rotationSpeed = 200f; // Tốc độ xoay

//     void Start()
//     {
//         // Tìm đối tượng "Weapon" theo tag
//         currentWeapon = GameObject.FindGameObjectWithTag("Gun");

//         if (currentWeapon == null)
//         {
//             Debug.LogError("Weapon object not found with tag 'Weapon'.");
//             return; // Thoát khỏi hàm Start để tránh lỗi
//         }

//         Debug.Log("Current weapon set successfully.");
//     }

//     void Update()
//     {
//         // Xoay quanh trục Y
//         transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
//         currentWeapon = GameObject.FindGameObjectWithTag("Gun");
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             if (currentWeapon != null)
//             {
//                 Destroy(currentWeapon);

//                 GameObject newWeapon = Instantiate(newWeaponPrefab, other.transform);
//                 newWeapon.tag = "Gun"; // Gán tag cho vũ khí mới

//                  // Gắn vũ khí mới vào Player mà không thừa hưởng scale của Player
//                 newWeapon.transform.SetParent(other.transform, false);

//                 // Đặt lại scale của vũ khí về (1, 1, 1)
//                 newWeapon.transform.localScale = Vector3.one;

//                 newWeapon.transform.localPosition = Vector3.zero;

//                 currentWeapon = newWeapon;

//                 Debug.Log("Weapon upgraded successfully.");
//                  // Đặt lại velocity của Rigidbody2D của Player (để ngừng rơi)
//                 Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
//                 if (rb != null)
//                 {
//                     rb.velocity = new Vector2(rb.velocity.x, 0);  // Đặt lại tốc độ Y về 0 (ngừng rơi)
//                 }

//             }
//             else
//             {
//                 Debug.LogError("Current weapon is null. Upgrade failed.");
//             }

//             Destroy(gameObject);
//         }
//     }
// }
// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;

// public class ChangeGun : MonoBehaviour
// {
//     public GameObject newWeaponPrefab; // Đối tượng súng mới
//     private GameObject currentWeapon; // Đối tượng súng hiện tại
//     public float rotationSpeed = 200f; // Tốc độ xoay

//     void Start()
//     {
//         // Tìm đối tượng súng hiện tại
//         currentWeapon = GameObject.FindGameObjectWithTag("Gun");

//         if (currentWeapon == null)
//         {
//             Debug.LogError("Weapon object not found with tag 'Gun'.");
//             return;
//         }

//         Debug.Log("Current weapon set successfully.");
//     }

//     void Update()
//     {
//         // Xoay đối tượng ChangeGun để tạo hiệu ứng
//         transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
//         currentWeapon = GameObject.FindGameObjectWithTag("Gun");
//     }

//     void OnTriggerEnter2D(Collider2D other)
//     {
//         if (other.CompareTag("Player"))
//         {
//             if (currentWeapon != null)
//             {
//                 // Lưu hướng của nhân vật (scale X) để áp dụng cho súng mới
//                 Transform playerTransform = other.transform;
//                 float playerScaleX = playerTransform.localScale.x;

//                 // Hủy súng hiện tại
//                 Destroy(currentWeapon);

//                 // Tạo súng mới và gắn vào Player
//                 GameObject newWeapon = Instantiate(newWeaponPrefab, other.transform);
//                 newWeapon.tag = "Gun"; // Gán tag cho súng mới

//                 // Gắn súng mới vào Player mà không thừa hưởng scale của Player
//                 newWeapon.transform.SetParent(other.transform, false);

//                 // Đặt lại vị trí của súng (giữ vị trí mặc định)
//                 newWeapon.transform.localPosition = Vector3.zero;

//                 // Đặt scale X của súng mới dựa trên hướng của nhân vật
//                 newWeapon.transform.localScale = new Vector3(playerScaleX > 0 ? 1 : -1, 1, 1);

//                 currentWeapon = newWeapon;
//                 // Log ra tên đối tượng cha của súng mới
//                 if (newWeapon.transform.parent != null)
//                 {
//                     Debug.Log("New weapon's parent: " + newWeapon.transform.parent.name);
//                 }
//                 else
//                 {
//                     Debug.Log("New weapon has no parent.");
//                 }


//                 Debug.Log("Weapon upgraded successfully.");
//             }
//             else
//             {
//                 Debug.LogError("Current weapon is null. Upgrade failed.");
//             }

//             // Hủy đối tượng ChangeGun sau khi nhặt
//             Destroy(gameObject);
//         }
//     }
// }
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeGun : MonoBehaviour
{
    public GameObject newWeaponPrefab; // Đối tượng súng mới
    private GameObject currentWeapon; // Đối tượng súng hiện tại
    public float rotationSpeed = 200f; // Tốc độ xoay
    public WeaponDisplay weaponDisplay;

    void Start()
    {
        // Tìm đối tượng súng hiện tại
        currentWeapon = GameObject.FindGameObjectWithTag("Gun");

        if (currentWeapon == null)
        {
            Debug.LogError("Weapon object not found with tag 'Gun'.");
            return;
        }

        Debug.Log("Current weapon set successfully.");
    }

    void Update()
    {
        // Xoay đối tượng ChangeGun để tạo hiệu ứng
        transform.Rotate(0, rotationSpeed * Time.deltaTime, 0);
        currentWeapon = GameObject.FindGameObjectWithTag("Gun");

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (currentWeapon != null)
            {
                // Lưu các thuộc tính của súng cũ (scale và rotation)
                Vector3 oldWeaponScale = currentWeapon.transform.localScale;
                Quaternion oldWeaponRotation = currentWeapon.transform.rotation;

                // Lưu hướng của nhân vật (scale X) để áp dụng cho súng mới
                Transform playerTransform = other.transform;
                float playerScaleX = playerTransform.localScale.x;

                // Hủy súng hiện tại
                Destroy(currentWeapon);

                // Tạo súng mới và gắn vào Player
                GameObject newWeapon = Instantiate(newWeaponPrefab, other.transform);
                newWeapon.tag = "Gun"; // Gán tag cho súng mới

                // Gắn súng mới vào Player mà không thừa hưởng scale của Player
                newWeapon.transform.SetParent(other.transform, false);

                // Đặt lại vị trí của súng (giữ vị trí mặc định)
                newWeapon.transform.localPosition = new Vector3(0, -0.18f, 1);

                // Đảm bảo rằng scale của súng không bị ảnh hưởng bởi scale của player
                newWeapon.transform.localScale = new Vector3(1, 1, 1); // Đặt lại scale về (1, 1, 1)

                // Đặt scale X và Y của súng mới dựa trên hướng của nhân vật
                newWeapon.transform.localScale = new Vector3(playerScaleX > 0 ? 1 : -1, 1, 1);

                // Áp dụng lại các thuộc tính của súng cũ (scale và rotation)
                newWeapon.transform.localScale = oldWeaponScale;
                newWeapon.transform.rotation = oldWeaponRotation;

                // Nếu cần, bạn có thể áp dụng lại các thuộc tính khác như vị trí và các hiệu ứng từ Weapon.cs
                // Ví dụ: newWeapon.GetComponent<Weapon>().DoSomething();

                currentWeapon = newWeapon;


                //Bùi Gia Đại bổ sung
                weaponDisplay.ChangeSprite(newWeaponPrefab);

                Debug.Log("Weapon upgraded successfully.");
            }
            else
            {
                Debug.LogError("Current weapon is null. Upgrade failed.");
            }

            // Hủy đối tượng ChangeGun sau khi nhặt
            Destroy(gameObject);
        }
    }
}






