using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float speed;
    public float sens;
    public float constY;
    public float smoothness;
    float rotationY;
    public Camera camera;
    public float y;
    public LayerMask layer;
    public Vector3 lastPos;
    private void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + transform.forward + new Vector3(0,20,0), Vector3.down, out hit, 99, layer, QueryTriggerInteraction.Ignore))
        {
            if (hit.collider != null && hit.transform.gameObject.layer != layer)
            {
                lastPos = hit.point;
                y = hit.point.y + constY;
            }
            else
            {
                y = constY;
            }
        }
        transform.position = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y, transform.position.z), smoothness * Time.deltaTime);

        transform.Translate(Vector3.forward * Input.GetAxis("Vertical") * speed * Time.deltaTime * constY);
        transform.Translate(Vector3.right * Input.GetAxis("Horizontal") * speed * Time.deltaTime * constY);

        if (Input.GetKeyDown(KeyCode.Mouse2))
        {
            Cursor.visible = false;
        }
        if (Input.GetKey(KeyCode.Mouse2))
        {
            float yrot = Input.GetAxisRaw("Mouse X");
            Vector3 rot = new Vector3(0, yrot, 0f) * 1;
            transform.rotation = (transform.rotation * Quaternion.Euler(rot));
            float xrot = Input.GetAxisRaw("Mouse Y");
            Vector3 camrot = new Vector3(-xrot, 0, 0f) * 1;
            camera.transform.rotation = (camera.transform.rotation * Quaternion.Euler(camrot)); 
            
            rotationY += Input.GetAxis("Mouse Y") * sens;
            rotationY = Mathf.Clamp(rotationY, -80, 80);

            camera.transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }
        if (Input.GetKeyUp(KeyCode.Mouse2))
        {
            Cursor.visible = true;
        }
    }

    public void AddY()
    {
        constY++;
        if (constY >= 11)
        {
            constY = 10;
        }
    }
    public void SubY()
    {
        constY--;
        if (constY < 2)
        {
            constY = 2;
        }
    }
}
