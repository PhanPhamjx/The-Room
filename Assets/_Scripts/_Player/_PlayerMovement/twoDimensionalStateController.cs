using UnityEngine;
using System.Collections;

public class TwoDimensionStateController : MonoBehaviour
{
    [Header("Animation")]
    [SerializeField] private Animator animator;
    [SerializeField] private float acceleration = 5.0f;
    [SerializeField] private float deceleration = 7.0f;
    [SerializeField] private float maximumWalkVelocity = 0.5f;
    [SerializeField] private float maximumRunVelocity = 2.0f;

    [Header("Audio")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip runSound;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private AudioClip landSound;

    private float velocityZ = 0.0f;
    private float velocityX = 0.0f;
    private bool isJumping = false;

    private void OnEnable()
    {
        PlayerEvents.OnMovement += HandleMovement;
        PlayerEvents.OnJump += HandleJump;
        PlayerEvents.OnLand += HandleLand;
    }

    private void OnDisable()
    {
        PlayerEvents.OnMovement -= HandleMovement;
        PlayerEvents.OnJump -= HandleJump;
        PlayerEvents.OnLand -= HandleLand;
    }

    private void HandleMovement(Vector2 moveInput, bool isSprinting, bool isGrounded)
    {
        float currentMaxVelocity = isSprinting ? maximumRunVelocity : maximumWalkVelocity;

        velocityZ = UpdateVelocitySmooth(moveInput.y > 0, moveInput.y < 0, velocityZ, currentMaxVelocity);
        velocityX = UpdateVelocitySmooth(moveInput.x > 0, moveInput.x < 0, velocityX, currentMaxVelocity);

        animator.SetFloat("velocityZ", velocityZ);
        animator.SetFloat("velocityX", velocityX);

        bool isMoving = moveInput != Vector2.zero;
        UpdateSoundEffects(isMoving, isSprinting);
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

    private void HandleJump()
    {
        if (!isJumping)
        {
            StartCoroutine(JumpRoutine(0.2f));
        }
    }

    private IEnumerator JumpRoutine(float duration)
    {
        isJumping = true;
        animator.SetTrigger("isJump");
        audioSource.PlayOneShot(jumpSound);

        yield return new WaitForSeconds(duration);

        animator.ResetTrigger("isJump");
        isJumping = false;
    }

    private void HandleLand()
    {
        //audioSource.PlayOneShot(landSound); // Uncomment to enable landing sound
    }

    private void UpdateSoundEffects(bool isMoving, bool isSprinting)
    {
        if (isJumping) return;

        if (isMoving && isSprinting)
        {
            if (!audioSource.isPlaying || audioSource.clip != runSound)
            {
                audioSource.clip = runSound;
                audioSource.Play();
            }
        }
        else if (isMoving)
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
                audioSource.Stop();
            }
        }
    }
}