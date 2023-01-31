using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public ImageTimer HarvestTimer;
    public ImageTimer EatingTimer;
    public Image RaidTimerImg;
    public Image PeasantTimerImg;
    public Image WarriorTimerImg;


    public Button peasantButton;
    public Button warriorButton;

    public Text resourcesText;
    public Text resursesResultText;

    public int peasantCount;
    public int warriorCount;
    public int wheatCount;

    public int wheatPerPeasant;
    public int wheatToWarriors;
    public int peasantCost;
    public int warriorCoast;

    public float peasantCreateTime;
    public float warriorCreateTime;
    public float raidMaxTime;
    public int raidIncrease;
    public int firstRaid;
    public int nextRaid;
    public int countRaid;
    public GameObject GameOverScreen;
    public GameObject WinGame;

    private float peasantTimer = -2;
    private float warriorTimer = -2;
    private float raidTimer;
    public AudioSource eat;
    public AudioSource peasantSound;
   
    void Start()
    {
        UpdateText();
        raidTimer = raidMaxTime;
        firstRaid = 2;
        countRaid = 0;
        eat = GetComponent<AudioSource>();
        peasantSound = GetComponent<AudioSource>();
    }
    void Update()
    {
        raidTimer -= Time.deltaTime;
        RaidTimerImg.fillAmount = raidTimer / raidMaxTime;

        if ((wheatCount - warriorCount * wheatToWarriors <= 0) | (warriorTimer > 0))
        {
            warriorButton.interactable = false;
        }
        else
        {
            warriorButton.interactable = true;
        }
        if ((wheatCount - peasantCount * wheatPerPeasant <= 0) | (peasantTimer > 0))
        {
            peasantButton.interactable = false;
        }
        else
        {
            peasantButton.interactable = true;
        }


        if (raidTimer <= 0)
        {
            raidTimer = raidMaxTime;
            warriorCount -= nextRaid;
            nextRaid += raidIncrease;
            UpdateText();
        }

        if (HarvestTimer.Tick)
        {
            wheatCount += peasantCount * wheatPerPeasant;
        }

        if (EatingTimer.Tick)
        {
            wheatCount -= warriorCount * wheatToWarriors;
            EatSound();
        }
        if (peasantTimer > 0)
        {
            peasantTimer -= Time.deltaTime;
            PeasantTimerImg.fillAmount = peasantTimer / peasantCreateTime;
        }
        else if (peasantTimer > -1)
        {
            PeasantTimerImg.fillAmount = 1;
            peasantButton.interactable = true;
            peasantCount += 1;
            peasantTimer = -2;
        }

        if (warriorTimer > 0)
        {
            warriorTimer -= Time.deltaTime;
            WarriorTimerImg.fillAmount = warriorTimer / warriorCreateTime;
        }
        else if (warriorTimer > -1)
        {
            WarriorTimerImg.fillAmount = 1;
            warriorButton.interactable = true;
            warriorCount += 1;
            warriorTimer = -2;

        }
        UpdateText();
        
            if (warriorCount < 0)
            {
                Time.timeScale = 0;
                GameOverScreen.SetActive(true);
                GameOverText();
            }

            if (peasantCount == 5)
            {
                Time.timeScale = 0;
                WinGame.SetActive(true);
                GameText();
            }
        
    }
    public void CreatePeasant()
    {
        wheatCount -= peasantCost;
        peasantTimer = peasantCreateTime;
        PeasanSpawntSound();
        peasantButton.interactable = false;
    }
    public void CreateWarrior()
    {
        wheatCount -= warriorCoast;
        warriorTimer = warriorCreateTime;
        warriorButton.interactable = false;
    }

    
    private void UpdateText()
    {
        resourcesText.text = peasantCount + "\n" + warriorCount + "\n\n" + wheatCount + "\n\n"+ nextRaid;
    }
    private void GameOverText()
    {
        resursesResultText.text = countRaid + "\n" + (wheatCount - 10) + "\n" + (peasantCount - 3) + "\n" + warriorCount;
    }
    private void GameText()
    {
        resursesResultText.text = countRaid + "\n" + (wheatCount - 10) + "\n" + (peasantCount - 3) + "\n" + warriorCount;
    }
    public void EatSound()
    {
        eat.Play();
    }
    public void PeasanSpawntSound()
    {
        peasantSound.Play();
    }
}
