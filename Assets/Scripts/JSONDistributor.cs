using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UWBServer_Client;

public class JSONDistributor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DistributeDataToClients(string _data)
    {
        ServerSend.JsonDataToAll(_data);
    }
}
