using System;
using System.Linq;
using NitroxUnityCodePlugin.Events;
using NitroxUnityCodePlugin.Generic;
using UnityEngine;
using UnityEngine.UI;

#pragma warning disable 649

namespace NitroxUnityCodePlugin
{
    [RequireComponent(typeof(AutoDisable))]
    public class ChatInput : MonoBehaviour
    {
        [SerializeField] private AutoDisable autoDisable;
        [SerializeField] private InputField input;
        [SerializeField] private Transform log;

        /// <summary>
        ///     Prefab to add to the log on chat input received.
        /// </summary>
        [SerializeField] private GameObject textEntryPrefab;

        /// <summary>
        ///     Called when the user has just entered something in the chat input field.
        /// </summary>
        public event EventHandler<ChatEntryEventArgs> ChatEntryAdded;

        private void Start()
        {
            if (textEntryPrefab == null)
            {
                throw new Exception("Prefab for chat text entry not set.");
            }

            if (log == null)
            {
                throw new Exception("Chat content log not set.");
            }

            if (input == null)
            {
                throw new Exception("Input field component not found on chat entry.");
            }

            if (autoDisable == null)
            {
                throw new Exception("Auto disable component not set.");
            }

            input.onEndEdit.AddListener(InputEntered);
        }

        private void Update()
        {
            if (input.isFocused)
            {
                // Keep chat open if the input field has focus.
                autoDisable.AutoReset();
            }
        }

        public void ShowChat()
        {
            autoDisable.AutoReset();
        }

        public void AddEntry(string username, Color color, string message)
        {
            var textEntry = Instantiate(textEntryPrefab, log, false);
            var chatEntry = textEntry.GetComponent<ChatEntry>();
            chatEntry.Username = username;
            chatEntry.UserColor = color;
            chatEntry.UserMessage = message;
            chatEntry.UpdateEntry();
        }

        public void ClearChat()
        {
            foreach (Transform child in log.transform)
            {
                Destroy(child.gameObject);
            }
        }

        private void InputEntered(string text)
        {
            if (string.IsNullOrEmpty(text) || text.All(char.IsWhiteSpace))
            {
                return;
            }

            var textEntry = Instantiate(textEntryPrefab, log, false);
            var chatEntry = textEntry.GetComponent<ChatEntry>();
            chatEntry.UserMessage = text;
            chatEntry.UpdateEntry();

            OnChatEntryAdded(new ChatEntryEventArgs(chatEntry));

            input.text = string.Empty;
            input.ActivateInputField();
        }

        protected virtual void OnChatEntryAdded(ChatEntryEventArgs e)
        {
            ChatEntryAdded?.Invoke(this, e);
        }
    }
}