using System.Linq;
using Dorodango.Structs;
using UnityEngine;

public class RandomizeXZPositionOfChildren : MonoBehaviour
{
    [SerializeField] private VectorXZ minPosition;
    [SerializeField] private VectorXZ maxPosition;

    private void Start()
    {
        // Transform は IEnumerable を実装しており，直接の子オブジェクトの Transform が取得できる
        // cf: https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Transform.html
        var children = transform.OfType<Transform>();
        foreach (var child in children) RandomizeXZPosition(child);
    }

    private void RandomizeXZPosition(Transform t)
    {
        var newX = Random.Range(minPosition.x, maxPosition.x);
        var newZ = Random.Range(minPosition.z, maxPosition.z);
        t.position = new Vector3(newX, t.position.y, newZ);
    }
}
