using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayButton, m_HelpButton, m_CreditsButton, m_QuitButton;
    [SerializeField] private GameObject m_HelpPanel, m_CreditPanel;


    private void Awake()
    {
        m_CreditPanel.SetActive(false);
        m_HelpPanel.SetActive(false);
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("Gameplay");
    }


    public void ActiveCreditPanel(bool isActive)
    {
        m_HelpPanel.SetActive(false);

        if (m_CreditPanel.activeSelf == false)
        {
            m_CreditPanel.SetActive(true);
        }
        else if (m_CreditPanel.activeSelf == true)
        {
            m_CreditPanel.SetActive(false);
        }

    }

    public void ActiveHelpPanel(bool isActive)
    {
        m_CreditPanel.SetActive(false);

        if (m_HelpPanel.activeSelf == false)
        {
            m_HelpPanel.SetActive(true);
        }
        else if (m_HelpPanel.activeSelf == true)
        {
            m_HelpPanel.SetActive(false);
        }

    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
