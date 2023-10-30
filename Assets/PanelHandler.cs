using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    public GameObject CurrentPanel;  // This will be your main menu panel
    public GameObject MenuPanel;     // This will be your options panel

    // This method toggles between the main menu and options
    public void TogglePanels()
    {
        if (CurrentPanel.activeSelf)
        {
            CurrentPanel.SetActive(false);
            MenuPanel.SetActive(true);
        }
        else
        {
            MenuPanel.SetActive(false);
            CurrentPanel.SetActive(true);
        }
    }

    // This method will show the main menu and hide the options
    public void ShowMainMenu()
    {
        CurrentPanel.SetActive(true);
        MenuPanel.SetActive(false);
    }
}
