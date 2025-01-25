using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    public enum ScreenKey
    {
       DEFAULT = 0,
       GAME = 1,
       PAUSE = 2,
       INFO = 3
    }

    [System.Serializable]
    public class ScreenEntry
    {
        public ScreenKey key;
        public GameObject screen;
    }

    [SerializeField] private List<ScreenEntry> screens = new List<ScreenEntry>();
    [SerializeField] private ScreenKey initialScreenKey = ScreenKey.DEFAULT;

    private GameObject _activeScreen;
    private ScreenKey _activeScreenKey = ScreenKey.DEFAULT;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional, if you want the manager to persist between scenes
    }

    private void Start()
    {
        if (initialScreenKey != ScreenKey.DEFAULT)
        {
            ShowScreen(initialScreenKey);
        }
    }

    /// <summary>
    /// Activates a new screen based on the provided key and deactivates the currently active one.
    /// </summary>
    /// <param name="key">The key of the screen to display.</param>
    public void ShowScreen(ScreenKey key)
    {
        if (key == ScreenKey.DEFAULT)
        {
            Debug.LogWarning("ShowScreen was called with a None key.");
            return;
        }

        if (_activeScreenKey == key)
        {
            Debug.Log("The requested screen is already active.");
            return;
        }

        // Find the screen associated with the key
        GameObject newScreen = GetScreenByKey(key);
        if (newScreen == null)
        {
            Debug.LogError($"No screen found for key: {key}");
            return;
        }

        // Deactivate the current screen
        if (_activeScreen != null)
        {
            _activeScreen.SetActive(false);
        }

        // Activate the new screen
        _activeScreen = newScreen;
        _activeScreen.SetActive(true);
        _activeScreenKey = key;
    }

    /// <summary>
    /// Deactivates the currently active screen.
    /// </summary>
    public void HideActiveScreen()
    {
        if (_activeScreen != null)
        {
            _activeScreen.SetActive(false);
            _activeScreen = null;
            _activeScreenKey = ScreenKey.DEFAULT;
        }
    }

    /// <summary>
    /// Gets the GameObject associated with a specific screen key.
    /// </summary>
    /// <param name="key">The screen key.</param>
    /// <returns>The screen GameObject or null if not found.</returns>
    private GameObject GetScreenByKey(ScreenKey key)
    {
        foreach (var entry in screens)
        {
            if (entry.key == key)
            {
                return entry.screen;
            }
        }
        return null;
    }
}
