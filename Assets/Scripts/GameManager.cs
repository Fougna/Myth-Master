using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // This Game Manager will help toggle between differents screens or scenes.
    // First, we need a quiz variable to access the Quiz script.
    Quiz quiz;
    // Second, we need an end screen variable to access the End Screen script.
    EndScreen endScreen;

    // In order to make all canvases are ready to use before the game actually starts,
    // we store the reference calls in the Awake method.
    void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        endScreen = FindObjectOfType<EndScreen>();        
    }

    void Start()
    {
        // At the beginning of the game, we set the quiz canvas to active so it pops up first...
        quiz.gameObject.SetActive(true);
        // And in the meantime, we disable the end screen canvas. 
        endScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        // If the quiz game is finshed...
        if (quiz.isComplete)
        {
            // The quiz canvas is disabled...
            quiz.gameObject.SetActive(false);
            // The end screen canvas appears...
            endScreen.gameObject.SetActive(true);
            // And the final score display method is shown.
            endScreen.ShowFinalScore();
        }
    }

    // Since the end screen has a replay button, we create a replay method
    // that will be linked later on the replay button game object.
    public void OnReplayLevel()
    {
        // Thanks to the 'using UnityEngine.SceneManagement' class,
        // we can use Scene Manager to load a scene.
        // The selected scene in parameters is another Scene Manager method
        // which load the active scene and reset it with the buildIndex method.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}