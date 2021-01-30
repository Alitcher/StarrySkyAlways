using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIGameManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_ScoreHeaderText, m_ScoreNumText;
    [SerializeField] private Button m_MenuButton;

    // Start is called before the first frame update
    void Start()
    {
        m_ScoreHeaderText.text = "Score";
    }

    // Update is called once per frame
    void Update()
    {
        m_ScoreNumText.text = GameControlManager.InGameScoreText.ToString();
    }
}
