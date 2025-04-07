using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TypewriterEffect : MonoBehaviour
{
    public TextMeshProUGUI txt_Dialogue; // UI Text để hiển thị hội thoại
    public float typingSpeed = 0.05f; // Tốc độ hiển thị (giây/ký tự)
    public float speedUpMultiplier = 3f; // Tốc độ tăng lên khi nhấn chuột

    private string currentText = ""; // Văn bản đang được hiển thị
    private Coroutine typingCoroutine; // Coroutine để hiển thị từng ký tự
    private bool isTyping = false; // Kiểm tra xem có đang hiển thị văn bản không

    private void Awake()
    {
        // Tìm đối tượng Text bằng tên và gán vào biến txt_Dialogue
        GameObject textObject = GameObject.Find("txt_Dialogue"); // Thay "DialogueText" bằng tên thực tế của đối tượng Text
        if (textObject != null)
        {
            txt_Dialogue = textObject.GetComponent<TextMeshProUGUI>();
        }
        else
        {
            Debug.LogError("Không tìm thấy đối tượng Text với tên 'DialogueText'!");
        }
    }

    // Bắt đầu hiển thị văn bản với typewriter effect
    public void StartTyping(string text)
    {
        if (txt_Dialogue == null)
        {
            Debug.LogError("Dialogue Text is not assigned!");
            return;
        }

        currentText = text;
        txt_Dialogue.text = ""; // Xóa văn bản hiện tại

        // Dừng coroutine hiện tại nếu có
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        // Bắt đầu coroutine mới
        typingCoroutine = StartCoroutine(TypeText());
    }

    // Coroutine để hiển thị từng ký tự
    private IEnumerator TypeText()
    {
        isTyping = true;

        for (int i = 0; i < currentText.Length; i++)
        {
            // Kiểm tra nếu văn bản bị ẩn hoặc xóa
            if (txt_Dialogue == null || string.IsNullOrEmpty(currentText))
            {
                isTyping = false;
                yield break; // Dừng coroutine
            }

            txt_Dialogue.text += currentText[i]; // Thêm ký tự vào văn bản
            yield return new WaitForSeconds(typingSpeed); // Đợi một khoảng thời gian
        }

        isTyping = false;
        typingCoroutine = null; // Đánh dấu coroutine đã kết thúc
    }

    // Tăng tốc độ hiển thị khi nhấn chuột
    public void SpeedUpTyping()
    {
        if (isTyping && typingCoroutine != null)
        {
            typingSpeed /= speedUpMultiplier; // Giảm thời gian đợi giữa các ký tự
        }
    }

    // Bỏ qua hiệu ứng typewriter và hiển thị toàn bộ văn bản ngay lập tức
    public void SkipTyping()
    {
        if (isTyping && typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // Dừng coroutine
            txt_Dialogue.text = currentText; // Hiển thị toàn bộ văn bản
            isTyping = false;
            typingCoroutine = null;
        }
    }
}