using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objectgen : MonoBehaviour
{
    public GameObject[] items; // �����_���ɔz�u����A�C�e���̃v���n�u�Ȃǂ�z��Ɋi�[

    public float minX = -10f; // �Q�[����ʂ�x���W�̍ŏ��l
    public float maxX = 10f; // �Q�[����ʂ�x���W�̍ő�l
    public float minZ = -5.5f; // �Q�[����ʂ�z���W�̍ŏ��l
    public float maxZ = 5.5f; //�Q�[����ʂ�z���W�̍ő�l

    void Start()
    {
        RandomizeItemPositionsOnXZPlane();//�S�~�������_���ɔz�u
    }

    void RandomizeItemPositionsOnXZPlane()
    {
        foreach (GameObject item in items)
        {
            float randomX = Random.Range(minX+2f, maxX-2f);
            float randomZ = Random.Range(minZ+2f, maxZ-2f);
            Vector3 randomPosition = new Vector3(randomX, item.transform.position.y, randomZ);
            Instantiate(item, randomPosition, Quaternion.identity);
        }
    }
}
