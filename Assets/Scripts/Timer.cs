using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{ 
    // Serialized variable of time left for the player to answer the question.
    [SerializeField] float timeToCompleteQuestion = 30f;

    // Serialized variable of time left to show the correct answer when the player finished answering (right or wrong).
    [SerializeField] float timeToShowCorrectAnswer = 10f;

    // Boolean variable to indicate if the player is answering a question (playing the game).
    // The variable is public and set by default to false.
    public bool isAnsweringQuestion = false;

    // Time left on the timer variable
    float timerValue;

    void Update()
    {
        UpdateTimer();
    }

    // Timer Update Method
    void UpdateTimer()
    {
        // At this point, the timer value is empty.
        // But when assigned a value, it will be decreased like a real timer with the Time.deltatime class.
        timerValue -= Time.deltaTime;

        // The timer value will be assigned a value only if the status of the player is active (answering or not).
        if (isAnsweringQuestion)
        {
            // If the timer reaches 0...
            if (timerValue <= 0)
            {
                // And the player cannot answer the question...
                isAnsweringQuestion = false;
                // The timer will last the amount of time needed to show the correct answer (right or wrong).
                timerValue = timeToShowCorrectAnswer;
            }
        }
        // Otherwise...
        else
        {
            // If the timer reaches 0...
            if (timerValue <=0)
            {
                // And the player can answer the question...
                isAnsweringQuestion = true;
                // The timer will last the amount of time needed for the player to answer the question.
                timerValue = timeToCompleteQuestion;
            }
        }

        Debug.Log(timerValue);
    }
}