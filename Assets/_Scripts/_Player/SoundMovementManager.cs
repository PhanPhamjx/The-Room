using UnityEngine;

public class SoundMovementManager : MonoBehaviour
{
    public AudioSource movementSource;
    public AudioClip walkClip;
    public AudioClip runClip;
    public AudioClip jumpClip;
    public AudioClip landClip;

    // Phát âm thanh di chuyển (đi bộ hay chạy)
    public void PlayMovementSound(bool isRunning)
    {
        if (isRunning)
        {
            movementSource.PlayOneShot(runClip); // Phát âm thanh chạy
        }
        else
        {
            movementSource.PlayOneShot(walkClip); // Phát âm thanh đi bộ
        }
    }

    // Phát âm thanh nhảy
    public void PlayJumpSound()
    {
        movementSource.PlayOneShot(jumpClip);
    }

    // Phát âm thanh chạm đất
    public void PlayLandSound()
    {
        movementSource.PlayOneShot(landClip);
    }
}
