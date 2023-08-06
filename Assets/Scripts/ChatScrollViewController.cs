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
        // 프레임 업데이트를 한 번 기다려서 레이아웃 변경을 반영
        yield return null;

        // 스크롤 위치를 맨 아래로 설정
        scrollRect.verticalNormalizedPosition = 0f;
    }
}
