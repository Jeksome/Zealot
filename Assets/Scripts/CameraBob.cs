using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBob : MonoBehaviour
{
    public Transform headTransform;
    public Transform cameraTransform;

    public float bobFrequency = 5f;
    public float bobHorizontalAmplitude = 0.1f;
    public float bobVerticalAmplitude = 0.1f;
    [Range(0, 1)] public float headBobSmoothing = 0.1f;

    public bool isRunning;
    private float runningTime;
    private Vector3 targetCameraPosition;

    void Update()
    {
        if (!isRunning) runningTime = 0;
        else runningTime += Time.deltaTime;

        targetCameraPosition = headTransform.position + CalculateHeadBobOffset(runningTime);

        cameraTransform.position = Vector3.Lerp(cameraTransform.position, targetCameraPosition, headBobSmoothing);

        if ((cameraTransform.position - targetCameraPosition).magnitude <= 0.001)
            cameraTransform.position = targetCameraPosition;
    }

    private Vector3 CalculateHeadBobOffset(float t)
    {
        float horizontalOffset = 0;
        float verticalOffset = 0;
        Vector3 offset = Vector3.zero;

        if (t>0)
        {
            horizontalOffset = Mathf.Cos(t * bobFrequency) * bobHorizontalAmplitude;
            verticalOffset = Mathf.Sin(t * bobFrequency * 2) * bobVerticalAmplitude;

            offset = headTransform.right * horizontalOffset + headTransform.up * verticalOffset;
        }

        return offset;
    }
}
