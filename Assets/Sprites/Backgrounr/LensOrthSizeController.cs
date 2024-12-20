using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CinemachineLensController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // Cinemachine Virtual Camera
    [SerializeField] private Slider lensSlider;                      // Slider UI

    // Giá trị min/max cho Lens Orthographic Size
    [SerializeField] private float minLensSize = 1f;
    [SerializeField] private float maxLensSize = 10f;

    private void Start()
    {
        // Kiểm tra và gán giá trị ban đầu
        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera chưa được gắn!");
            return;
        }

        if (lensSlider == null)
        {
            Debug.LogError("Slider chưa được gắn!");
            return;
        }

        // Lấy giá trị ban đầu từ Virtual Camera
        CinemachineComponentBase lensComponent = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        // Cấu hình Slider
        lensSlider.minValue = minLensSize;
        lensSlider.maxValue = maxLensSize;
        lensSlider.value = virtualCamera.m_Lens.OrthographicSize;

        // Lắng nghe thay đổi giá trị của Slider
        lensSlider.onValueChanged.AddListener(UpdateLensSize);
    }

    private void UpdateLensSize(float newSize)
    {
        // Cập nhật Lens Orthographic Size của Cinemachine Virtual Camera
        if (virtualCamera != null)
        {
            virtualCamera.m_Lens.OrthographicSize = newSize;
        }
    }

    private void OnDestroy()
    {
        // Gỡ bỏ listener khi đối tượng bị phá hủy
        if (lensSlider != null)
        {
            lensSlider.onValueChanged.RemoveListener(UpdateLensSize);
        }
    }
}
