using System;
using UnityEngine;

public static class PlayerEvents
{
    public static event Action<Vector2, bool, bool> OnMovement;
    public static event Action OnJump;
    public static event Action OnLand;
    public static event Action<bool> OnCrouch;
    public static event Action<bool> OnDialogueToggle;  // Sự kiện cho việc bật/tắt hội thoại

    public static void TriggerMovement(Vector2 moveInput, bool isSprinting, bool isGrounded)
        => OnMovement?.Invoke(moveInput, isSprinting, isGrounded);

    public static void TriggerJump() => OnJump?.Invoke();
    public static void TriggerLand() => OnLand?.Invoke();
    public static void TriggerCrouch(bool isCrouching) => OnCrouch?.Invoke(isCrouching);
    public static void TriggerDialogueToggle(bool inDialogue) => OnDialogueToggle?.Invoke(inDialogue);  // Phương thức kích hoạt sự kiện hội thoại
}