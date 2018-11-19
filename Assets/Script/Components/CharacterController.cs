using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
	IInputSystem input;

	// Use this for initialization
	void Start () {
		if ( !ServiceManager.Singleton.RequestService( out input ) ) {
			enabled = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate( input.Horizontal(), 0, input.Vertical() );
	}
}
