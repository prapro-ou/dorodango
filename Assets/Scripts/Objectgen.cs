using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectgen : MonoBehaviour
{
    public GameObject[] items; // ランダムに配置するアイテムのプレハブなどを配列に格納

    public float minX = -10f; // ゲーム画面のx座標の最小値
    public float maxX = 10f; // ゲーム画面のx座標の最大値
    public float minZ = -5.5f; // ゲーム画面のz座標の最小値
    public float maxZ = 5.5f; //ゲーム画面のz座標の最大値

    void Start()
    {
        RandomizeItemPositionsOnXZPlane();//ゴミをランダムに配置
    }

    void RandomizeItemPositionsOnXZPlane()
    {
        foreach (GameObject item in items)
        {
            float randomX = Random.Range(minX+2f, maxX-2f);
            float randomZ = Random.Range(minZ+2f, maxZ-2f);
            Vector3 randomPosition = new Vector3(randomX, item.transform.position.y, randomZ);
            Instantiate(item, randomPosition, Quaternion.identity);
        }
    }
}
