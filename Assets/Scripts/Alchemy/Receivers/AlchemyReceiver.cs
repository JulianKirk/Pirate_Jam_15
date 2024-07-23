using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyReceiver : MonoBehaviour
{
    public void Activate()
    {
        Activate(null);
    }

    public virtual void Activate(Collider other) { }
}
