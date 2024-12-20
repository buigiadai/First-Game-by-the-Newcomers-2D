using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class NewWeapon : MonoBehaviour
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

    private Transform playerTransform; // Tham chiếu đến đối tượng Player

    void Start()
    {
        shotTimer = timeBetweenShots;

        // Lấy AudioSource từ đối tượng
        audioSource = GetComponent<AudioSource>();

        // Tìm đối tượng Player
        playerTransform = transform.parent; // Giả sử súng là con của Player
    }

    void Update()
    {
        shotTimer -= Time.deltaTime;

        RotateGun(); // Hướng súng về vị trí chuột

        UpdateWeaponScale();

        if (Input.GetKeyDown(KeyCode.E))
        {
            isAutoShoot = !isAutoShoot;
        }

        // Kiểm tra nếu ở chế độ tự động và thời gian đủ để bắn
        if (isAutoShoot && Time.time >= nextAutoShootTime)
        {
            Shoot(autoShootSound);
            shotTimer = timeBetweenShots;
            nextAutoShootTime = Time.time + autoShootDelay;
        }
        // Nếu có nâng cấp và chuột được nhấn
        else if (isUpgrade)
        {
            if (Input.GetMouseButton(0) && shotTimer < 0)
            {
                Shoot2(shootSound);
                shotTimer = timeBetweenShots;
            }
        }
        // Nếu không ở chế độ tự động và chuột được nhấn
        else
        {
            if (Input.GetMouseButton(0) && shotTimer < 0)
            {
                Shoot(shootSound);
                shotTimer = timeBetweenShots;
            }
        }
    }

    void RotateGun()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        // Tính hướng từ súng tới vị trí chuột
        Vector3 direction = mousePosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        // Xoay súng theo góc đã tính
        transform.rotation = Quaternion.Euler(0, 0, angle);

        // Lật súng nếu cần
        if (angle > 90 || angle < -90)
        {
            transform.localScale = new Vector3(1, -1, 1); // Lật súng theo trục Y
        }
        else
        {
            transform.localScale = new Vector3(1, 1, 1); // Giữ bình thường
        }
    }

    void UpdateWeaponScale()
    {
        if (playerTransform != null)
        {
            // Kiểm tra scale X của nhân vật
            float playerScaleX = playerTransform.localScale.x;

            // Cập nhật scale X của súng dựa trên scale X của nhân vật
            Vector3 weaponScale = transform.localScale;
            weaponScale.x = Mathf.Abs(weaponScale.x) * Mathf.Sign(playerScaleX); // Lật súng theo trục X nếu nhân vật lật
            transform.localScale = weaponScale;
        }
    }

    void Shoot(AudioClip sound)
    {
        if (firePos != null)
        {
            shotTimer = timeBetweenShots;

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = (mousePosition - firePos.position).normalized;

            GameObject bulletTmp = Instantiate(bullet, firePos.position, Quaternion.identity);
            Instantiate(bulletBomb, firePos.position, Quaternion.identity, transform);

            Rigidbody2D rb = bulletTmp.GetComponent<Rigidbody2D>();

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            bulletTmp.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            rb.AddForce(direction * bulletSpeed, ForceMode2D.Impulse);

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
            GameObject bulletTmp1 = Instantiate(bullet, firePos.position, Quaternion.identity);
            Rigidbody2D rb1 = bulletTmp1.GetComponent<Rigidbody2D>();

            Vector2 direction1 = Quaternion.Euler(0, 0, -10) * transform.right;
            rb1.AddForce(direction1 * bulletSpeed, ForceMode2D.Impulse);

            GameObject bulletTmp2 = Instantiate(bullet, firePos.position, Quaternion.identity);
            Rigidbody2D rb2 = bulletTmp2.GetComponent<Rigidbody2D>();

            Vector2 direction2 = Quaternion.Euler(0, 0, 10) * transform.right;
            rb2.AddForce(direction2 * bulletSpeed, ForceMode2D.Impulse);

            float angle = Mathf.Atan2(direction1.y, direction1.x) * Mathf.Rad2Deg;
            bulletTmp1.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            angle = Mathf.Atan2(direction2.y, direction2.x) * Mathf.Rad2Deg;
            bulletTmp2.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

            Instantiate(bulletBomb, firePos.position, transform.rotation, transform);

            audioSource.PlayOneShot(sound);
        }
        else
        {
            Debug.LogError("firePos chưa được gán trong Inspector.");
        }
    }
}
