using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    public GameObject tracedObject;
    public UnityEngine.UI.Text text;
	
	void Update ()
    {
        if (tracedObject != null && text != null)
        {
            text.text = tracedObject.transform.position.ToString();
        }
	}
}
