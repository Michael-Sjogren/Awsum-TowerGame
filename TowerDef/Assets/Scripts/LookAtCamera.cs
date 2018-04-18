using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour 
{
	// Update is called once per frame
	void Update () {
		this.transform.rotation = Quaternion.LookRotation( this.transform.position - Camera.main.transform.position );
	}
}
