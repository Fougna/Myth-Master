using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    // In order to calculate the player's score, we need two integer variables.
    // One for the correct answers...
    int correctAnswers = 0;
    // And another for the answered questions right or wrong.
    int questionsSeen = 0;

    // Then, in order to protect the score value, we need to pass a Getter method for the correct answers...
    public int GetCorrectAnswers()
    {
        return correctAnswers;
    }

    // And a Setter method that will increment by one every correct answer given by the player.
    public void IncrementCorrectAnswers()
    {
        correctAnswers++;
    }

    // Again, we create a Getter method to get the questions seen...
    public int GetQuestionsSeen()
    {
        return questionsSeen;
    }

    // And a Setter method that will increment by one every question seen.
    public void IncrementQuestionsSeen()
    {
        questionsSeen++;
    }

    // Finally, we need a method to calculate the score in a percentage formula.
    public int CalculateScore()
    {
        // The value is returned as such...
        // We divide the number of correct answers by the total number of questions answered, and multiply the total by 100.
        // But we have declared integer values to calculate the score,
        // and we're going to have decimal results that can only be calculated with float values.
        // Therefore the following code is needed...
        // 1 - Mathf is a mathematical class containing multiple math functions.
        // 2 - RoundToInt is a MathF method that will convert integer value to float value.
        // 3 - With the (float), we pass the number of questions seen to a decimal value.
        return Mathf.RoundToInt(correctAnswers / (float)questionsSeen * 100);
    }
}
