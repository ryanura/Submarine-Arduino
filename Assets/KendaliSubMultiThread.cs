using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;
using UnityEngine.UI;
using System.Threading;

public class KendaliSubMultiThread : MonoBehaviour
{
    public float subB; //belok
    public float subN; //naik
    public float subM; //mundur
    public float sub; //maju
    public float subT; //tembak

    private Queue inputQueue;
    private Queue outputQueue;

    [SerializeField]
    bool threadRunning;
    public bool looping;
    Thread thread;
    SerialPort serial = new SerialPort("COM4", 9600);
    // Use this for initialization
    void Start()
    {
        serial = new SerialPort("COM4", 9600);
        OpenConnection();
        //serial.Open();
        serial.ReadTimeout = 25;

        thread = new Thread(ThreadWork);
        thread.Start();
    }

    public void StartThreading()
    {
        //serial = new SerialPort("\\\\.\\COM18", 9600);
        //serial.Open();
        //serial.ReadTimeout = 25;

        thread = new Thread(ThreadWork);
        thread.Start();
    }

    public void OpenConnection()
    {
        if (serial != null)
        {
            if (serial.IsOpen)
            {
                //serial.Close();
                Debug.Log("Closing port, because it was already open!");
            }
            else
            {
                serial.Open();
                serial.ReadTimeout = 50;
                Debug.Log("Port Opened!");
            }
        }
        else
        {
            if (serial.IsOpen)
            {
                Debug.Log("Port Opened!");
            }
            else
            {
                Debug.Log("Port Opened!");
            }
        }
    }

    public void CloseConnection()
    {
        if (serial != null)
        {
            if (serial.IsOpen)
            {
                serial.Close();
                Debug.Log("Closing port!");
            }
            else
            {
                Debug.Log("Port Closed!");
            }
        }
        else
        {
            Debug.Log("Port == null");
        }
    }

    public bool IsLooping()
    {
        lock (this)
        {
            return looping;
        }
    }

    public bool StopThreadLoop()
    {
        Debug.Log("StopTh");
        lock (this)
        {
            return looping = false;
        }
    }

    public bool StartThreadLoop()
    {
        Debug.Log("Start");
        lock (this)
        {
            return looping = true;
        }
    }

    public void ThreadWork()
    {
        Debug.Log("asdsad");
        looping = true;

        var i = System.Diagnostics.Process.GetCurrentProcess();
        int j = i.Threads.Count;
        Debug.Log(i + " " + j);

        //This pattern lets us interrupt the work at a safe point if neeeded.
        while (looping == true)
        {
            string[] values = serial.ReadLine().Split(',');
            subB = (float.Parse(values[0]));
            subN = (float.Parse(values[1]));
            sub = (float.Parse(values[2]));
            subM = (float.Parse(values[3]));
            subT = (float.Parse(values[4]));
        }

        DisableThread();
    }

    /*public string ReadFromArduino (int timeout )
    {
        return timeout.ToString();
       
        if (inputQueue.Count == 0;
        return null;

        return (string) inputQueue.Dequeue();
    }*/

    void ThreadLoopQueue()
    {
        serial.ReadTimeout = 50;
        serial.Open();

        /*while (StopThreadLoop())
        {
             Read from Arduino
		    string result = ReadFromArduino(50);
		    if (result != null)
			    inputQueue.Queue(result);
        
        serial.Close();
        }*/
    }

    void OnDisable()
    {
        Debug.Log("OnDisable");
        // If the thread is still running, we should shut it down,
        // otherwise it can prevent the game from exiting correctly.
        if (threadRunning)
        {
            // This forces the while loop in the ThreadedWork function to abort.
            threadRunning = false;

            // This waits until the thread exits,
            // ensuring any cleanup we do after this is safe. 
            thread.Join();
        }
        // Thread is guaranteed no longer running. Do other cleanup tasks.
    }

    public void DisableThread()
    {
        if (looping)
        {
            // This forces the while loop in the ThreadedWork function to abort.
            StopThreadLoop();
            looping = false;
            //This waits until the thread exits,
            //ensuring any cleanup we do after this is safe.
            thread.Join();
            int i = System.Diagnostics.Process.GetCurrentProcess().Threads.Count;
            Debug.Log(i);
            Debug.Log("DisableThIf");
        }
    }

    private void OnApplicationQuit()
    {
        serial.Close();
        Debug.Log("onQuit");
        StopThreadLoop();
        thread.Abort();
    }
}
