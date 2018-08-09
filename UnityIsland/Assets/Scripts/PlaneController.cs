using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    public float m_speed = 5;
    public float m_angularSpeed = 5;

    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        var t = this.gameObject.transform;
        t.Rotate(t.right, Input.GetAxis("Vertical") * m_angularSpeed * Time.deltaTime, Space.Self);
        t.Rotate(Vector3.up, Input.GetAxis("Horizontal") * m_angularSpeed * Time.deltaTime);
        t.Translate(t.forward * m_speed * Time.deltaTime);
		
	}
}
