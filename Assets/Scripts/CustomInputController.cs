using System;
using System.Collections;
using System.IO;
using System.Net.Sockets;

using TMPro;
using UnityEngine;

public class CustomInputController : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        inputField.onSubmit.AddListener(OnSubmit);
        inputField.onValidateInput += delegate(string text, int charIndex, char addedChar)
        {
            if (addedChar == '\n' || addedChar == '\r')
            {
                return '\0';
            }
            return addedChar;
        };
    }

    void LateUpdate()
    {
        if (!inputField.isFocused)
        {
            inputField.ActivateInputField();
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
            ChatManager.Instance.SendMessage(inputField.text);
            inputField.text = "";
            PrepareForNextInput();
        }
    }
}
