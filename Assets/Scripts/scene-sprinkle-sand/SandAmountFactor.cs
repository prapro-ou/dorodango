using UnityEngine;

namespace scene_sprinkle_sand
{
    public class SandAmountFactor : MonoBehaviour
    {
        [SerializeField] private float increaseSandAmount = 0.1f;
        [SerializeField] private SandAmountManager sandAmountManager;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("dorodango")) sandAmountManager.AddSandAmount(increaseSandAmount);
        }
    }
}
