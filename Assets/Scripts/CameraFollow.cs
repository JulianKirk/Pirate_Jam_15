using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform CamTarget;
    public Transform FollowTarget;
    public FollowState currState = FollowState.DEADZONE;
    public double deadZoneRadius = 3;
    public double softZoneStopRadius = 0.5;
    public float lerpStrength = 0.01f;

    public enum FollowState
    {
        DEADZONE,
        SOFTZONE
    }

    // Update is called once per frame
    void LateUpdate()
    {
        // Update the state machine
        switch (currState)
        {
            case FollowState.DEADZONE when (FollowTarget.localPosition - CamTarget.localPosition).magnitude > deadZoneRadius:
                currState = FollowState.SOFTZONE;
                break;
            case FollowState.SOFTZONE when (FollowTarget.localPosition - CamTarget.localPosition).magnitude < softZoneStopRadius:
                currState = FollowState.DEADZONE;
                break;
        }

        // Run the FSM
        switch (currState)
        {
            case FollowState.SOFTZONE:
                CamTarget.localPosition = Vector3.Lerp(CamTarget.localPosition, FollowTarget.localPosition, lerpStrength);
                break;
            default:
                break;
        }
    }

}