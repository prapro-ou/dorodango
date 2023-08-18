using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Gensand : MonoBehaviour
{

    public GameObject sandPrefab; // 砂の粒のプレハブ
    private const float generationInterval = 0.1f; // 生成する間隔（秒）
    private const float fallSpeed = 2.0f; // 落下速度
    private const float rotationSpeed = 60.0f; // 回転速度
    private const int sandCountPerGeneration = 10;
    private Vector3 correctPosition = new Vector3(0f, 0f, 6f);//砂を生成する場所を補正
    private float generationTimer = 0f; // 生成用のタイマー
    MousePoint Mouse;
    // ...
    void Start()
    { 
        
    }

    void Update()
    {
        Mouse = GameObject.Find("Hand").GetComponent<MousePoint>();
        // マウスの左クリックを検知
        if (Input.GetMouseButtonDown(0))
        {
            generationTimer = 0f; // タイマーをリセット
           
        }

        // マウスの左クリックを離したとき
        if (Input.GetMouseButtonUp(0))
        {
            //isHoldingMouse = false; // 長押し中フラグをfalseにする
        }

        // 長押しかつ手を操作しているときの処理
        if (Input.GetMouseButton(0) && Mouse.pickedUp)
        {
            generationTimer += Time.deltaTime; // タイマーを更新

            // 一定間隔ごとに砂を生成
            if (generationTimer >= generationInterval)
            {
                GenerateSand(); // 砂を生成
                generationTimer = 0f; // タイマーをリセット
            }
        }
    }

    void GenerateSand()
    {
        // 砂を生成する処理

        for (int i = 0; i < sandCountPerGeneration; i++)
        {
            Vector3 spawnPosition = transform.position+correctPosition;//砂を生成する座標
            GameObject sand = Instantiate(sandPrefab, spawnPosition, Quaternion.identity);
            StartCoroutine(MoveAndRotateSand(sand, spawnPosition));
        }
    }

    //三角錐の形状に砂が落ちていくようにする
    IEnumerator MoveAndRotateSand(GameObject sand,Vector3 spawnPosition)
    {
        Vector3 destination = new Vector3(spawnPosition.x+Random.Range(-5f, 5f), -10f, spawnPosition.z+Random.Range(-5f, 5f));
        Vector3 axis = Vector3.Cross(Vector3.down, destination - sand.transform.position).normalized;

        float distance = Vector3.Distance(sand.transform.position, destination);
        float elapsedTime = 0f;

        while (elapsedTime < fallSpeed)
        {
            sand.transform.RotateAround(sand.transform.position, axis, rotationSpeed * Time.deltaTime);

            sand.transform.position = Vector3.Lerp(spawnPosition, destination, elapsedTime / fallSpeed);
            
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        
        Destroy(sand);
    }
}


