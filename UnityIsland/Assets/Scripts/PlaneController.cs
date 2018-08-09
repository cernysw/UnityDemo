using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float m_speed = 59;
    public float m_rotationSpeed = 59;

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


        //t.Rotate(t.right, Input.GetAxis("Vertical") * m_rotationSpeed * Time.deltaTime, Space.Self);
        //t.Rotate(t.up, Input.GetAxis("Horizontal") * m_rotationSpeed * Time.deltaTime, Space.Self);
        t.position += t.forward * m_speed * Time.deltaTime;
	}
}
