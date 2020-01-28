using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.Networking;
using Unity.Jobs;
using System;

public class UDPReceiver : MonoBehaviour
{
    // read Thread
    Thread readThread;

    // udpclient object
    UdpClient client;

    // port number
    public int port = 5300;

    // UDP packet store
    public string lastReceivedPacket = "";
    //public string allReceivedPackets = ""; // this one has to be cleaned up from time to time

    public JSONDistributor distributor;

    // start from unity3d
    void Start()
    {
        // create thread for reading UDP messages
        readThread = new Thread(new ThreadStart(ReceiveData));
        readThread.IsBackground = true;
        readThread.Start();
    }

    // Unity Update Function
    void Update()
    {
        // check button "s" to abort the read-thread
        if (Input.GetKeyDown("q"))
            stopThread();
    }

    // Unity Application Quit Function
    void OnApplicationQuit()
    {
        stopThread();
    }

    // Stop reading UDP messages
    private void stopThread()
    {
        if (readThread.IsAlive)
        {
            readThread.Abort();
        }
        client.Close();
    }

    // receive thread function
    private void ReceiveData()
    {
        client = new UdpClient(port);
        Debug.Log($">> Started receiving data from UWB Sensor server on port: {port}");
        //client.Client.Blocking = false;
        while (true)
        {
            try
            {
                // receive bytes
                IPEndPoint anyIP = new IPEndPoint(IPAddress.Any, 0);
                byte[] data = client.Receive(ref anyIP);

                // encode UTF8-coded bytes to text format
                string text = Encoding.UTF8.GetString(data);

                // show received message
                //print(">> " + text);
                distributor.DistributeDataToClients(text);

                // store new massage as latest message
                lastReceivedPacket = text;

                // update received messages
                //allReceivedPackets = allReceivedPackets + text;

            }
            catch (Exception err)
            {
                print(err.ToString());
            }
        }
    }

    // return the latest message
    public string getLatestPacket()
    {
        //allReceivedPackets = "";
        return lastReceivedPacket;
    }
}
