using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandScript : MonoBehaviour
{
    Vector3 mouse;
	public Vector3 mouse3d;
    float objPosY;

    // XとZの上限
    float limit = 4.5f;

	// Use this for initialization
	void Start () {
        objPosY = transform.position.y;
	}
	// Update is called once per frame
	void Update () {
		mouse = Input.mousePosition;
		mouse.z = 10f;
	}

    void OnMouseDrag(){
        mouse3d = Camera.main.ScreenToWorldPoint(mouse);
		// Debug.Log (mouse3d);
        mouse3d.y = objPosY;

        // Mathf.ClampでX,Zの値それぞれが最小～最大の範囲内に収める。
        // 範囲を超えていたら範囲内の値を代入する
        mouse3d.x = Mathf.Clamp(mouse3d.x, -limit, limit);
        mouse3d.z = Mathf.Clamp(mouse3d.z, -limit, limit);

        transform.position = mouse3d;        
    }
}
