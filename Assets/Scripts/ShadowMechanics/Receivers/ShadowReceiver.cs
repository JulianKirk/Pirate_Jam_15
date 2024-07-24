using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.ShadowMechanics.Receivers
{
    public abstract class ShadowReceiver : MonoBehaviour
    {
        public List<Vector3> GetLightDetectionPoints()
        {
            var colliderCorners = new List<Vector3>();

            var transform = gameObject.transform;

            colliderCorners.Add(transform.position);

            var colliders = gameObject.GetComponents<Collider>();

            foreach (var collider in colliders)
            {
                var center = collider.bounds.center;
                var extents = collider.bounds.extents;

                colliderCorners.Add(center + new Vector3(extents.x, extents.y, extents.z)); //Equivalent to max
                colliderCorners.Add(center + new Vector3(extents.x, -extents.y, extents.z));
                colliderCorners.Add(center + new Vector3(extents.x, -extents.y, -extents.z));
                colliderCorners.Add(center + new Vector3(extents.x, extents.y, -extents.z));
                colliderCorners.Add(center + new Vector3(-extents.x, extents.y, extents.z));
                colliderCorners.Add(center + new Vector3(-extents.x, -extents.y, extents.z));
                colliderCorners.Add(center + new Vector3(-extents.x, extents.y, -extents.z));
                colliderCorners.Add(center + new Vector3(-extents.x, -extents.y, -extents.z)); //Equivalent to min
            }

            return colliderCorners;
        }

        public abstract void OnEnterShadow();

        public abstract void OnExitShadow();
    }
}
