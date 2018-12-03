using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class RandomScanService : IScanService {

    public event ScanEvent onRead;

    System.Random randInstance;
    bool running = false;

    public RandomScanService( int seed ) {
        //Here we initialize anything we need to perform scanning, such as connecting to external libraries or devices...
        randInstance = new System.Random(seed);

        //Since this is a scanner, this is typically something that needs to "poll" an external device
        //this could be done through a socket-style connection of some sort, which usually runs on a separate Thread
        Thread t = new Thread(ThreadFunc);
        t.Start();
    }

    public void Stop() {
        running = false;
    }

    void ThreadFunc() {
        running = true;

        //we basically keep polling the thread until our application gets killed off
        while( running ) {
            //5% chance that we read something
            if ( randInstance.NextDouble() < .05f )  {
                //return some randomly read data
                List<string> randomData = new List<string>() { 
                    randInstance.NextDouble().ToString(),
                    randInstance.NextDouble().ToString(),
                    randInstance.NextDouble().ToString(),
                    randInstance.NextDouble().ToString(),
                    randInstance.NextDouble().ToString(),
                    randInstance.NextDouble().ToString(),
                    randInstance.NextDouble().ToString(),
                    randInstance.NextDouble().ToString()
                };
                if ( onRead != null ) {
                    onRead( randomData.ToArray() );
                }
            }
            //read 10x per seconds, by waiting 100ms every time
            Thread.Sleep(100);
        }
    }
}
