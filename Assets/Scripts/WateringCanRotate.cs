using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WateringCanRotate : MonoBehaviour
{
    public WaterTrigger waterTriggerScript;
    
    float speed = 0.1f; //向くスピード(秒速)

    private bool initFlag1 = false, initFlag2 = false;
    public bool rtFlag = false; // じょうろを傾けたかどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // WateringCanPrefabオブジェクトのWaterTriggerスクリプト内の変数を参照
        waterTriggerScript = GameObject.Find("WateringCanPrefab").GetComponent<WaterTrigger>();

        if (waterTriggerScript.flag)
        {
            if(initFlag1 == false)
            {
                initFlag1 = true;
                StartCoroutine(Rotate());
            }
        } 
        if (!waterTriggerScript.flag)
        {
            rtFlag = false;
            if(initFlag2 == true)
            {
                initFlag2 = false;
                StartCoroutine(ReRotate());
            }
        }
    }

    IEnumerator Rotate()
    {
        int i = 0;
        while(i < 40)
        {
            i++;
            this.transform.Rotate(0, 0, speed);
            yield return null;
        }
        initFlag2 = true;
        rtFlag    = true;
    }
    IEnumerator ReRotate()
    {
        int i = 0;
        while(i < 40)
        {
            i++;
            this.transform.Rotate(0, 0, -speed);
            yield return null;
        }
        initFlag1 = false;
    }
}