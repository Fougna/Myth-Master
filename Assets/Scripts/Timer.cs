using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{ 
    // Serialized variable of time left for the player to answer the question.
    [SerializeField] float timeToCompleteQuestion = 30f;

    // Serialized variable of time left to show the correct answer when the player finished answering (right or wrong).
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    // Public boolean variable to load (or not) the next question.
    public bool loadNextQuestion;

    // Boolean variable to indicate if the player is answering a question (playing the game).
    // The variable is public and set by default to false.
    public bool isAnsweringQuestion = false;

    // Public variable to split the timer image depending on the time left.
    public float fillFraction;

    // Time left on the timer variable
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    // Time stop method, used to stop the timer when the question is answered (right or wrong).
    // This method is public so the script can communicate with the timer image.
    public void CancelTimer()
    {
        timerValue = 0;
    }

    // Timer Update Method
    void UpdateTimer()
    {
        // At this point, the timer value is empty.
        // But when assigned a value, it will be decreasing like a real timer with the Time.deltatime class.
        timerValue -= Time.deltaTime;

        // The timer value will be assigned a value only if the status of the player is answering the question.
        if (isAnsweringQuestion)
        {
            // If the timer is superior to zero...
            if (timerValue > 0)
            {
                // The timer image will be emptied with a timer value to complete the question divided by its same value.
                fillFraction = timerValue / timeToCompleteQuestion;
            }
            // Otherwise...
            else
            {
                // The player cannot answer the question...
                isAnsweringQuestion = false;
                // The timer will last the amount of time needed to show the correct answer (right or wrong).
                timerValue = timeToShowCorrectAnswer;
            }
        }
        // Otherwise...
        else
        {
            // If the timer is superior to zero...
            if (timerValue > 0)
            {
                // The timer image will be emptied with a timer value to show the correct answer divided by its same value.
                fillFraction = timerValue / timeToShowCorrectAnswer;
            }
            // Otherwise...
            else
            {
                // The player can answer the question...
                isAnsweringQuestion = true;
                // The timer will last the amount of time needed for the player to answer the question.
                timerValue = timeToCompleteQuestion;
                loadNextQuestion = true;
            }
        }

        Debug.Log(isAnsweringQuestion + ": " + timerValue + " = " + fillFraction);
    }
}