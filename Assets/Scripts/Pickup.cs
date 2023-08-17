using UnityEngine;

public class Pickup : MonoBehaviour
{
    private const float DistanceOfLift = 1.5f;
    private const float MaxX = 9.0f; // カメラ座標xの最大値
    private const float MaxZ = 4.5f; // カメラ座標zの最大値
    [SerializeField] private AudioSource audioSource;
    private new Collider collider;
    private float defaultY;
    private Camera mainCamera;
    private bool pickedUp = false;

    private void Start()
    {
        collider = GetComponent<Collider>();
        mainCamera = Camera.main;
        defaultY = transform.position.y;
    }

    private void Update()
    {
        var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        // クリックした瞬間にオブジェクトに当たっていれば持ち上げる
        if (Input.GetMouseButtonDown(0) && DoesHitMe(ray))
        {
            pickedUp = true;
            audioSource.PlayOneShot(audioSource.clip);
        }

        // マウスホールドをやめたとき
        if (Input.GetMouseButtonUp(0) && pickedUp)
        {
            pickedUp = false;
            var newPosition = transform.position;
            newPosition.y = defaultY;
            transform.position = newPosition;
            if (Mathf.Abs(transform.position.x) >= MaxX || Mathf.Abs(transform.position.z) >= MaxZ)
                Destroy(gameObject); // 画面外にあれば破壊
        }

        // 持ち上げていてかつマウスをホールドしている間オブジェクトを移動
        if (Input.GetMouseButton(0) && pickedUp)
        {
            RaycastHit hit;
            if (!Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity)) return;
            var newPosition = hit.point;
            newPosition.y = defaultY + DistanceOfLift;
            transform.position = newPosition;
        }
    }


    private bool DoesHitMe(Ray ray)
    {
        RaycastHit hit;
        return Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) &&
               hit.collider == collider;
    }
}
