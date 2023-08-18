using UnityEngine;

namespace scene_shaping
{
    public class ClickTarget : MonoBehaviour
    {
        [HideInInspector] public new Collider collider;
        [SerializeField] private GameObject clickEffectPrefab;
        [SerializeField] private AudioClip clickSound;
        private AudioSource audioSource;

        private void Start()
        {
            collider = GetComponent<Collider>();
            audioSource = GetComponent<AudioSource>();
            MoveRandomly();
        }

        public void OnTargetCleared()
        {
            SpawnParticle();
            PlaySe();
            MoveRandomly();
        }

        private void SpawnParticle()
        {
            var effectInstance = Instantiate(clickEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effectInstance, 1.0f); // 2秒後にエフェクトを削除
        }

        private void MoveRandomly()
        {
            var newPosition = new Vector3(Random.Range(-0.8f, 0.8f), Random.Range(-0.4f, 0.7f), -1.36f);
            transform.position = newPosition;
        }

        private void PlaySe()
        {
            audioSource.PlayOneShot(clickSound);
        }
    }
}
