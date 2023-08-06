using System;
using System.Collections;
using UnityEngine;

public abstract class ChatClient : IDisposable
{
    public abstract void Connect(string host, int port);
    public abstract void SendMessage(string message);
    public abstract IEnumerator ReadData();
    public abstract void Close();
    public abstract void Dispose();
}
