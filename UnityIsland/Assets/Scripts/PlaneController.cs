using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float m_speed = 50;
    public float m_rotationSpeed = 40;
    public float m_acceleration = 5;
    public float m_holdHorizontal = 0;
    public float m_holdVertical = 0;

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var t = this.gameObject.transform;

        if (Input.GetKeyDown(KeyCode.Keypad6)) m_holdHorizontal = +1;
        if (Input.GetKeyDown(KeyCode.Keypad4)) m_holdHorizontal = -1;
        if (Input.GetKeyDown(KeyCode.Keypad8)) m_holdVertical = +1;
        if (Input.GetKeyDown(KeyCode.Keypad2)) m_holdVertical = -1;
        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            m_holdVertical = m_holdHorizontal = 0;
        }


        var rotation = new Vector3();
        rotation.x = (m_holdVertical + Input.GetAxis("Vertical")) * m_rotationSpeed * Time.deltaTime;
        rotation.y = (m_holdHorizontal + Input.GetAxis("Horizontal")) * m_rotationSpeed * Time.deltaTime;
        //rotation.z = Input.GetAxis("Axe3") * _rotationSpeed * Time.deltaTime;
        t.Rotate(rotation);


        m_speed += Input.GetAxis("Throttle") * m_acceleration * Time.deltaTime;
        m_speed = m_speed < 0 ? 0 : m_speed;
        t.position += t.forward * m_speed * Time.deltaTime;
	}
}
