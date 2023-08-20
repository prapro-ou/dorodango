using System.Linq;
using UnityEngine;

namespace scene_sprinkle_sand
{
    public class MoveHand : MonoBehaviour
    {
        [SerializeField] private Collider wallCollider;
        [SerializeField] private Vector3 correctPosition = new(0f, -2f, -4f); //マウスと手の位置を補正

        private AudioSource audioSource;

        private float audioStartTime;
        private new Collider collider;
        private Camera mainCamera;

        public bool PickedUp { get; private set; }

        private void Start()
        {
            collider = GetComponent<Collider>();
            audioSource = GetComponent<AudioSource>();
            mainCamera = Camera.main;
            audioStartTime = 0f;
        }

        private void Update()
        {
            var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            var hits = Physics.RaycastAll(ray, Mathf.Infinity);

            // クリックした瞬間にオブジェクトに当たっていれば持ち上げる
            if (Input.GetMouseButtonDown(0))
            {
                audioStartTime = Time.time;
                PickedUp = DoesHitThisAndWall(hits);

                if (PickedUp) audioSource.Play();
            }

            if (Input.GetMouseButtonUp(0) && PickedUp)
            {
                PickedUp = false;
                audioSource.Stop();
            }

            // 持ち上げていてかつマウスをホールドしている間Handを壁に沿って移動
            if (Input.GetMouseButton(0) && PickedUp)
            {
                // 再生して4秒たっても砂を出していたら砂の音を更新
                if (Time.time - audioStartTime > 4f)
                {
                    audioSource.Play();
                    audioStartTime = Time.time;
                }

                var wallHit = hits.First(hit => hit.collider == wallCollider);
                transform.position = wallHit.point + correctPosition;
            }
        }

        // WallにもHandにもRayが当たっていたらtrue
        private bool DoesHitThisAndWall(RaycastHit[] hits)
        {
            var doesHitHand = hits.Any(hit => hit.collider == collider);
            var doesHitWall = hits.Any(hit => hit.collider == wallCollider);

            return doesHitHand && doesHitWall;
        }
    }
}
