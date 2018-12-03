using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public delegate void LevelEvent( float[][] levelData );
public delegate void Callback();

//The interface that defines how we communicate with this facade
public interface IFacade {
	event LevelEvent onLevelScanComplete;
	void PrintLevel( float[][] level, Callback success, Callback failure );
	void Dispose();
}

//This facade has a specific function in relation to the code.
// This function consists of many different parts, but it's much simpler to "wrap" this in a 
//  simplified interface.
public class Facade : IFacade {
	public event LevelEvent onLevelScanComplete;

	IScanService scanner;
	IPrintService printer;

	public Facade() {
		//construct all the instances of relevant external libraries / systems / etc.
		scanner = new RandomScanService(0);
		printer = new ConsolePrintService();

		scanner.onRead += LevelRead;
	}

	public void PrintLevel( float[][] level, Callback success, Callback failure ) {
		printer.Print( LevelToText( level ), success, failure );
	}

	public void Dispose() {
		//clean up all the resources
		scanner.Stop();
	}

	void LevelRead( string[] linesRead ) {
		if ( onLevelScanComplete != null ) {
			onLevelScanComplete( TextToLevel( linesRead ) );
		}
	}

	//Functions to convert level format to strings (and vice versa) (this could even be a separate conversion class)
	// We could test if this works by confirming we get the same back as we put in (when we actually read back from printed data)
	//  In general it's a good idea to write these kinds of tests
	string[] LevelToText(float[][] level) {
		List<string> lines = new List<string>();
		for( int x = 0; x < level.Length; ++x ) {
			for( int y = 0; y < level[x].Length; ++y ) {
				lines.Add(level[x][y].ToString());
			}
		}
		return lines.ToArray();
	}

	//We're assuming a 4x2 level in this case (for simplicity)
	// this could also be a setting passed to the Facade (width/height)
	float[][] TextToLevel( string[] lines ) {
		float[][] level = new float[][] { 
			new float[] { float.Parse(lines[0]), float.Parse(lines[1]), float.Parse(lines[2]), float.Parse(lines[3]) },
			new float[] { float.Parse(lines[4]), float.Parse(lines[5]), float.Parse(lines[6]), float.Parse(lines[7]) }
		};

		return level;
	}
}