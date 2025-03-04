using UnityEngine;

public class TestAnim : MonoBehaviour
{
    public Animator animator; // Gắn Animator vào nhân vật

    private void Start()
    {
        // animator = GetComponent<Animator>();
        Moveing();
    }
    void Update()
    {

        animator.Play("Player_Idle");
    }
    private void Moveing()
    {
        // Chạy animation Idle
        if (Input.GetKey(KeyCode.T))
        {
            animator.Play("Player_Walk");
        }
        // Chạy animation Run
        else if (Input.GetKey(KeyCode.R))
        {
            animator.Play("Player_Run");
        }
        // Trở về Idle
        else
        {
            animator.Play("Player_Idle");
        }
    }
}
