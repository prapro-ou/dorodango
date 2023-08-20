using System;
using System.Collections;
using UniRx;
using UniRx.Triggers;
using UnityEngine;
using Random = UnityEngine.Random;

namespace scene_sprinkle_sand
{
    public class SandGenerator : MonoBehaviour
    {
        private const float GenerationInterval = 0.1f; // 生成する間隔（秒）
        private const float FallSpeed = 2.0f; // 落下速度
        private const float RotationSpeed = 60.0f; // 回転速度
        private const int SandCountPerGeneration = 10;

        [SerializeField] private GameObject sandPrefab; // 砂の粒のプレハブ
        [SerializeField] private MoveHand moveHand;
        [SerializeField] private Vector3 correctPosition = new(0f, 0f, 6f); // 砂を生成する場所を補正

        private void Start()
        {
            this.UpdateAsObservable()
                .Where(_ => moveHand.PickedUp)
                .ThrottleFirst(TimeSpan.FromSeconds(GenerationInterval))
                .Subscribe(_ => { GenerateSand(); });
        }

        /// 砂を生成する処理
        private void GenerateSand()
        {
            for (var i = 0; i < SandCountPerGeneration; i++)
            {
                var spawnPosition = transform.position + correctPosition; //砂を生成する座標
                var sand = Instantiate(sandPrefab, spawnPosition, Quaternion.identity);
                StartCoroutine(MoveAndRotateSand(sand, spawnPosition));
            }
        }

        // 三角錐の形状に砂が落ちていくようにする
        private IEnumerator MoveAndRotateSand(GameObject sand, Vector3 spawnPosition)
        {
            var destination = new Vector3(spawnPosition.x + Random.Range(-5f, 5f), -10f,
                spawnPosition.z + Random.Range(-5f, 5f));
            var axis = Vector3.Cross(Vector3.down, destination - sand.transform.position).normalized;

            var distance = Vector3.Distance(sand.transform.position, destination);
            var elapsedTime = 0f;

            while (elapsedTime < FallSpeed)
            {
                sand.transform.RotateAround(sand.transform.position, axis, RotationSpeed * Time.deltaTime);

                sand.transform.position = Vector3.Lerp(spawnPosition, destination, elapsedTime / FallSpeed);

                elapsedTime += Time.deltaTime;

                yield return null;
            }

            Destroy(sand);
        }
    }
}
