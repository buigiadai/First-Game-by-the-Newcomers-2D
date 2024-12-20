using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameObject bulletBomb;
    public GameObject bullet;
    public Transform firePos;
    public float timeBetweenShots = 0.2f;
    public float bulletSpeed = 50;
    private bool isUpgrade = false;

    private bool isAutoShoot = false;

    private float shotTimer;

    private AudioSource audioSource; // Biến để lưu AudioSource
    public AudioClip shootSound; // Âm thanh bắn thường
    public AudioClip autoShootSound; // Âm thanh bắn tự động

    public float autoShootDelay = 0.5f; // Thời gian delay giữa các phát bắn tự động
    private float nextAutoShootTime = 0f; // Thời gian cho lần bắn tự động tiếp theo

    public int trucX = 1;
    private AutoButton autoButton;
    void Start()
    {
        shotTimer = timeBetweenShots; // Khởi tạo timer

        // Lấy AudioSource từ đối tượng
        audioSource = GetComponent<AudioSource>();
        autoButton = GameObject.Find("EventSystem").GetComponent<AutoButton>();
    }
    void Update()
    {

        transform.localScale = new Vector3(trucX, 1, 1);  // Đảm bảo tỷ lệ là 1
        shotTimer -= Time.deltaTime;
        RotateGun();  // Hướng súng về chuột

        if (Input.GetKeyDown(KeyCode.E))
        {
            isAutoShoot = !isAutoShoot;
            autoButton.ChangeImage();
        }

        // Kiểm tra nếu ở chế độ tự động và thời gian đủ để bắn
        if (isAutoShoot && Time.time >= nextAutoShootTime)
        {
            Shoot(autoShootSound);  // Bắn theo hướng chuột
            shotTimer = timeBetweenShots;  // Reset thời gian giữa các lần bắn
            nextAutoShootTime = Time.time + autoShootDelay;  // Cập nhật thời gian bắn lần sau
        }
        // Nếu có nâng cấp và chuột được nhấn
        else if (isUpgrade)
        {
            if (Input.GetMouseButton(0) && shotTimer < 0)
            {
                Shoot2(shootSound);  // Bắn theo hướng chuột
                shotTimer = timeBetweenShots;  // Reset thời gian giữa các lần bắn
            }
        }
        // Nếu không ở chế độ tự động và chuột được nhấn
        else
        {
            if (Input.GetMouseButton(0) && shotTimer < 0)
            {
                Shoot(shootSound);  // Bắn theo hướng chuột
                shotTimer = timeBetweenShots;  // Reset thời gian giữa các lần bắn
            }
        }

    }


    // GameObject FindClosestEnemy()
    // {
    //     GameObject[] enemies = GameObject.FindGameObjectsWithTag("Monster");
    //     GameObject closest = null;
    //     float minDistance = Mathf.Infinity;

    //     foreach (GameObject enemy in enemies)
    //     {
    //         float distance = Vector3.Distance(transform.position, enemy.transform.position);
    //         if (distance < minDistance)
    //         {
    //             minDistance = distance;
    //             closest = enemy;
    //         }
    //     }
    //     return closest;
    // }

    void RotateGun(Vector3? targetPosition = null)
    {
        Vector3 lookPos;

        if (targetPosition.HasValue)
        {
            lookPos = targetPosition.Value;
        }
        else
        {
            lookPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lookPos.z = 0; // Đảm bảo z = 0 cho 2D
        }

        Vector2 lookDir = lookPos - transform.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (transform.eulerAngles.z > 90 && transform.eulerAngles.z < 270)
            transform.localScale = new Vector3(trucX, -1, 0);
        else
            transform.localScale = new Vector3(trucX, 1, 0);
    }


    void Shoot(AudioClip sound)
    {
        if (firePos != null)
        {
            shotTimer = timeBetweenShots;

            // Tính hướng từ vị trí firePos đến vị trí con trỏ chuột
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - firePos.position).normalized;

            // Khởi tạo đạn
            GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
            Instantiate(bulletBomb, firePos.position, Quaternion.identity, transform);

            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

            // Thiết lập góc quay của đạn theo hướng bắn
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletTmp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Bắn đạn theo hướng đã tính
            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

            // Phát âm thanh
            audioSource.PlayOneShot(sound);
        }
        else
        {
            Debug.LogError("firePos chưa được gán trong Inspector.");
        }

    }


    void Shoot2(AudioClip sound)
    {
        if (firePos != null)
        {
            // Bắn viên đạn đầu tiên
            GameObject bulletTmp1 = Instantiate(bullet, firePos.position, Quaternion.identity);
            Rigidbody2D rb1 = bulletTmp1.GetComponent<Rigidbody2D>();

            // Bắn viên đạn đầu tiên theo hướng súng với độ lệch -15 độ
            Vector2 direction1 = Quaternion.Euler(0, 0, -10) * transform.right;
            rb1.AddForce(direction1 * bulletSpeed, ForceMode2D.Impulse);


            // Bắn viên đạn thứ hai
            GameObject bulletTmp2 = Instantiate(bullet, firePos.position, Quaternion.identity);
            Rigidbody2D rb2 = bulletTmp2.GetComponent<Rigidbody2D>();

            // Bắn viên đạn thứ hai theo hướng súng với độ lệch +15 độ
            Vector2 direction2 = Quaternion.Euler(0, 0, 10) * transform.right;
            rb2.AddForce(direction2 * bulletSpeed, ForceMode2D.Impulse);


            // Thiết lập góc quay của đạn theo hướng bắn
            float angle = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg;
            bulletTmp1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Thiết lập góc quay của đạn theo hướng bắn
            angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;
            bulletTmp2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            // Bắn viên đạn bom
            Instantiate(bulletBomb, firePos.position, transform.rotation, transform);

            // Phát âm thanh
            audioSource.PlayOneShot(sound);
        }
        else
        {
            Debug.LogError("firePos chưa được gán trong Inspector.");
        }
    }

    //Hàm cập nhật prefab viên đạn mới
    public void UpdateBulletPrefab(GameObject newBulletPrefab)
    {
        bullet = newBulletPrefab;
        Debug.Log("Updated bullet prefab to: " + newBulletPrefab.name);
    }

    //Hàm cập nhật hiệu ứng bắn mới tại vị trí đạn bắn
    public void UpdateBulletBomb(GameObject newBulletBomb)
    {
        bulletBomb = newBulletBomb;
        Debug.Log("Updated bullet bomb to: " + newBulletBomb.name);
    }

    public void ShootX2()
    {
        isUpgrade = true;
        Debug.Log("Upgraded!!!");
    }

    public void Auto()
    {
        isAutoShoot = true;
        Debug.Log("Auto Shooted!!!");
    }


}
