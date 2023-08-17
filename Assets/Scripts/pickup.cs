using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class pickup : MonoBehaviour
{
    private static float maxX = 9.0f; // �J�������Wx�̍ő�l
    private static float maxZ = 4.5f; // �J�������Wz�̍ő�l
    AudioSource audioSource;
    RaycastHit hit = new RaycastHit();
    bool IsPickUp;//�����グ��Ԃ��ǂ���
    float ObjectPosition;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        
        IsPickUp = false;
        ObjectPosition = transform.position.y;//�����ʒu�ł̍���
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
        //�N���b�N�����u�ԂɃI�u�W�F�N�g�ɓ������Ă���Ύ����グ��
        if (Input.GetMouseButtonDown(0))
            {
           
            IsPickUp =IsHitTrash(ray);
            if (IsPickUp) { Debug.Log("��"); audioSource.PlayOneShot(audioSource.clip); }//�����グ�����̌��ʉ�
            Debug.Log(hit.collider);

        }
        //�}�E�X����߂��Ƃ�
        if (Input.GetMouseButtonUp(0) && IsPickUp)
        {
            IsPickUp=false;
            transform.position = new Vector3(transform.position.x, ObjectPosition, transform.position.z);
            Debug.Log(Mathf.Abs(transform.position.x));
            Debug.Log(Mathf.Abs(transform.position.z));
            if (Mathf.Abs(transform.position.x)>=maxX || Mathf.Abs(transform.position.z) >= maxZ) Destroy(this.gameObject);//��ʊO�ɂ���Δj��
        }

        //�����グ�Ă��Ă��}�E�X���z�[���h���Ă���ԃI�u�W�F�N�g���ړ�
        if (Input.GetMouseButton(0) && IsPickUp)
        {
           if (!Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity)) return;
            Vector3 hitPoint = hit.point;
            transform.position = new Vector3(hitPoint.x, 1.5f, hitPoint.z);
        }
       
    }
    
}
