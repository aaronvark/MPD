using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPrintService {
	void Print(string[] lines, Callback onSuccess, Callback onFailure );
}

public delegate void ScanEvent( string[] linesRead );

public interface IScanService {
	event ScanEvent onRead;
	void Stop();
}
