using UnityEngine;

namespace Project_TR.Events
{
    [RequireComponent(typeof(AudioSource))]
    public class TriggerSoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioClip enterClip;   // Âm thanh khi vào
        [SerializeField] private AudioClip oneTimeClip; // Âm thanh phát một lần

        private AudioSource audioSource;
        private bool hasPlayed = false;  // Theo dõi trạng thái phát một lần

        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();

            // Preload các clip nếu có
            if (enterClip != null) audioSource.clip = enterClip;
        }

        /// <summary>
        /// Phát âm thanh khi đi vào trigger zone.
        /// </summary>
        public void PlayOnEnter()
        {
            PlayClip(enterClip);
            Debug.Log("Playing enter sound.");
        }

        /// <summary>
        /// Dừng âm thanh khi rời khỏi trigger zone.
        /// </summary>
        public void StopOnExit()
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                Debug.Log("Stopped exit sound.");
            }
        }

        /// <summary>
        /// Phát âm thanh duy nhất một lần khi đi vào trigger zone.
        /// </summary>
        public void PlayOnce()
        {
            if (!hasPlayed)
            {
                PlayClip(oneTimeClip);
                hasPlayed = true;
                Debug.Log("Playing one-time sound.");
            }
            else
            {
                Debug.Log("Sound has already been played once.");
            }
        }

        /// <summary>
        /// Hàm phát clip tùy chỉnh.
        /// </summary>
        private void PlayClip(AudioClip clip)
        {
            if (clip == null)
            {
                Debug.LogWarning("No AudioClip assigned!");
                return;
            }

            audioSource.PlayOneShot(clip);
        }
    }
}
