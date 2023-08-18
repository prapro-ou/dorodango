using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SandSplinkleSlider : MonoBehaviour
{


    private const float gaugeIncreaseAmount = 0.1f; // ゲージが増加する量
    private const float maxGaugeValue = 100f; // ゲージの最大値
    public Slider gaugeSlider; // ゲージのSlider

    private void Start()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("dorodango"))
        {
            Debug.Log(gaugeSlider.value);
            // ゲージを増加させる
            gaugeSlider.value += gaugeIncreaseAmount;
        }
    }
}










