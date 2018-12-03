using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

public class ConsolePrintService : IPrintService {
	public void Print(string[] lines, Callback onSuccess, Callback onFailure ) {
		//kicks off a print sequence to a specific device... in this case the Console
		StringBuilder sb = new StringBuilder();
		foreach( string s in lines ) {
			sb.AppendLine(s);
		}
		Debug.Log(sb.ToString());

		if ( onSuccess != null ) {
			onSuccess();
		}
	}
}
