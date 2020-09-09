using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeCamera : MonoBehaviour
{
    // Transform of the camera to shake. Grabs the gameObject's transform
    // if null.
    public Transform camTransform;

    // How long the object should shake for.
    public float shakeDuration = 0f;
    float _shakeDuration;
    public bool shake;
    // Amplitude of the shake. A larger value shakes the camera harder.
    public float shakeAmount = 0.7f;
    public float decreaseFactor = 1.0f;

    public bool useUnscaledTime = false;

    Vector3 originalPos;

    public void Shake()
    {
        shake = true;
    }

    void Awake()
    {
        _shakeDuration = shakeDuration;
        if (camTransform == null)
        {
            camTransform = GetComponent(typeof(Transform)) as Transform;
        }
    }

    void OnEnable()
    {
        originalPos = camTransform.localPosition;
    }

    void Update()
    {
        if (shake)
        {
            if (_shakeDuration > 0)
            {
                camTransform.localPosition = originalPos + Random.insideUnitSphere * shakeAmount;

                if (!useUnscaledTime)
                    _shakeDuration -= Time.deltaTime * decreaseFactor;
                else
                    _shakeDuration -= Time.unscaledDeltaTime * decreaseFactor;
            }
            else
            {
                shake = false;
                _shakeDuration = shakeDuration;
                camTransform.localPosition = originalPos;
            }
        }
        else
        {

        }
    }
}
