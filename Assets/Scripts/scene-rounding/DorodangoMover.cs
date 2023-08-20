using UnityEngine;

public class DorodangoMover : MonoBehaviour
{
    [SerializeField] private Transform hand;
    private Vector3 objPos;
    private float initialY;

    private void Start()
    {
        initialY = transform.position.y;
    }

    private void Update()
    {
        objPos += hand.position - transform.position;
        objPos.y = initialY;
        transform.position += objPos *= Time.deltaTime;
    }
}
