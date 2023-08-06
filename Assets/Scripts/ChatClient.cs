using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using UnityEngine;

public class ChatClient : MonoBehaviour
{
    private TcpClient client;
    private StreamReader reader;
    private StreamWriter writer;

    public ChatClient(string host, int port)
    {
        client = new TcpClient(host, port);
        var stream = client.GetStream();
        reader = new StreamReader(stream);
        writer = new StreamWriter(stream) { AutoFlush = true };
    }

    public void SendMessage(string message)
    {
        writer.WriteLine(message);
    }

    private void ListenForMessages()
    {
        while (true)
        {
            try
            {
                string message = reader.ReadLine();
                if (message != null)
                {
                    Debug.Log("Received : " + message);
                }
            }
            catch (Exception e)
            {
                Debug.LogException(e);
                break;
            }
        }
    }

    public void Close()
    {
        client.Close();
    }
}
