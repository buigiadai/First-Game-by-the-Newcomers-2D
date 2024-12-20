//Chatgpt: https://chatgpt.com/c/67421fc2-d89c-800f-b58c-789552db9e47
//Các video đa số sẽ chỉ tạo animation cho nhân vật, điều này khá tốn thời gian và công sức. 
//Tuy nhiên, hiệu ứng bóng mờ (after image) cho nhân vật một cách dễ dàng hơn bằng cách tạo một prefab AfterImageEffect.
//Việc này tối đa hóa sự linh hoạt và giảm thời gian cần thiết để tạo hiệu ứng bóng mờ cho nhân vật.

//Bùi Gia Đại
using UnityEngine;

public class AfterImageEffect : MonoBehaviour
{
    public Sprite sprite;                // Sprite để hiển thị
    public Color startColor = Color.white; // Màu bắt đầu
    public float fadeDuration = 0.5f;     // Thời gian mờ dần

    private SpriteRenderer spriteRenderer; // SpriteRenderer để hiển thị hình ảnh
    private float timer;

    void Start()
    {
        spriteRenderer = gameObject.AddComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;
        spriteRenderer.color = startColor;

        timer = fadeDuration;
    }

    void Update()
    {
        // Giảm dần thời gian
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            Destroy(gameObject); // Xóa bóng mờ sau khi mờ hoàn toàn
        }
        else
        {
            // Giữ màu trắng và chỉ thay đổi độ mờ (alpha)
            Color color = spriteRenderer.color;
            color.a = Mathf.Lerp(1f, 0f, (fadeDuration - timer) / fadeDuration); // Alpha mờ dần từ 1 đến 0
            spriteRenderer.color = color;
        }
    }

}
