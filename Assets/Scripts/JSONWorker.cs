using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JSONWorker : MonoBehaviour
{
    //public List<SensorDataRaw> sensorDataRaw;
    //public SensorDataRaw currentSensorDataRaw;
    //public static JSONWorker instance;

    private UWBObjectManager UWBMan;

    private void Awake()
    {
        //if (instance == null)
        //{
        //    instance = this;
        //}
        //else if (instance != this)
        //{
        //    Debug.Log("Instance already exists, destroying object!");
        //    Destroy(this);
        //}
    }

    private void Start()
    {
        UWBMan = UWBObjectManager.i;
    }

    /// <summary>
    /// Receives raw JSON data as a string. Converts it to SensorDataRaw (contains variables and lists to store all data acquired from raw JSON) using JsonUtility. Sends converted data to ProcessCurrentData().
    /// </summary>
    /// <param name="json"></param>
    public void JsonToObject(string json)
    {
        //print(json);
        SensorDataRaw d = JsonUtility.FromJson<SensorDataRaw>(json);

        //sensorDataRaw.Add(d);
        //currentSensorDataRaw = d;
        ProcessCurrentData(d);
    }

    /// <summary>
    /// Converts raw JSON data to a SensorDataRaw object and returns it.
    /// </summary>
    /// <param name="json"></param>
    /// <returns></returns>
    public SensorDataRaw JsonToObjectReturn(string json)
    {
        SensorDataRaw d = JsonUtility.FromJson<SensorDataRaw>(json);
        return d;
    }

    private void Update()
    {
        //print(sensorDataRaw.Count);
    }

    /// <summary>
    /// Processes SensorDataRaw object data. If scene doesn't have an object with the received data address (address from tag), then it tells UWBObjectManager to create and add a new one to the scene with that address.
    /// Otherwise calls SendDataToUWBObject() to send the data to the object in scene. After that, removes that data from the data list so that next ones can be processed.
    /// </summary>
    /// <param name="currentData"></param>
    void ProcessCurrentData(SensorDataRaw currentData)
    {

        // check if there is an object with address
        if (!UWBObjectManager.i.SceneHasObjectWithAddress(currentData.address))
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => UWBObjectManager.i.CreateNewUWBObject(currentData.address)); // Calling external methods from non-main thread is not allowed, so we need a script that calls them on main thread.
        }
        else
        {
            UnityMainThreadDispatcher.Instance().Enqueue(() => SendDataToUWBObject(currentData)); // Calling external methods from non-main thread is not allowed, so we need a script that calls them on main thread.
            //SendDataToUWBObject(currentData);
        }
        //currentSensorDataRaw = null;
        //sensorDataRaw.RemoveAt(0);
    }

    /// <summary>
    /// Calls UWBObjectManager to find an object in the scene by address. If the object is found, sends sensor data to it.
    /// </summary>
    /// <param name="data"></param>
    public void SendDataToUWBObject(SensorDataRaw data)
    {
        UWBObject obj = UWBMan.FindObjectByAddress(data.address);
        if(obj != null)
            obj.ReceiveData(data);
    }

    /// <summary>
    /// Checks if there is any data in sensor data list.
    /// </summary>
    /// <returns>Returns a bool value.</returns>
    //public bool HasData()
    //{
    //    return sensorDataRaw.Count > 0;
    //}
}

