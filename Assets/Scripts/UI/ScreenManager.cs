using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{
    public static ScreenManager Instance { get; private set; }

    public enum ScreenKey
    {
       MENU = 0,     //Will be turned to menu
       GAME = 1,
       PAUSE = 2,
       INFO = 3,
       SETTINGS = 4,
       CREDITS = 5
    }

    [System.Serializable]
    public class ScreenEntry
    {
        public ScreenKey key;
        public GameObject screen;
    }

    [SerializeField] private List<ScreenEntry> screens = new List<ScreenEntry>();
    [SerializeField] private ScreenKey initialScreenKey = ScreenKey.MENU;

    [SerializeField]
    private GameObject _activeScreen;

    [SerializeField]
    private ScreenKey _activeScreenKey = ScreenKey.MENU;

    public ScreenKey ActiveKey => _activeScreenKey;

    public GameObject ActiveScreen => _activeScreen;

    [SerializeField]
    private GameObject instructionText;


    [Header("Info section")]
    [SerializeField]
    private ScreenInfoText infoObject;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
      
    }

    private void Start()
    {
        ShowScreen(initialScreenKey);
    }

    /// <summary>
    /// Activates a new screen based on the provided key and deactivates the currently active one.
    /// </summary>
    /// <param name="key">The key of the screen to display.</param>
    public void ShowScreen(ScreenKey key)
    {
       
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
            _activeScreenKey = ScreenKey.MENU;
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

    public void StartGame()
    {
        ShowScreen(ScreenKey.GAME);
    }

    public void PauseGame()
    {
        ShowScreen(ScreenKey.PAUSE);
    }

    public void ResumeGame()
    {
        ShowScreen(ScreenKey.GAME);
    }

    public void ShowInfo()
    {
        ShowScreen(ScreenKey.INFO);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowSettings()
    {
        ShowScreen(ScreenKey.SETTINGS);
    }

    public void ShowCredits()
    {
        ShowScreen(ScreenKey.CREDITS);
    }

    public void ShowMenu()
    {
        ShowScreen(ScreenKey.MENU);
    }

    public void ShowInstructionText()
    {
        instructionText.SetActive(true);
    }

    public void HideInstructionText()
    {
        instructionText.SetActive(false);
    }


    #region Info Handling

    public void PopulateInfoText(string _text)
    {
        infoObject.PopulateInfoText(_text);

        ShowInstructionText();
    }

    #endregion
}
