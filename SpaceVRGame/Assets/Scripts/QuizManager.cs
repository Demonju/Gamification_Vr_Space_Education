using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

/*
 * manager to handle quiz logic
 */
public class QuizManager : MonoBehaviour {
    public List<QuestionsAndAnswers> QnA; 
    public GameObject[] options;  // array of answer option gameObjects
    public int currentQuestion;  // index of the current question

    public Text questionText;

    public AudioSource audioSource;
    public AudioClip winSound;

    public GameObject quizPanel;  // panel for displaying the quiz
    public GameObject winPanel;  // panel for displaying win screen

    public int score = 0;
    public Text scoreText;  // Assign this in the inspector to a TextMeshProUGUI or Text component


    private void Start()
    {
        score = 0;
        UpdateScoreUI();
        GenerateQuestion();
    }


    private void Awake()
    {
        QnA = new List<QuestionsAndAnswers> {
        new QuestionsAndAnswers {
            question = "Which planet do we live on?",
            answers = new string[] { "Mars", "Earth", "Jupiter", "Venus" },
            correctAnswer = 2
        },
        new QuestionsAndAnswers {
            question = "Which planet is known as the Red Planet?",
            answers = new string[] { "Mars", "Mercury", "Earth", "Saturn" },
            correctAnswer = 1
        },
        new QuestionsAndAnswers {
            question = "Which planet is the largest in our Solar System?",
            answers = new string[] { "Saturn", "Jupiter", "Earth", "Neptune" },
            correctAnswer = 2
        },
        new QuestionsAndAnswers {
            question = "Which planet is closest to the Sun?",
            answers = new string[] { "Earth", "Venus", "Mercury", "Mars" },
            correctAnswer = 3
        },
        new QuestionsAndAnswers {
            question = "Which planet is known as Earth’s twin due to its similar size?",
            answers = new string[] { "Venus", "Jupiter", "Mars", "Uranus" },
            correctAnswer = 1
        },
        new QuestionsAndAnswers {
            question = "Which planet has the most prominent ring system?",
            answers = new string[] { "Neptune", "Saturn", "Jupiter", "Mars" },
            correctAnswer = 2
        },
        new QuestionsAndAnswers {
            question = "Which is the farthest planet from the Sun?",
            answers = new string[] { "Neptune", "Uranus", "Saturn", "Jupiter" },
            correctAnswer = 1
        },
        new QuestionsAndAnswers {
            question = "Which planet rotates on its side and has a bluish color?",
            answers = new string[] { "Neptune", "Saturn", "Uranus", "Mercury" },
            correctAnswer = 3
        }
    };
    }


    // method to handle correct answer selection
    public void Correct()
    {
        score += 1; // Increase score by 1
        UpdateScoreUI(); // Update the UI
        QnA.RemoveAt(currentQuestion);
        GenerateQuestion();
    }

    void UpdateScoreUI()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score.ToString();
    }



    // method to set the answers on the answer option gameObjects
    void SetAnswers() {
        int correctAnswerIndex = QnA[currentQuestion].correctAnswer - 1;  // subtract 1 to convert to zero-based indexing

        for (int i = 0; i < options.Length; i++) {
            options[i].GetComponent<Answer>().isCorrect = (i == correctAnswerIndex);  // set isCorrect based on index comparison
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].answers[i];
        }
    }

    // method to generate a new question
    void GenerateQuestion() {
        if (QnA.Count == 0) {
            quizPanel.SetActive(false);  // hide the quiz panel if there are no more questions
            winPanel.SetActive(true);  // show the win panel if there are no more questions
            // all questions have been answered, play the win audio
            PlayWinSound();
        } else {
            currentQuestion = Random.Range(0, QnA.Count);  // randomly select a new question
            questionText.text = QnA[currentQuestion].question;  // set the question text
            SetAnswers();  // set the answer options
        }
    }

    // method to play the win sound
    private void PlayWinSound() {
        if (audioSource != null && winSound != null) {
            audioSource.clip = winSound;  // set the audio clip
            audioSource.Play();  // play the audio
        }
    }
}
