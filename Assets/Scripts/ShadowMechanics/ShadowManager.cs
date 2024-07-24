using Assets.Scripts.ShadowMechanics.Receivers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class LightManager : MonoBehaviour
{
    Dictionary<ShadowReceiver, bool> shadowReceiverStates;
    List<Light> lightSources;

    // Start is called before the first frame update
    void Start()
    {
        shadowReceiverStates = new Dictionary<ShadowReceiver, bool>();
        lightSources = Object.FindObjectsOfType<Light>().ToList();

        var shadowReceivers = Object.FindObjectsOfType<ShadowReceiver>();

        foreach (var receiver in shadowReceivers)
        {
            shadowReceiverStates.Add(receiver, CheckIfInShadow(receiver));
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
        var lightDetectionPoints = receiver.GetLightDetectionPoints();

        foreach (var light in lightSources)
        {
            switch (light.type)
            {
                case LightType.Directional:
                    var direction = light.transform.rotation.eulerAngles;

                    foreach (var target in lightDetectionPoints)
                    {
                        if (Physics.Raycast(target, -direction)) //If it hits any object it is in shadow
                        {
                            return true;
                        }
                    }
                    break;
                case LightType.Spot:
                    foreach (var target in lightDetectionPoints)
                    {
                            var innerSpotAngle = light.innerSpotAngle;
                        var spotAngle = light.spotAngle;

                        var rotation = Quaternion.Angle(Quaternion.LookRotation((target - light.transform.position).normalized, Vector3.up),
                            Quaternion.LookRotation(light.transform.forward, light.transform.up));
                    }
                    break;
                case LightType.Point:
                    foreach (var target in lightDetectionPoints)
                    {
                        if (Physics.Raycast(light.transform.position, (target - light.transform.position).normalized, out RaycastHit hitInfo))
                        {
                            if (hitInfo.transform.gameObject == receiver.gameObject) //If it hit a collider attached to the receiver game object
                            {
                                return false;
                            }
                        }
                    }
                    break;
                case LightType.Area:
                    break;
                default:
                    Debug.Log("Light type not taken care of");
                    break;
            }
        }

        return false;
    }
}
