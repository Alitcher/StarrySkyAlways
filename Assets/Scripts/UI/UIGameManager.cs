using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ScoreHeaderText, m_ScoreNumText;
    [SerializeField] private TextMeshProUGUI m_TimeHeaderText, m_TimeNumText;
    [SerializeField] private Button m_MenuButton;
    [SerializeField] private GameObject m_GameOverPanel;

    // Start is called before the first frame update
    void Start()
    {
        m_ScoreHeaderText.text = "Score:";
        m_TimeHeaderText.text = "Time:";
        m_GameOverPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameControlManager.IsGameOver)
        {
            ActivePastGamePanel();
            return;
        }
        m_ScoreNumText.text = GameControlManager.InGameScoreCount.ToString();
        m_TimeNumText.text = GameControlManager.InGameTimeCount.ToString();
    }


    void ActivePastGamePanel()
    {
        //GameObject GameoverPanel = Instantiate(m_GameOverPanel, GetComponent<RectTransform>());
        m_GameOverPanel.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

}
