using Project_TR.Events;
using UnityEngine;
using System.Collections;

public class TwoDimensionStateController : MonoBehaviour
{
    Animator animator;

    public float acceleration = 5.0f;
    public float deceleration = 7.0f;
    public float maximumWalkVelocity = 0.5f;
    public float maximumRunVelocity = 2.0f;

    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;
    private bool isJumping = false;

    // Các biến để quản lý âm thanh
    public AudioSource audioSource;  // AudioSource gắn với nhân vật
    public AudioClip walkSound;      // Âm thanh đi bộ
    public AudioClip runSound;       // Âm thanh chạy
    public AudioClip jumpSound;      // Âm thanh nhảy
    public AudioClip landSound;      // Âm thanh chạm đất

    void Start()
    {
        animator = GetComponent<Animator>();

        // Kiểm tra nếu chưa gắn AudioSource
        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void Update()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool jumpPressed = Input.GetKeyDown(KeyCode.Space);

        // Kiểm tra và xử lý nhảy
        if (jumpPressed && !isJumping)
        {
            StartCoroutine(JumpRoutine(0.2f));  // Thực hiện jump trong 0.2 giây
        }

        float currentMaxVelocity = runPressed ? maximumRunVelocity : maximumWalkVelocity;
        velocityZ = UpdateVelocitySmooth(forwardPressed, backwardPressed, velocityZ, currentMaxVelocity);
        velocityX = UpdateVelocitySmooth(rightPressed, leftPressed, velocityX, currentMaxVelocity);

        animator.SetFloat("velocityZ", velocityZ);
        animator.SetFloat("velocityX", velocityX);

        // Cập nhật âm thanh
        UpdateSoundEffects(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed);
    }

    private float UpdateVelocitySmooth(bool positivePressed, bool negativePressed, float currentVelocity, float maxVelocity)
    {
        if (positivePressed)
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, maxVelocity, Time.deltaTime * acceleration);
        }
        else if (negativePressed)
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, -maxVelocity, Time.deltaTime * acceleration);
        }
        else
        {
            currentVelocity = Mathf.MoveTowards(currentVelocity, 0, Time.deltaTime * deceleration);
        }

        if (Mathf.Abs(currentVelocity) < 0.01f)
        {
            currentVelocity = 0.0f;
        }

        return Mathf.Clamp(currentVelocity, -maxVelocity, maxVelocity);
    }

    private IEnumerator JumpRoutine(float duration)
    {
        isJumping = true;
        animator.SetTrigger("isJump");  // Kích hoạt trigger Jump
        audioSource.PlayOneShot(jumpSound);  // Phát âm thanh nhảy
        yield return new WaitForSeconds(duration);  // Chờ trong khoảng thời gian duration (0.2 giây)
        animator.ResetTrigger("isJump");  // Reset trigger để ngăn animation lặp lại
        isJumping = false;
        
        // Phát âm thanh chạm đất sau khi hết hoạt ảnh nhảy
        audioSource.PlayOneShot(landSound);
    }

    private void UpdateSoundEffects(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed)
    {
        if (isJumping)
        {
            return;  // Nếu đang nhảy thì không xử lý âm thanh đi bộ hoặc chạy
        }

        // Xử lý âm thanh chạy
        if ((forwardPressed || backwardPressed || leftPressed || rightPressed) && runPressed)
        {
            if (!audioSource.isPlaying || audioSource.clip != runSound)
            {
                audioSource.clip = runSound;
                audioSource.Play();
            }
        }
        else if (forwardPressed || backwardPressed || leftPressed || rightPressed)
        {
            if (!audioSource.isPlaying || audioSource.clip != walkSound)
            {
                audioSource.clip = walkSound;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying && (audioSource.clip == walkSound || audioSource.clip == runSound))
            {
                audioSource.Stop();  // Dừng âm thanh khi không di chuyển
            }
        }
    }
}