using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KorokoroScript : MonoBehaviour
{
    Vector3 objPos;
    float objPosY;
    public Transform targetObject;

    // Start is called before the first frame update
    void Start()
    {
        objPosY = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        //　Debug.Log (transform.position);

        objPos += targetObject.position - transform.position;
        objPos.y = objPosY;
        transform.position += objPos *= Time.deltaTime;
    }
}
