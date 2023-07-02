using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("UI Management")]
    [SerializeField] private GameObject PausePanel;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject SettingsMenu;
    [SerializeField] private GameObject QuitMenu;

    public InputManager inputManager;

    

    void Start()
    {
        inputManager = InputManager.instance;
        
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P))
        {
            if (!PausePanel.activeSelf)
                Pause();
            else Resume();
        }
       
        Object.Destroy(GameObject.Find("Cube"));
    }

    public void Pause()
    {
        inputManager.OnDisable();
        PausePanel.SetActive(true);
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
    }

    public void Resume()
    {
        inputManager.OnEnable();
        PausePanel.SetActive(false);
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Settings()
    {
        PauseMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void BackToPauseMenu()
    {
        PauseMenu.SetActive(true);
        SettingsMenu.SetActive(false);
    }

   
    public void Quit()
    {
        PauseMenu.SetActive(false);
        QuitMenu.SetActive(true);
    }

    public void QuitToMainMenu()
    {
        //load main menu scene
    }

    public void QuitToDesktop()
    {
        Application.Quit();
    }
}
