//Bùi Gia Đại
//Tham số nhận: biến xác định quái vật 
//Chức năng: Hiển thị vòng tròn
// + isEnemyCircle= false: vòng tròn của player
// + isEnemyCircle= true: vòng tròn của quái vật, mặc định ẩn đi

using UnityEngine;


public class RotateCircle : MonoBehaviour
{
    // Biến xác định đây là vòng tròn của quái vật
    public bool isEnemyCircle = false;

    // Biến thời gian và trạng thái hiển thị
    private float timeToHide = 3f;   // 3 giây để ẩn vòng tròn
    private float timer = 0f;        // Bộ đếm thời gian
    private bool isVisible = false;  // Trạng thái hiển thị của vòng tròn
    public float rotationSpeed = 100f; // Tốc độ xoay, có thể điều chỉnh tùy ý

    private void Start()
    {
        if (isEnemyCircle)
            // Ẩn vòng tròn khi bắt đầu
            SetCircleVisibility(false);
    }

    private void Update()
    {
        // Nếu vòng tròn đang hiển thị, đếm thời gian để ẩn
        if (isVisible)
        {
            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // Xoay vòng tròn
            timer += Time.deltaTime;
            if (timer >= timeToHide)
            {
                SetCircleVisibility(false); // Ẩn vòng tròn sau 3 giây
            }
        }
        else

            transform.Rotate(0, 0, rotationSpeed * Time.deltaTime); // Xoay vòng tròn
    }

    // Phương thức để hiện vòng tròn khi nhận sát thương
    public void ShowCircleOnDamage()
    {
        // Debug.Log("Hiển thị vòng tròn quái vật");w
        SetCircleVisibility(true); // Hiện vòng tròn
        timer = 0f;                // Đặt lại thời gian chờ
    }

    // Phương thức đặt trạng thái hiển thị của vòng tròn
    private void SetCircleVisibility(bool visible)
    {
        isVisible = visible;
        gameObject.SetActive(visible); // Ẩn hoặc hiện vòng tròn
    }
}
