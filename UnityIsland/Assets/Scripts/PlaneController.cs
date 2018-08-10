using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float m_speed = 50;
    public float m_rotationSpeed = 40;
    public float m_acceleration = 5;

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var t = this.gameObject.transform;


        var rotation = new Vector3();
        rotation.x = Input.GetAxis("Vertical") * m_rotationSpeed * Time.deltaTime;
        rotation.y = Input.GetAxis("Horizontal") * m_rotationSpeed * Time.deltaTime;
        //rotation.z = Input.GetAxis("Axe3") * _rotationSpeed * Time.deltaTime;
        t.Rotate(rotation);


        m_speed += Input.GetAxis("Throttle") * m_acceleration * Time.deltaTime;
        m_speed = m_speed < 0 ? 0 : m_speed;
        t.position += t.forward * m_speed * Time.deltaTime;
	}
}
