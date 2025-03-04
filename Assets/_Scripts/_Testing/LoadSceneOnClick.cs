using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour
{
    // Tên của scene mà bạn muốn load
    public string sceneName;

    // Phương thức này sẽ được gọi khi button được nhấn
    public void LoadTargetScene()
    {
        // Kiểm tra nếu tên scene không rỗng
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Load scene với tên được chỉ định
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Debug.LogError("Scene name is not specified!");
        }
    }
}