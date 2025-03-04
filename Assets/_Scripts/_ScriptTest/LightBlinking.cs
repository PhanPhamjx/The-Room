using UnityEngine;

public class LightBlinking : MonoBehaviour
{
    [SerializeField] private Light flickerLight;
    [SerializeField] private float flickSpeed = 0.1f;
    [SerializeField] private float minIndensity = 0.5f;
    [SerializeField] private float maxIndensity = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        flickerLight = GetComponent<Light>();   
    }
     
    
    public void Flicking()
    {
       
    }
}
