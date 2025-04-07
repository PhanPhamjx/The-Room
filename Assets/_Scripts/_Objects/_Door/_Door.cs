using UnityEngine;

public class _Door : MonoBehaviour
{
    // Class cở sỏ của door
    // about01: sẽ có các loại cửa với các cơ chế mở cửa khác nhau, có của xoay
    [SerializeField] public bool IsOpen ;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public virtual void Open()
    {
        IsOpen = true;  
    }
    public virtual void Close()
    {
        IsOpen = false;
    }
    public virtual void Toggle()
    {
        if (IsOpen)
        {
            Close();
        }
        else
        {
            Open();
        }
    }
}
