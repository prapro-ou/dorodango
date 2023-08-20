using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class HandMover : MonoBehaviour
{
    [SerializeField] private float xzLimit = 4.5f;

    private void Start()
    {
        var initialY = transform.position.y;
        var mainCamera = Camera.main!;
        this.OnMouseDragAsObservable()
            .Subscribe(_ =>
            {
                var mouseWorldPos = MouseWorldPos(mainCamera);
                // Mathf.ClampでX,Zの値それぞれが最小～最大の範囲内に収める
                // 範囲を超えていたら範囲内の値を代入する
                var newX = Mathf.Clamp(mouseWorldPos.x, -xzLimit, xzLimit);
                var newZ = Mathf.Clamp(mouseWorldPos.z, -xzLimit, xzLimit);
                transform.position = new Vector3(newX, initialY, newZ);
            });
    }

    private static Vector3 MouseWorldPos(Camera camera) {
        var mouseScreenPos = Input.mousePosition;
        mouseScreenPos.z = 10f;
        return camera.ScreenToWorldPoint(mouseScreenPos);
    }
}
