using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GGJ.Toaster
{
    /// <summary>
    /// Toaster singleton to manage, show, hide toaster and to populate messages
    /// on the toaster
    /// </summary>
    public class ToasterManager : MonoBehaviour
    {
        public static ToasterManager Instance = null;

        [SerializeField] private GameObject toasterDisplay;

        [Space(5)]
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI toasterText;


        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
        }

        public void PopulateToasterMessage(GameObject _object)
        {

        }

        public void Open()
        {
        }

        public void Close()
        {
            
        }


    }
}
