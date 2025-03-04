using UnityEngine;
using UnityEngine.UI;

namespace Project_TR.UI
{
    public class _UIManager : MonoBehaviour
    {
        public static _UIManager Instance { get; private set; }

        [SerializeField] private GameObject interactPrompt; // UI "E"

        void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject); // Đảm bảo không bị hủy khi load scene
            }
            else
            {
                Destroy(gameObject);
                return;
            }

            if (interactPrompt == null)
            {
                interactPrompt = GameObject.Find("sdsd");
                if (interactPrompt == null)
                {
                    Debug.LogWarning("InteractPrompt không được tìm thấy! Hãy kiểm tra tên GameObject.");
                }
            }
        }

        public void ShowInteractPrompt()
        {
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(true);
            }
        }

        public void HideInteractPrompt()
        {
            if (interactPrompt != null)
            {
                interactPrompt.SetActive(false);
            }
        }
    }
}
