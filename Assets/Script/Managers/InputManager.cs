using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {
	public static IInputSystem CreateInstance() {
		#if IETS
		//
		#endif

		#if UNITY_ANDROID || UNITY_IPHONE
			return new MobileInputSystem();
		#elif UNITY_STANDALONE
			return new PCInputSystem();
		#elif UNITY_EDITOR
			return new PCInputSystem();
		#else
			Debug.LogError("Unsupported platform");
			return new PCInputSystem(); 
		#endif
	}
}

public abstract class AInputSystem : IInputSystem {
	//abstract base class allows you to implement dummy or similar behaviour
	public virtual float Horizontal() {
		return 0;
	}
	
	public virtual float Vertical() {
		return 0;
	}
}

public class MobileInputSystem : AInputSystem {
	//TODO: swipe-controls, or virtual joystick
}

public class PCInputSystem : AInputSystem {
	//keyboard & mouse controls
	public override float Horizontal() {
		return Input.GetAxis("Horizontal");
	}

	public override float Vertical() {
		return Input.GetAxis("Vertical");
	}
}
