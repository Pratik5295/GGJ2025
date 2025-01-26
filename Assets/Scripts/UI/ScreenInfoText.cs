using TMPro;
using UnityEngine;

public class ScreenInfoText : MonoBehaviour
{
    public TextMeshProUGUI infoText;

    public void PopulateInfoText(string _text)
    {
        infoText.text = _text;
    }
}
