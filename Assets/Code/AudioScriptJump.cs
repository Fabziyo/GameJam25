using UnityEngine;
using UnityEngine.Audio;

public class AudioScriptJump : MonoBehaviour
{

    public AudioClip jumpClip;
    private AudioSource audioSource;
    private bool isGrounded = true;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            PlayJumpSound();
            isGrounded = false;
        }
    }

    void PlayJumpSound()
    {
        if (jumpClip != null)
            audioSource.PlayOneShot(jumpClip, 1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }
}
