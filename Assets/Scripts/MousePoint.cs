using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MousePoint : MonoBehaviour
{
    internal bool pickedUp=false;//GenSandで使うため
    //private Collider wallCollider;
    private Vector3 correctPosition=new Vector3(0f,-2f,-4f);//マウスと手の位置を補正
    public GameObject Wall;
    RaycastHit[] hits;
    AudioSource audioSource;
    private float audioStartTime;



    // Start is called before the first frame update
    void Start()
    {
        
        audioSource=GetComponent<AudioSource>();
        audioStartTime = 0f;
        
    }

    //WallにもHandにもRayが当たっていたらtrue
    public bool DoesHitHand()
    {
        bool a = false;
        bool b = false;
        foreach(var hit in hits)
        {
            if (hit.collider == GetComponent<Collider>())
            {
                a = true;
                Debug.Log(hit.collider);
            }
            if (hit.collider == Wall.GetComponent<Collider>())
            {
                b = true;
                Debug.Log(hit.collider);
            }
        }
        if (a && b) return true;

        return false;
    }
    
    
    // Update is called once per frame
    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        hits = Physics.RaycastAll(ray, Mathf.Infinity);
        //クリックした瞬間にオブジェクトに当たっていれば持ち上げる
        if (Input.GetMouseButtonDown(0))
        {
            audioStartTime = Time.time;
            pickedUp = DoesHitHand();

            if (pickedUp) audioSource.Play();
        }
        if (Input.GetMouseButtonUp(0) && pickedUp)
        {
            pickedUp = false;
            audioSource.Stop();
        }

        //持ち上げていてかつマウスをホールドしている間Handを壁に沿って移動
        if (Input.GetMouseButton(0) && pickedUp)
        {
            
            //再生して4秒たっても砂を出していたら砂の音を更新
            if (Time.time-audioStartTime>4f)
            {
                audioSource.Play();
                audioStartTime = Time.time;
            }
            foreach (var hit in hits)
            {
                if (hit.collider == Wall.GetComponent<Collider>())
                {
                    Vector3 hitPoint = hit.point;
                    transform.position = new Vector3(hitPoint.x, hitPoint.y, hitPoint.z)+ correctPosition;
                }
            }
        }
    }
}
