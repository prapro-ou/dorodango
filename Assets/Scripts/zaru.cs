using UnityEngine;

public class zaru : MonoBehaviour
{
    private Vector3 mOffset;

    private Vector3 lastMousePosition;
    private bool[] soundPlayedArray;
    private int shakeCount = 0;
    public float[] shakeThresholds;

    private float mZCoord;
    private Vector2 movementLimits;
    private Vector2 startPosition;
    private float distanceMoved;
    private float initialScaleX;
    private float initialScaleY;
    private float initialScaleZ;
    private float initialPositionX;
    private float initialPositionY;
    private float initialPositionZ;
    public float sum;

    private AudioSource audioSource;
    public AudioClip shakeSound;

    public float verticalSpeed = 0.0001f;

    public GameObject Cylinder;



    void Start(){
        movementLimits=new Vector2(0.2f,0.2f);
        initialScaleX = Cylinder.transform.localScale.x;
        initialScaleY = Cylinder.transform.localScale.y;
        initialScaleZ = Cylinder.transform.localScale.z;
        initialPositionX=Cylinder.transform.position.x;
        initialPositionY=Cylinder.transform.position.y;
        initialPositionZ=Cylinder.transform.position.z;
        audioSource = GetComponent<AudioSource>();
        soundPlayedArray = new bool[shakeThresholds.Length];
    }

    void OnMouseDown()
    {
        mZCoord = Camera.main.WorldToScreenPoint(gameObject.transform.position).z;
        mOffset = gameObject.transform.position - GetMouseWorldPos();
        mOffset.y = 0f;
        startPosition = Input.mousePosition;
        distanceMoved = 0f;
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mousePoint = Input.mousePosition;
        mousePoint.z = mZCoord;
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    void OnMouseDrag()
    {
        Vector3 desiredPosition = GetMouseWorldPos() + mOffset;
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, -movementLimits.x, movementLimits.x);
        desiredPosition.z = Mathf.Clamp(desiredPosition.z, -movementLimits.y, movementLimits.y);
        desiredPosition.y = transform.position.y;
        transform.position = desiredPosition;

        Vector2 currentPosition = Input.mousePosition;
        distanceMoved += Vector2.Distance(currentPosition, startPosition)/1000f;
        
        startPosition = currentPosition;
        sum+=distanceMoved;
        distanceMoved=0f;
    }

    void Update(){
        Debug.Log("Distance Moved: " + sum);
        Cylinder.transform.localScale=new Vector3(initialScaleX,-sum * 0.01f + initialScaleY,initialScaleZ);
        

        if(-sum * 0.01f + initialScaleY<0){
            Cylinder.gameObject.SetActive(false);
        }


        if(shakeCount >=shakeThresholds.Length)
            return;
        

        for (int i = 0; i < shakeThresholds.Length; i++)
        {
            if (sum > shakeThresholds[i] && !soundPlayedArray[i])
            {
                audioSource.PlayOneShot(shakeSound);
                soundPlayedArray[i] = true; // 効果音が再生されたことを記録
                shakeCount++; // カウンターを増やす
                break;
            }
        }
    }



}