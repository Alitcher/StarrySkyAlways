using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUIManager : MonoBehaviour
{
    [SerializeField] private GameObject m_PlayButton, m_CreditsButton, m_QuitButton;
    [SerializeField] private GameObject m_CreditPanel;


    private void Awake()
    {
        m_CreditPanel.SetActive(false);
    }

    public void OnClickPlayButton()
    {
        SceneManager.LoadScene("Gameplay");
    }


    public void ActiveCreditPanel(bool isActive)
    {
        m_CreditPanel.SetActive(isActive);
    }

    public void OnClickQuitButton()
    {
        Application.Quit();
    }
}
