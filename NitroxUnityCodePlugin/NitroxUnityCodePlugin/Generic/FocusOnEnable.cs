using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace NitroxUnityCodePlugin.Generic
{
    [RequireComponent(typeof(InputField))]
    public class FocusOnEnable : MonoBehaviour
    {
        private InputField field;

        void Start()
        {
            field = GetComponent<InputField>();

            if (field == null)
            {
                throw new Exception("No input field component attached for the FocusOnEnable component.");
            }
        }

        void OnEnable()
        {
            if (field == null)
            {
                return;
            }
            field.ActivateInputField();
        }
    }
}
