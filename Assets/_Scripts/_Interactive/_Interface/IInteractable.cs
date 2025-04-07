public interface IInteractable
{
    // Maximum range at which interaction is possible
    float MaxRange { get; }

    // Called when the player starts hovering over this interactable
    void OnStartHover();

    // Called when the player interacts with this interactable
    void OnInteract();

    // Called when the player stops hovering over this interactable 
    void OnEndHover();
}