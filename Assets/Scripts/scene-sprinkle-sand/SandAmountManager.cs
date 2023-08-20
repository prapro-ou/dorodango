using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace scene_sprinkle_sand
{
    public class SandAmountManager : MonoBehaviour
    {
        [SerializeField] private Slider gaugeSlider;
        private readonly FloatReactiveProperty sprinkledSandAmount = new(0f);

        private void Start()
        {
            sprinkledSandAmount.Subscribe(value => { gaugeSlider.value = value; });
        }

        public void AddSandAmount(float amount)
        {
            sprinkledSandAmount.Value += amount;
        }
    }
}
