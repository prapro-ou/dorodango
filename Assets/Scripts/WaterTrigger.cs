using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    public WaterPouring waterPouringScript;  // WaterPouringスクリプトの参照

    public bool flag = false;   // マウスが押されたかどうかのフラグ

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // マウスの左クリックを検知(マウスが押されている間)
        if (Input.GetMouseButton(0))
        {
            flag = true;
        } else {
            flag = false;
        }
    }
}
