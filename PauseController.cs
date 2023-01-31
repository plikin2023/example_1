using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    private bool paused;
    public GameObject PausedGameScreen;
    private AudioSource audio;
    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }
    public void PausedGame()
    {
        if (paused)
        {
            Time.timeScale = 1;
            PausedGameScreen.SetActive(false);
        }
        else
        {
            Time.timeScale = 0;
            PausedGameScreen.SetActive(true);
        }
        paused = !paused;
    }
    public void PauseSound()
    {
        if (audio.isPlaying)
            audio.Pause();
        else
            audio.Play();
    }
}
