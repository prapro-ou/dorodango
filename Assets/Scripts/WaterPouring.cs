using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPouring : MonoBehaviour
{
    public WaterTrigger waterTriggerScript;
    // public float pouringSpeed = 0.2f;   // 水を注ぐ速度

    // 水の起動用
    // Transform endPos;                       // 終点座標
    // float flightTime = 2;                   //滞空時間
    // float speedRate = 1;                    //滞空時間を基準とした移動速度倍率
    // private const float gravity = -9.8f;    //重力
    public float speed = 10f; // 水を注ぐ速度
    //射出角度
    [Range(0.0f, 90.0f)] public float throwingAngle = 30.0f;

    private float startTime, distance, targetPosX, targetPosY, targetPosZ;
    private Vector3 startPosition, targetPosition;
    float Travel;

    // private float sinθ, cosθ, r = 1;
    // private float deg = 0, rad; // 角度，ラジアン

    // Start is called before the first frame update
    void Start()
    {
//         //スタート時間をキャッシュ
//         startTime = Time.time;
// 　　　　 //スタート位置をキャッシュ
//         startPosition = transform.position;
//         //到着地点をセット
//         targetPosition = new Vector3(0, -5, -5);
//         //目的地までの距離を求める
//         distance = Vector3.Distance(startPosition, targetPosition);

        // rad = deg * Mathf.Deg2Rad;  //角度をラジアンに変換
        // //ラジアンを用いて，sinとcosを求める
        // sinθ = Mathf.Sin(rad);
        // cosθ = Mathf.Cos(rad);
        // targetPosition = new Vector3 (cosθ * (1.5f + r), -5, sinθ * (1.5f + r));

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
            // 水の位置を変更する
            // transform.Translate(0, - (0.01f + 0.1f * pouringSpeed), 0, Space.World);

            //現在フレームの補間値を計算
            // float interpolatedValue = (Time.time - startTime) / distance;
            //球面線形移動
            // transform.position = Vector3.Slerp(startPosition, targetPosition, interpolatedValue * speed);
            
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

    // private IEnumerator Jump(Vector3 endPos, float flightTime, float speedRate, float gravity)
    // {
    //     Debug.Log("1");
    //     var startPos = transform.position; // 初期位置
    //     var diffY = (endPos - startPos).z; // 始点と終点のy成分の差分
    //     var vn = (diffY - gravity * 0.5f * flightTime * flightTime) / flightTime; // 鉛直方向の初速度vn
 
    //     // 放物運動
    //     for (var t = 0f; t < flightTime; t += (Time.deltaTime * speedRate))
    //     { 
    //         var p = Vector3.Lerp(startPos, endPos, t/flightTime);   //水平方向の座標を求める (x,z座標)
    //         p.z = startPos.z - vn * t + 0.5f * gravity * t * t;     // 鉛直方向の座標 y
    //         transform.position = p;
    //         yield return null; //1フレーム経過
    //     }
    //     // 終点座標へ補正
    //     transform.position = endPos;
    // }

}