using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterGenerator : MonoBehaviour
{
    public WaterTrigger waterTriggerScript;
    public WateringCanRotate wateringCanRotateScript;
    public GameObject waterPrefab;
    
    public float interval; // interval秒ごとにオブジェクトを生成
    private int i, j, k;

    // 放射状
    private float sinθ, cosθ, r = 0;
    private float deg = 0, rad; // 角度，ラジアン

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating ("GenWater", 0f, interval);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenWater ()
    {
        //時間が経つことに数値が増える
        // time += Time.deltaTime;

        // WateringCanPrefabオブジェクトのWaterTriggerスクリプト内の変数を参照
        waterTriggerScript = GameObject.Find("WateringCanPrefab").GetComponent<WaterTrigger>();
        if (waterTriggerScript.flag && wateringCanRotateScript.rtFlag)
        // if (waterTriggerScript.flag && time >= limit)
        {
            //Prefabを生成する
            //Instantiate(waterPrefab, new Vector3 (0, 13, 0), Quaternion.identity); // 中心
            for(i = 0; i < 3; i++) {
                for(j = 0; j < 12; j++) {
                    rad = deg * Mathf.Deg2Rad;  //角度をラジアンに変換
                    //ラジアンを用いて，sinとcosを求める
                    sinθ = Mathf.Sin(rad);
                    cosθ = Mathf.Cos(rad);
                    //円周上の点を求める
                    //円の中心座標に半径をかけたcosとsinを足す
                    // var pos = this.gameObject.transform.position + new Vector3(cosθ*(2.0f + r), sinθ*(2.0f + r), 0);
                    //弾の作成
                    // var t = Instantiate(waterPrefab) as GameObject;
                    //弾を先ほど求めた円周上の座標に置く
                    // t.transform.position = pos;
                    Instantiate(waterPrefab, new Vector3 (cosθ * (0.7f + r), 13, sinθ * (0.7f + r)), Quaternion.identity);
                    //角度を30°足す
                    deg += 30;
                    //330°よりも大きくなったら弾を作らないのでフラグをtrueにしておく
                    // if (deg > 330) hasMakeTama = true;

                    // for(k = 0; k < 8; k++) {
                    // Instantiate(waterPrefab, new Vector3 (-2 + i, 10 + j, -2 + k), Quaternion.identity);
                        // Instantiate(waterPrefab, new Vector3 (0, 13, -1), Quaternion.identity);
                    // }
                }
                r += 0.5f;
                deg = 0;
            }
            r = 0;
            // Instantiate(waterParticle, new Vector3 (transform.position.x, 13, transform.position.z), Quaternion.identity);
            //数値を0に戻して一から時間を計り直す
            // time = 0;
        }
    }
}
