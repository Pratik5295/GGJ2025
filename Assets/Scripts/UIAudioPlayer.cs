using GGJ.Managers;
using UnityEngine;

namespace GGJ.Audio
{
    public class UIAudioPlayer : MonoBehaviour
    {

        [SerializeField]
        private AudioClip clip;


        public void PlaySfx()
        {
            if (clip != null)
            {
                AudioManager.Instance.PlaySFX(clip);
            }
        }
    }
}
