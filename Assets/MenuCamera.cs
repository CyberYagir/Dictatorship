using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCamera : MonoBehaviour
{
    public float y;
    public float sens;
    public float lerpSpeed;
    private void Update()
    {
        y += Input.GetAxisRaw("Mouse X") * sens;

        if (y < -5)
        {
            y = -5;
        }
        if (y > 5)
        {
            y = 5;
        }
        transform.localEulerAngles = Vector3.Lerp(transform.localEulerAngles, new Vector3(0, 205 + y, 0), lerpSpeed * Time.deltaTime);
        y -= Time.deltaTime;
    }
}

