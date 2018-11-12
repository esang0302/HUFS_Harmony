using UnityEngine;
using System.Collections;
using Ardunity;

public class ArdunityApp2 : MonoBehaviour
{
    public ArdunityApp ardunityApp;

    void Awake()
    {
        ardunityApp.OnConnected.AddListener(OnConnected);
        ardunityApp.OnConnectionFailed.AddListener(OnConnectionFailed);
        ardunityApp.OnDisconnected.AddListener(OnDisconnected);
        ardunityApp.OnLostConnection.AddListener(OnLostConnection);
    }

    void OnConnected()
    {
        Debug.Log("On Connected");
    }

    void OnConnectionFailed()
    {
        Debug.Log("On Connection Failed");
    }

    void OnDisconnected()
    {
        Debug.Log("On Disconnected");
    }

    void OnLostConnection()
    {
        Debug.Log("On Lost Connection");
    }
}