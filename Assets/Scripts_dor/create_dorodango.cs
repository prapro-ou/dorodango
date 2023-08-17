using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class create_dorodango : MonoBehaviour
{
    // Start is called before the first frame update
    private int clickCount = 0;
    public int clicksToSwitch = 5; // 切り替えるためのクリック回数
    public GameObject[] childObjects; // 9個の子オブジェクトを持つ配列
    private int currentChildIndex = 0;
    private bool canSwitch = true;
    public CountDown countDownScript;
    
    void Start()
    {
        ShowChildObject(currentChildIndex);
    }

    private void ShowChildObject(int index)
    {
        // 指定された子オブジェクトを表示する
        for (int i = 0; i < childObjects.Length; i++)
        {
            childObjects[i].SetActive(i == index);
        }
    }

    public void IncrementClickCount()
    {

        if (canSwitch)
        {
            clickCount++;

            if(clickCount>=clicksToSwitch)
            {
                currentChildIndex = (currentChildIndex + 1) % childObjects.Length;
                ShowChildObject(currentChildIndex);
                clickCount=0;
                if (currentChildIndex == childObjects.Length - 1)
                {
                    canSwitch = false;
                    countDownScript.StopTimer();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}