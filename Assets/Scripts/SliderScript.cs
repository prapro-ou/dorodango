using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{
    public WaterTrigger waterTriggerScript;
    public WateringCanRotate wateringCanRotateScript;
    private float maxWater = 100f;  // 最大の水の量
    private float nowWater;       // 現在の水の量
    private bool sliderFlag = false;
    //Slider
    public Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider.value = 0;   // Sliderを0にする
        nowWater = 0;       // 現在の水の量を0にする
    }

    // Update is called once per frame
    void Update()
    {
        // WateringCanPrefabオブジェクトのWaterTriggerスクリプト内の変数を参照
        waterTriggerScript = GameObject.Find("WateringCanPrefab").GetComponent<WaterTrigger>();
        if (waterTriggerScript.flag && wateringCanRotateScript.rtFlag)    // マウスが左クリックされている間 && じょうろを傾けた後実行する
        {
            if (sliderFlag == false)    {
                sliderFlag = true;
                Invoke("WaterSlider", 2f);
            } else {
                Invoke("WaterSlider", 0f);
            }
        } else {
            sliderFlag = false;
        }
    }

    void WaterSlider()
    {
        nowWater += 0.1f;
        slider.value = nowWater / maxWater;    //nowWaterをSliderに反映
    }
}
