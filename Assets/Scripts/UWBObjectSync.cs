using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UWBObjectSync : MonoBehaviour
{
    public static UWBObjectSync instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    public void SendUpdateToClients(UWBObject _senderObj)
    {
        UWBObjectData data = new UWBObjectData();

        data.UWBAddress = _senderObj.UWBAddress;
        data.m_OverridePosition = _senderObj.m_OverridePosition;
        data.m_OverridePositionState = _senderObj.m_OverridePositionState;
        data.allowRotation = _senderObj.allowRotation;
        data.m_InvertPositionState = _senderObj.m_InvertPositionState;
        data.m_OffsetPosition = _senderObj.m_OffsetPosition;
        data.position = _senderObj.position;

        ServerSend.UWBObjectData(UWBObjectSerializer.instance.Serialize(data));
    }

    public void UpdateSettingsFromClient(int _sender, UWBObject _target, UWBObjectData _newSettings)
    {
        _target.m_OverridePosition = _newSettings.m_OverridePosition;
        _target.m_OverridePositionState = _newSettings.m_OverridePositionState;
        _target.allowRotation = _newSettings.allowRotation;
        _target.m_InvertPositionState = _newSettings.m_InvertPositionState;
        _target.m_OffsetPosition = _newSettings.m_OffsetPosition;
        _target.position = _newSettings.position;

        _target.UpdateObjectPosition(_target.position);

        // TODO: Send this data to other clients
        ServerSend.UWBObjectDataExcludeUser(_sender, UWBObjectSerializer.instance.Serialize(_newSettings));
    }
}
