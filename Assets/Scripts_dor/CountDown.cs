using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class CountDown : MonoBehaviour
{
    // Start is called before the first frame update
    public static float CountDownTime;
    public click clickCounter;
    public create_dorodango dorodango;
    public Text TextCountDown;
    public GameObject[] objectsToControl;
    private bool timerStopped = false;

    
    void Start()
    {
        CountDownTime=30.0f;
    }

    public void StopTimer()
    {
        timerStopped = true;
    }

    public void SetGroupActive(bool isActive)
    {
        foreach (GameObject obj in objectsToControl)
        {
            obj.SetActive(isActive);
        }
    }



    // Update is called once per frame
    void Update()
    {
        // カウントダウンタイムを整形して表示
        if (CountDownTime <= 0.0F)
        {
            CountDownTime = 0.0F;
            SetGroupActive(false);
        }

        if (timerStopped)
        {
            SetGroupActive(false);// タイマーが停止したら何もしない
            return;
        }


        TextCountDown.text = String.Format("Time: {0:00.00}", CountDownTime);
        // 経過時刻を引いていく
        CountDownTime -= Time.deltaTime;
    }
}
