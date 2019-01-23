using System;

namespace NitroxUnityCodePlugin.Events
{
    public class ChatEntryEventArgs : EventArgs
    {
        public ChatEntry Entry { get; protected set; }

        public ChatEntryEventArgs(ChatEntry entry)
        {
            Entry = entry ?? throw new ArgumentNullException(nameof(entry));
        }
    }
}