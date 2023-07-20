using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPouring : MonoBehaviour
{
    public WaterTrigger waterTriggerScript;
    
    public float speed = 10f; // 水を注ぐ速度
    //射出角度
    [Range(0.0f, 90.0f)] public float throwingAngle = 20.0f;

    private float startTime, distance, targetPosX, targetPosY, targetPosZ;
    private Vector3 startPosition, targetPosition;
    private float Travel;

    void Start()
    {
        //現在の位置を地上の高さに合わせ、その地点を出発地とする
        startPosition = transform.position;
        //到着地点をセット
        if(transform.position.x < 0) {
            targetPosX = (transform.position.x - 2.0f);
        } else if(transform.position.x > 0) {
            targetPosX = transform.position.x + 2.0f;
        } else {
            targetPosX = transform.position.x;
        }
        if(transform.position.z < 0) {
            targetPosZ = (transform.position.z - 2.0f);
        } else if(transform.position.z > 0) {
            targetPosZ = transform.position.z + 2.0f;
        } else {
            targetPosZ = transform.position.z;
        }
        // targetPosY = -5;
        // targetPosX = transform.position - 5)
        targetPosition = new Vector3(targetPosX, -5, targetPosZ - 7);
        //移動量を0.0にリセットしておく
        Travel = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        // WateringCanPrefabオブジェクトのWaterTriggerスクリプト内の変数を参照
        waterTriggerScript = GameObject.Find("WateringCanPrefab").GetComponent<WaterTrigger>();
        if (waterTriggerScript.flag)    // マウスが左クリックされている間実行する
        {
            //出発地からの水平移動量を求め...
            Travel += speed * Time.deltaTime;
            //出発地と目的地の距離を求め...
            var distance = Vector3.Distance(startPosition, targetPosition);
            //進行割合を求め...
            var t = Travel / distance;
            if (t < 1.0f)
            {
                //tが0.5（つまり中間地点）からどれだけ離れているかを求める
                //中間地点で0.0、出発地や目的地で1.0となるような値にする
                var d = Mathf.Abs(t - 0.5f) * 2.0f;
                //現在の水平位置を決め...
                var p = Vector3.Lerp(startPosition, targetPosition, t);
                //高さを二次関数の曲線に沿って調整し...
                p.y += Mathf.Tan(Mathf.Deg2Rad * this.throwingAngle) * 0.25f * distance * (1.0f - (d * d));
                //位置を設定する
                transform.position = p;
            }
            else
            {
                //tが1.0に到達したら移動終了とする
                transform.position = targetPosition;
            }
        }
        if (!waterTriggerScript.flag || transform.position.y < 0) 
        {
            Destroy(gameObject);
        }
    }
}