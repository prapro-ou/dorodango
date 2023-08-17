using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class click : MonoBehaviour
{
    // Start is called before the first frame update
    public create_dorodango clickCounter;
    public GameObject clickEffectPrefab;
    public AudioClip clickSound; // クリック音をアタッチ
    private AudioSource audioSource; // AudioSource コンポーネント
    private int clickCount = 0; // クリック回数

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        Vector3 newPosition = new Vector3(Random.Range(-0.8f, 0.8f), Random.Range(-0.4f, 0.7f), -1.36f);
        transform.position = newPosition;
    }





    private void OnMouseDown()
    {
        // クリックされたことを通知し、カウントを増やす
        clickCounter.IncrementClickCount();
        clickCount++;

        if (clickCount >= 3)
        {
            Vector3 currentPosition = transform.position;

            // クリック回数が5回に達したら位置を変更
            Vector3 newPosition = new Vector3(Random.Range(-0.8f, 0.8f), Random.Range(-0.4f, 0.7f), -1.36f);
            transform.position = newPosition;

            if (clickEffectPrefab != null)
            {
                GameObject effectInstance = Instantiate(clickEffectPrefab, currentPosition, Quaternion.identity);
                Destroy(effectInstance, 1.0f); // 2秒後にエフェクトを削除
            }

            if (clickSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(clickSound);
            }

            clickCount = 0; // クリック回数をリセット
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
