using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;

using UnityEngine;

public class TcpChatClient : ChatClient
{
    private TcpClient tcpClient;
    private StreamWriter writer;
    private StreamReader reader;

    public override void Connect(string host, int port)
    {
        tcpClient = new TcpClient();
        tcpClient.Connect(host, port);

        var stream = tcpClient.GetStream();
        writer = new StreamWriter(stream) { AutoFlush = true };
        reader = new StreamReader(stream);
    }

    public override void SendMessage(string message)
    {
        if (!string.IsNullOrEmpty(message))
        {
            writer.WriteLine(message);
        }
    }

    public override IEnumerator ReadData()
    {
        while (tcpClient.Connected)
        {
            yield return new WaitUntil(() => tcpClient.GetStream().DataAvailable);
            var data = reader.ReadLine();
            Debug.Log("Received from server : " + data);
        }
    }

    public override void Close()
    {
        writer.Close();
        reader.Close();
        tcpClient.Close();
    }

    public override void Dispose()
    {
        Close();
    }
}
