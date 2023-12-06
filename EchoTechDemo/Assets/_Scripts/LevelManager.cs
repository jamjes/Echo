using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelManager : MonoBehaviour
{
    public GameObject ui;
    public TextMeshProUGUI status;

    private void OnEnable()
    {
        RewindController.OnEnterRewind += SetRewinding;
        RewindController.OnExitRewind += SetRecording;
    }

    private void OnDisable()
    {
        RewindController.OnEnterRewind -= SetRewinding;
        RewindController.OnExitRewind -= SetRecording;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ToggleUI();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            RestartLevel();
        }
    }

    private void ToggleUI()
    {
        if (ui.activeSelf)
        {
            ui.SetActive(false);
        }
        else 
        {
            ui.SetActive(true);
        }
    }

    private void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void SetRecording()
    {
        status.color = Color.red;
        status.text = "Recording";
    }

    private void SetRewinding()
    {
        status.color = Color.cyan;
        status.text = "Rewinding";
    }
}
