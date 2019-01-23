using System.Collections;
using UnityEngine;

namespace NitroxUnityCodePlugin.Generic
{
    public class AutoDisable : MonoBehaviour
    {
        [Tooltip("Delay in seconds to wait until disabling the game objects.")]
        public float Delay = 5;

        [Tooltip("Objects to auto disable.")] public GameObject[] objects;

        public KeyCode ResetKey = KeyCode.None;
        public bool StartDisabled;

        public void AutoReset()
        {
            if (objects == null)
            {
                return;
            }

            StopAllCoroutines();
            StartCoroutine(AutoClose());
        }

        private void Start()
        {
            if (StartDisabled && objects != null)
            {
                foreach (var obj in objects)
                {
                    obj.SetActive(false);
                }
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(ResetKey))
            {
                AutoReset();
            }
        }

        private IEnumerator AutoClose()
        {
            foreach (var obj in objects)
            {
                obj.SetActive(true);
            }

            yield return new WaitForSecondsRealtime(Delay);
            foreach (var obj in objects)
            {
                obj.SetActive(false);
            }
        }
    }
}