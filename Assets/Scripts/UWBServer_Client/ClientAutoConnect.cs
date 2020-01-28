using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//namespace UWBServer_Client
//{
    public class ClientAutoConnect : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            DoConnect();
        }

        public void DoConnect()
        {
        UWBServer_Client.Client.instance.ConnectToServer();

        }
    }
//}
