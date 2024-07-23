using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowReceiver : MonoBehaviour
{
    public List<Vector3> ColliderCorners;

    // Start is called before the first frame update
    void Awake()
    {
        var transform = gameObject.transform;

        ColliderCorners.Add(transform.position);

        var colliders = gameObject.GetComponents<Collider>();

        foreach (var collider in colliders)
        {
            var center = collider.bounds.center;
            var extents = collider.bounds.extents;

            ColliderCorners.Add(center + new Vector3(extents.x, extents.y, extents.z)); //Equivalent to max
            ColliderCorners.Add(center + new Vector3(extents.x, -extents.y, extents.z));
            ColliderCorners.Add(center + new Vector3(extents.x, -extents.y, -extents.z));
            ColliderCorners.Add(center + new Vector3(extents.x, extents.y, -extents.z));
            ColliderCorners.Add(center + new Vector3(-extents.x, extents.y, extents.z));
            ColliderCorners.Add(center + new Vector3(-extents.x, -extents.y, extents.z));
            ColliderCorners.Add(center + new Vector3(-extents.x, extents.y, -extents.z));
            ColliderCorners.Add(center + new Vector3(-extents.x, -extents.y, -extents.z)); //Equivalent to min
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public virtual void OnEnterShadow()
    {
    }
    
    public virtual void OnExitShadow()
    {
    }
}
