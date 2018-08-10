using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControler : MonoBehaviour
{
    public GameObject m_observerObject;
    public float m_distance = 40;
    public float m_angleX = 0;
    public float m_angleY = 0;
    public float m_rotationSpeed = 150;


    void Update()
    {
        if (Input.GetButton("Fire2"))
        {
            m_angleY -= Input.GetAxis("Mouse X") * -m_rotationSpeed * Time.deltaTime;
            m_angleX += Input.GetAxis("Mouse Y") * -m_rotationSpeed * Time.deltaTime;
        }
    }
    

    void LateUpdate ()
    {
        var target = m_observerObject.transform;

        // compute desired location
        var desiredWorldPosition = target.TransformPoint(Quaternion.Euler(m_angleX, m_angleY, 0) * (-Vector3.forward * m_distance));

        // move to desired position
        this.gameObject.transform.position = desiredWorldPosition;
        this.transform.LookAt(m_observerObject.transform);
    }
}
