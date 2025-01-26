using UnityEngine;

namespace GGJ.Managers
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance = null;

        // References to audio sources
        public AudioSource backgroundAudioSource;
        public AudioSource[] foregroundAudioSources = new AudioSource[20];

        // Audio clips for background and foreground sounds
        public AudioClip backgroundMusic;
        public AudioClip[] foregroundClips = new AudioClip[20];

        // Volume control for each audio source
        [Range(0f, 1f)] public float backgroundVolume = 0.5f;
        [Range(0f, 1f)] public float foregroundVolume = 0.8f;

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
            InitializeAudioSources();   
        }

        private void InitializeAudioSources()
        {
            // Set up the background audio source
            if (backgroundAudioSource != null && backgroundMusic != null)
            {
                backgroundAudioSource.clip = backgroundMusic;
                backgroundAudioSource.loop = true;
                backgroundAudioSource.volume = backgroundVolume;
                backgroundAudioSource.Play();
            }

            // Set up the foreground audio sources
            for (int i = 0; i < foregroundAudioSources.Length; i++)
            {
                if (foregroundAudioSources[i] != null && foregroundClips[i] != null)
                {
                    foregroundAudioSources[i].volume = foregroundVolume;
                }
            }

            PlayBackgroundMusic(backgroundMusic);
        }

        // Play the background music with an option to stop it first
        public void PlayBackgroundMusic(AudioClip newClip, bool restart = false)
        {
            if(newClip == backgroundAudioSource.clip)
            {
                //Same clip is already being played
                if (!restart)
                {
                    //Do not restart the clip
                    return;
                }
                else
                {
                    backgroundAudioSource.clip = newClip;
                    backgroundAudioSource.Play();
                }
            }

            if (newClip != null)
            {
                backgroundAudioSource.clip = newClip;
                backgroundAudioSource.Play();
            }
            else if (!backgroundAudioSource.isPlaying || restart)
            {
                backgroundAudioSource.Play();
            }
        }

        // Stop the background music
        public void StopBackgroundMusic()
        {
            backgroundAudioSource.Stop();
        }

        // Play a foreground sound by index (0, 1, or 2)
        public void PlayForegroundSound(int index)
        {
            if (index >= 0 && index < foregroundAudioSources.Length && foregroundAudioSources[index] != null && foregroundClips[index] != null)
            {
                foregroundAudioSources[index].clip = foregroundClips[index];
                foregroundAudioSources[index].Play();
            }
        }

        public void PlaySFX(AudioClip clip)
        {
            var source = GetAvailableAudioSource();

            if(source != null)
            {
                source.clip = clip;
                source.Play();
            }
        }

        // Helper function to return the first available AudioSource
        private AudioSource GetAvailableAudioSource()
        {
            foreach (AudioSource audioSource in foregroundAudioSources)
            {
                // Check if the audio source is not currently playing
                if (!audioSource.isPlaying)
                {
                    return audioSource; // Return the first available (inactive) audio source
                }
            }

            // If no available AudioSource is found, return null
            return null;
        }

        // Stop a specific foreground sound by index
        public void StopForegroundSound(int index)
        {
            if (index >= 0 && index < foregroundAudioSources.Length)
            {
                foregroundAudioSources[index].Stop();
            }
        }

        // Stop all foreground sounds
        public void StopAllForegroundSounds()
        {
            foreach (var source in foregroundAudioSources)
            {
                source.Stop();
            }
        }

        // Set the volume for background music
        public void SetBackgroundVolume(float volume)
        {
            backgroundAudioSource.volume = Mathf.Clamp01(volume);
        }

        // Set the volume for all foreground sounds
        public void SetForegroundVolume(float volume)
        {
            foreach (var source in foregroundAudioSources)
            {
                source.volume = Mathf.Clamp01(volume);
            }
        }

        // Mute or unmute all sounds
        public void ToggleMute(bool mute)
        {
            backgroundAudioSource.mute = mute;
            foreach (var source in foregroundAudioSources)
            {
                source.mute = mute;
            }
        }
    }
}
