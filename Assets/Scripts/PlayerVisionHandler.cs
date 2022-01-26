using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;
using EventBusSystem;

public class PlayerVisionHandler : MonoBehaviour, IInventoryHandler
{
    public Light2D PlayerVisionLight;

    private float visionMaxOuterAngle;
    private float visionMaxInnerAngle;

    private void OnEnable()
    {
        EventBus.Subscribe(this);
    }

    private void OnDisable()
    {
        EventBus.Unsubscribe(this);
    }

    private void Start()
    {
        visionMaxOuterAngle = PlayerVisionLight.pointLightOuterAngle;
        visionMaxInnerAngle = PlayerVisionLight.pointLightInnerAngle;
    }

    IEnumerator UpdateVision(float outerTarget, float innerTarget)
    {
        float elapsedTime = 0;
        float waitTime = 0.5f;
        float outerCurrent = PlayerVisionLight.pointLightOuterAngle;
        float innerCurrent = PlayerVisionLight.pointLightInnerAngle;

        while (elapsedTime < waitTime)
        {
            PlayerVisionLight.pointLightOuterAngle = Mathf.Lerp(outerCurrent, outerTarget, (elapsedTime / waitTime));
            PlayerVisionLight.pointLightInnerAngle = Mathf.Lerp(innerCurrent, innerTarget, (elapsedTime / waitTime));
            elapsedTime += Time.deltaTime;

            yield return null;
        }
        PlayerVisionLight.pointLightOuterAngle = outerTarget;
        PlayerVisionLight.pointLightInnerAngle = innerTarget;
        yield return null;
    }

    public void OnOpenInventory()
    {
        StartCoroutine(UpdateVision(0, 0));
    }

    public void OnCloseInventory()
    {
        StartCoroutine(UpdateVision(visionMaxOuterAngle, visionMaxInnerAngle));
    }
}
