using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterTrigger : MonoBehaviour
{
    public WaterPouring waterPouringScript;  // WaterPouringスクリプトの参照

    public bool flag = false;   // マウスが押されたかどうかのフラグ
    private bool audioFlag = false; // Audioが再生されているかどうかのフラグ

    // 水を流す効果音用
    public AudioClip sound;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        //Componentを取得
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // マウスの左クリックを検知(マウスが押されている間)
        if (Input.GetMouseButton(0))
        {
            flag = true;
            if (audioFlag == false)
            {
                Invoke("WaterSound", 0.5f);
                audioFlag = true;
            }
        } else {
            flag = false;
            audioFlag = false;
            audioSource.Stop();
        }
    }

    void WaterSound()
    {
        audioSource.PlayOneShot(sound);    //音(sound)を鳴らす
    }
}
