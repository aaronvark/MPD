using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystem : MonoBehaviour {

	public float[][] level;

	IFacade levelPrintScanner;
	bool printing = false;

	void Start() {
		//we could get access to the Facade in a more loose-coupled way
		levelPrintScanner = new Facade();
		levelPrintScanner.onLevelScanComplete += LevelScanned;
	}

	void Update() {
		if ( !printing && Input.GetKeyUp( KeyCode.Space ) ) {
			printing = true;
			levelPrintScanner.PrintLevel(level, PrintSuccess, PrintFailed );
		}
	}

	void OnApplicationQuit() {
		levelPrintScanner.Dispose();
	}

	void PrintSuccess() {
		printing = false;
		Debug.Log("YAY DONE");
	}

	void PrintFailed() {
		printing = false;
		Debug.Log("WHOOPS!");
	}

	void LevelScanned( float[][] level ) {
		Debug.Log( "Got level!" );
		this.level = level; 
		levelPrintScanner.PrintLevel(level, null, null);
	}
}
