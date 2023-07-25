using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPosition : MonoBehaviour
{
    // Start is called before the first frame update

    RaycastHit hit = new RaycastHit();
    Vector3 position;
    void Start()
    {
       
    }

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Input.GetMouseButtonDown(0)) {
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider == gameObject.GetComponent<Collider>())
            {
                Vector3 hitPoint = hit.point;
                position = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z);
                Debug.Log(position);//âÊñ í[ÇÃç¿ïWÇéÊìæ
            }
        }
    }

        // Update is called once per frame



    }
