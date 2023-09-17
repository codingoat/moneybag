using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Moneybag.UI
{
    [RequireComponent(typeof(FadingUI))]
    public class MessageUI : MonoBehaviour
    {
        private static MessageUI instance;
        public static MessageUI Instance => instance ? instance : instance = FindFirstObjectByType<MessageUI>();

        [SerializeField] private TMP_Text text;
        private FadingUI fadingUI;

        private Queue<Message> messages = new();
        private bool showingMessage;
        
        private void Awake()
        {
            fadingUI = GetComponent<FadingUI>();
            fadingUI.FadeInstant(false);
        }
        
        public void AddMessage(string message, float duration = 3)
        {
            messages.Enqueue(new Message{text = message, duration = duration});
        }

        private void Update()
        {
            if (!showingMessage && messages.Count > 0) 
                StartCoroutine(DisplayMessage(messages.Dequeue()));
        }

        private IEnumerator DisplayMessage(Message msg)
        {
            showingMessage = true;
            fadingUI.visible = true;
            text.text = msg.text;
            yield return new WaitForSeconds(msg.duration);
            fadingUI.visible = false;
            yield return new WaitForSeconds(.5f);
            showingMessage = false;
        }


        private class Message
        {
            public string text;
            public float duration = .3f;
        }
    }
}