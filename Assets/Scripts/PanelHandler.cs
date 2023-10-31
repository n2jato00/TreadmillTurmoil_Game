using UnityEngine;

public class PanelHandler : MonoBehaviour
{
    public GameObject CurrentPanel;  // This will be your main menu panel
    public GameObject SecondPanel;     // This will be your options panel

    // This method toggles between the main menu and options
    public void TogglePanels()
    {
        if (CurrentPanel.activeSelf)
        {
            CurrentPanel.SetActive(false);
            SecondPanel.SetActive(true);
        }
        else
        {
            SecondPanel.SetActive(false);
            CurrentPanel.SetActive(true);
        }
    }

    // This method will show the main menu and hide the options
    public void ShowMainMenu()
    {
        CurrentPanel.SetActive(true);
        SecondPanel.SetActive(false);
    }
}
