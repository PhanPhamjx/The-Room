using UnityEngine;
using UnityEngine.UI;
namespace StarterAssets
{
    public class StaminaBar : MonoBehaviour
    {
        [SerializeField] public GameObject StaminaBarGUI;
        [SerializeField] public Image StaminaIcon;
        [SerializeField] FirstPersonController firstPersonController;

        private
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            StaminaBarGUI = GameObject.Find("StaminaBar");
           // StaminaIcon = GameObject.Find("StaminaIcon");
        }
        // Update is called once per frame
        void Update()
        {

        }

        
    }

}
