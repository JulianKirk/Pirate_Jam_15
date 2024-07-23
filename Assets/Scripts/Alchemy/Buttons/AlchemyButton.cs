using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlchemyButton : MonoBehaviour
{
    public AlchemyReceiver Receiver;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Receiver.Activate();
        }
    }
}
