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
        lightSources = new List<Light>();

        var shadowReceivers = Object.FindObjectsOfType<ShadowReceiver>();

        foreach (var receiver in shadowReceivers)
        {
            shadowReceiverStates[receiver] = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void CheckIfInShadow(ShadowReceiver receiver)
    {

    }
}
