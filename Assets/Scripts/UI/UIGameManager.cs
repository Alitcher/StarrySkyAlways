using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ScoreHeaderText, m_ScoreNumText, m_ScoreIncrementText, m_FinalTimeText, m_FinalScoreText;
    [SerializeField] private TextMeshProUGUI m_TimeHeaderText, m_TimeNumText;
    [SerializeField] private Image m_TimerImage;
    [SerializeField] private Button m_MenuButton, m_ReplayButton;
    [SerializeField] private GameObject m_GameOverPanel, m_GameSessionPanel;
    [SerializeField] private Animator m_textAnimator, m_gameOverAnimator;

    int scoreDisplay;
    int scoreStore;
    int scoreDiff;
    int timeText;
    float timeAmount;
    float textAnimation;

    // Start is called before the first frame update
    void Start()
    {
        m_ScoreHeaderText.text = "Score:";
        m_TimeHeaderText.text = "Time:";
        m_GameOverPanel.SetActive(false);
        scoreStore = 0;
        scoreDisplay = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControlManager.IsGameOver)
        {
            ActivePastGamePanel();
            return;
        }

        if (scoreStore < GameControlManager.InGameScoreCount)
        {
            scoreDiff = GameControlManager.InGameScoreCount - scoreStore;
            scoreStore = GameControlManager.InGameScoreCount; 
            m_ScoreIncrementText.text = "+" + scoreDiff.ToString();
            m_textAnimator.Play("ScoreText", -1, 0f);
        }

        m_ScoreNumText.text = GameControlManager.InGameScoreCount.ToString();

        timeText = Mathf.RoundToInt(GameControlManager.InGameTimeCount);
        m_TimeNumText.text = timeText.ToString();

        timeAmount = GameControlManager.InGameTimeCount / 60f;

        if (m_TimerImage.fillAmount < timeAmount)
        {
            m_TimerImage.fillAmount += 0.015f;
        }
        else if (m_TimerImage.fillAmount >= timeAmount)
        {
            m_TimerImage.fillAmount = timeAmount;
        }
    }


    void ActivePastGamePanel()
    {
        //GameObject GameoverPanel = Instantiate(m_GameOverPanel, GetComponent<RectTransform>());
        m_GameSessionPanel.SetActive(false);

        m_FinalScoreText.text = GameControlManager.InGameScoreCount.ToString();
        timeText = Mathf.RoundToInt(GameControlManager.PlayerPlaysTimeCount);
        m_FinalTimeText.text = timeText.ToString();
        m_GameOverPanel.SetActive(true);
        m_gameOverAnimator.Play("GameOver");
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Restart()
    {
        SceneManager.LoadScene("Gameplay");
    }

}
