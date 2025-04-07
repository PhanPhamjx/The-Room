using UnityEngine;

public class StarterAssetsInputs : MonoBehaviour
{
    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump; // Tracks if Space is held (for compatibility)
    public bool jumpPressed; // True only on the frame Space is pressed
    public bool sprint;

    [Header("Movement Settings")]
    public bool analogMovement;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;

    private void Update()
    {
        // Move input
        move = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) move.y = 1f;
        if (Input.GetKey(KeyCode.S)) move.y = -1f;
        if (Input.GetKey(KeyCode.D)) move.x = 1f;
        if (Input.GetKey(KeyCode.A)) move.x = -1f;

        // Sprint input
        sprint = Input.GetKey(KeyCode.LeftShift);

        // Jump input
        jump = Input.GetKey(KeyCode.Space); // Held state for compatibility
        jumpPressed = Input.GetKeyDown(KeyCode.Space); // Triggered once on press

        // Look input
        if (cursorInputForLook)
        {
            look = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
        }
    }

    private void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    private void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
}