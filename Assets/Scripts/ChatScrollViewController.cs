using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChatScrollViewController : MonoBehaviour
{
    public ScrollRect scrollRect;

    public GameObject messagePrefab;
    public Transform content;

    public void AddMessage(string msg)
    {
        GameObject newMessage = Instantiate(messagePrefab);
        newMessage.transform.SetParent(content, false);

        newMessage.GetComponent<TextMeshProUGUI>().text = msg;

        StartCoroutine(ScrollToBottom());
    }

    private IEnumerator ScrollToBottom()
    {
        yield return new WaitForSeconds(0.1f);
        Debug.Log("called");
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
