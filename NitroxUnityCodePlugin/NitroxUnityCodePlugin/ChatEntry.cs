using System;
using UnityEngine;
using UnityEngine.UI;

namespace NitroxUnityCodePlugin
{
    [RequireComponent(typeof(Text))]
    public class ChatEntry : MonoBehaviour
    {
        private Text text;
        public Color UserColor;
        public string UserMessage;
        public string Username;

        private void Awake()
        {
            UserMessage = "";
            Username = "Me";
            UserColor = Color.white;
        }

        /// <summary>
        ///     Updates the internal text property with the values from this model class.
        /// </summary>
        public void UpdateEntry()
        {
            GetComponent<Text>().text =
                $"<color=#{ColorUtility.ToHtmlStringRGBA(UserColor).ToLowerInvariant()}>{Username}</color>: {UserMessage}";
        }
    }
}