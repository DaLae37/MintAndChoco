using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class cameraRot : MonoBehaviour
{
    private Touch initTouch = new Touch();
    public Camera cam;
    private float rotX = 0.0f;
    private float rotY = 0.0f;
    private Vector3 origRot;
    public float rotSpeed = 1.0f;
    public float dir = -1;
    private int whatSilde = -1;
    // Use this for initialization
    void Start()
    {
        rotSpeed = 1.0f;
        origRot = cam.transform.eulerAngles;
        rotX = origRot.x;
        rotY = origRot.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {

        rotSpeed = 1.0f;
        
        if (Input.touchCount > 0)
        {
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (EventSystem.current.IsPointerOverGameObject(i) == false)
                {
                    Touch touch = Input.GetTouch(i);
                    if (touch.phase == TouchPhase.Began)
                    {
                        whatSilde = i;
                        initTouch = touch;
                    }
                    else if (touch.phase == TouchPhase.Moved)
                    {
                        if (whatSilde == i)
                        {
                            float deltaX = initTouch.position.x - touch.position.x;
                            float deltaY = initTouch.position.y - touch.position.y;
                            rotX -= deltaY * Time.deltaTime * rotSpeed * dir;
                            rotY += deltaX * Time.deltaTime * rotSpeed * dir;
                            rotX = Mathf.Clamp(rotX, -45f, 45f);
                            cam.transform.eulerAngles = new Vector3(rotX, rotY, 0f);
                            
                        }
                    }
                    else if (touch.phase == TouchPhase.Ended)
                    {
                        initTouch = new Touch();
                        if (whatSilde == i)
                            whatSilde = -1;
                    }
                }
            }

        }
    }
}
