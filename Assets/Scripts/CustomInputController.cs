using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;

using TMPro;
using UnityEngine;

public class CustomInputController : MonoBehaviour
{
    public TMP_InputField inputField;

    private ChatClient chatClient;

    // private TcpClient client;
    // private StreamWriter writer;
    // private StreamReader reader;

    private string host = "localhost";
    private int port = 4000;

    void Start()
    {
        inputField.onSubmit.AddListener(OnSubmit);

        inputField.onValidateInput += delegate(string text, int charIndex, char addedChar)
        {
            if (addedChar == '\n' || addedChar == '\r')
            {
                OnSubmit(text);
                return '\0';
            }
            return addedChar;
        };

        chatClient = new TcpChatClient();
        chatClient.Connect("localhost", 4000);

        // // client = new TcpClient();
        // // yield return client.ConnectAsync(host, port);

        // var stream = client.GetStream();
        // writer = new StreamWriter(stream) { AutoFlush = true };
        // reader = new StreamReader(stream);

        StartCoroutine(chatClient.ReadData());
    }

    // IEnumerator ReadData()
    // {
    //     while (chatClient.Connected)
    //     {
    //         yield return new WaitUntil(() => chatClient.GetStream().DataAvailable);

    //         var data = reader.ReadLine();
    //         Debug.Log("Received from server: " + data);
    //     }
    // }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                PrepareForNextInput();
            }
        }
    }

    void PrepareForNextInput()
    {
        Debug.Log("prepare???");
        inputField.ActivateInputField();
        inputField.MoveTextEnd(false);
    }

    void OnSubmit(string text)
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            return;

        if (!string.IsNullOrEmpty(text))
        {
            Debug.Log("Submitted text: " + text + ", size: " + text.Length);
            string sendMsg = text + "\n";
            // writer.WriteLine(sendMsg);
            chatClient.SendMessage(sendMsg);
            inputField.text = "";
            PrepareForNextInput();
        }
    }

    void OnDestroy()
    {
        chatClient.Close();
    }
}
