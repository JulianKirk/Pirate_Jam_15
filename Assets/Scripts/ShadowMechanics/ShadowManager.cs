using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LightManager : MonoBehaviour
{
    Dictionary<ShadowReceiver, bool> shadowReceiverStates;
    List<Light> lightSources;

    // Start is called before the first frame update
    void Start()
    {
        lightSources = Object.FindObjectsOfType<Light>().ToList();

        var shadowReceivers = Object.FindObjectsOfType<ShadowReceiver>();

        foreach (var receiver in shadowReceivers)
        {
            shadowReceiverStates[receiver] = CheckIfInShadow(receiver);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (var (receiver, wasInShadow) in shadowReceiverStates) 
        {
            var inShadow = CheckIfInShadow(receiver);

            if (inShadow != wasInShadow)
            {
                if (inShadow == true)
                {
                    receiver.OnEnterShadow();
                }
                else
                {
                    receiver.OnExitShadow();
                }
            }
        }
    }

    bool CheckIfInShadow(ShadowReceiver receiver)
    {
        foreach (var light in lightSources)
        {
            foreach (var target in receiver.ColliderCorners)
            {
                if (Physics.Raycast(light.transform.position, (target - light.transform.position).normalized, out RaycastHit hitInfo))
                {
                    if (hitInfo.transform.gameObject == receiver.gameObject) //If it hit a collider attached to the receiver game object
                    {
                        return true;
                    }
                }
            }
        }

        return false;
    }
}
