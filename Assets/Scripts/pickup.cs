using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class pickup : MonoBehaviour
{
    private static float maxX = 9.0f; // カメラ座標xの最大値
    private static float maxZ = 4.5f; // カメラ座標zの最大値
    AudioSource audioSource;
    RaycastHit hit = new RaycastHit();
    bool IsPickUp;//持ち上げ状態かどうか
    float ObjectPosition;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        IsPickUp = false;
        ObjectPosition = transform.position.y;//初期位置での高さ
        Debug.Log(gameObject.GetComponent<Collider>());
       
    }
    

    bool IsHitTrash(Ray ray)
    {
        if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity) && hit.collider == this.gameObject.GetComponent<Collider>()) 
        {
            Debug.Log(hit.collider);
            //audioSource.Play();
            return true;
        }

        return false;
    }
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        //クリックした瞬間にオブジェクトに当たっていれば持ち上げる
        if (Input.GetMouseButtonDown(0))
            {
           
            IsPickUp =IsHitTrash(ray);
            if (IsPickUp) { Debug.Log("音"); audioSource.PlayOneShot(audioSource.clip); }//持ち上げた時の効果音
            Debug.Log(hit.collider);

        }
        //マウスをやめたとき
        if (Input.GetMouseButtonUp(0) && IsPickUp)
        {
            IsPickUp=false;
            transform.position = new Vector3(transform.position.x, ObjectPosition, transform.position.z);
            Debug.Log(Mathf.Abs(transform.position.x));
            Debug.Log(Mathf.Abs(transform.position.z));
            if (Mathf.Abs(transform.position.x)>=maxX || Mathf.Abs(transform.position.z) >= maxZ) Destroy(this.gameObject);//画面外にあれば破壊
        }

        //持ち上げていてかつマウスをホールドしている間オブジェクトを移動
        if (Input.GetMouseButton(0) && IsPickUp)
        {
           if (!Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity)) return;
            Vector3 hitPoint = hit.point;
            transform.position = new Vector3(hitPoint.x, 1.5f, hitPoint.z);
        }
       
    }
    
}
