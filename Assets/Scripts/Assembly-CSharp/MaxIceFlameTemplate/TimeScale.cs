using UnityEngine;

namespace DancingLineFanmade.Level
{
    [DisallowMultipleComponent]
    public class TimeScale : MonoBehaviour
    {
        [SerializeField] private AudioSource targetAudioSource;
        [SerializeField] private bool autoEnableOnStart = false; 
        [SerializeField] private KeyCode key = KeyCode.T;
        [SerializeField, Range(0f, 3f)] private float enabledValue = 1.25f;
        [SerializeField, Range(0f, 3f)] private float disabledValue = 1f;

        private bool isFastMode = false;

        private void Awake()
        {
            if (targetAudioSource == null)
            {
                targetAudioSource = GetComponent<AudioSource>();
            }
        }

        private void Start()
        {
            ApplyTimeScale(autoEnableOnStart);
        }

        private void Update()
        {
            if (Input.GetKeyDown(key))
            {
                ApplyTimeScale(!isFastMode);
            }
        }

        public void ApplyTimeScale(bool enable)
        {
            isFastMode = enable;
            float targetScale = isFastMode ? enabledValue : disabledValue;

            if (targetAudioSource != null)
            {
                targetAudioSource.pitch = targetScale;
            }

            Time.timeScale = targetScale;
            Time.fixedDeltaTime = 0.02f * Time.timeScale;
        }

        private void OnDisable()
        {
            Time.timeScale = 1f;
            Time.fixedDeltaTime = 0.02f;
            if (targetAudioSource != null)
            {
                targetAudioSource.pitch = 1f;
            }
        }
    }
}