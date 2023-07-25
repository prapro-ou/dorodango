using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tamari : MonoBehaviour
{
    public zaru SM_B;

    private float initialScaleX;
    private float initialScaleY;
    private float initialScaleZ;

    // Start is called before the first frame update
    void Start()
    {
        initialScaleX = transform.localScale.x;
        initialScaleY = transform.localScale.y;
        initialScaleZ = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        float ScaleY = SM_B.sum;
        transform.localScale=new Vector3(initialScaleX,ScaleY * 0.1f + initialScaleY,initialScaleZ);
        

    }
}
