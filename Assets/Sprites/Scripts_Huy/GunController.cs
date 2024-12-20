using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public GameObject currentGun; // Súng hiện tại
    public GameObject currentBulletPrefab; // Prefab viên đạn hiện tại
 
    // Phương thức để cập nhật chỉ viên đạn
    public void UpdateBulletPrefab(GameObject newBulletPrefab)
    {
        currentBulletPrefab = newBulletPrefab;
        Debug.Log("Updated bullet prefab to: " + newBulletPrefab.name);
    }

    public void ChangeBulletPrefab(GameObject newBulletPrefab)
    {
        currentBulletPrefab = newBulletPrefab;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
