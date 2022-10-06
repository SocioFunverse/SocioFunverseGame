using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuUI : MonoBehaviour
{
    [SerializeField] GameObject loadingPanel;
    [SerializeField] GameObject tutorialUI;
    [SerializeField] int currentTutorial = 0;
    [SerializeField] GameObject[] tutorialObjects;

    [SerializeField] Animator[] model_animators;

    private void Start()
    {
        SetAnimationSpeed();
    }
    public void Play()
    {
        loadingPanel.SetActive(true);
        SceneManager.LoadScene("OpenWorld");


    }
    public void OpenTutorial()
    {
        for (int i = 0; i < tutorialObjects.Length; i++)
        {
            tutorialObjects[i].SetActive(false);
        }

        tutorialObjects[currentTutorial].SetActive(true);
        tutorialUI.SetActive(true);        
    }

    public void NextTutorial()
    {
        tutorialObjects[currentTutorial].SetActive(false);
        currentTutorial++;
        if (currentTutorial >= tutorialObjects.Length)
        {
            SkipTutorial();
            return;
        }
        tutorialObjects[currentTutorial].SetActive(true);
    }
    public void SkipTutorial()
    {
        PlayerPrefs.SetInt("tutorial", 1);
        tutorialUI.SetActive(false);        
    }

    public void SetAnimationSpeed()
    {
        for (int i = 0; i < model_animators.Length; i++)
        {
            model_animators[i].SetFloat("speed", Random.Range(1, 3));
        }
    }

}
