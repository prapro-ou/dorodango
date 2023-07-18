// no useing
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDestroy : MonoBehaviour
{
    public WaterTrigger waterTriggerScript;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // WateringCanPrefabオブジェクトのWaterTriggerスクリプト内の変数を参照
        waterTriggerScript = GameObject.Find("WateringCanPrefab").GetComponent<WaterTrigger>();
        if (!waterTriggerScript.flag || transform.position.y < 0) 
        {
            // Debug.Log("delete");
            Destroy(gameObject);
        }
    }
}
