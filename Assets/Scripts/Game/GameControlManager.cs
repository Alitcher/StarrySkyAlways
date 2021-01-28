using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlManager : MonoBehaviour
{
    public static int InGameScoreText;
    [SerializeField] private SparksType m_SparkType;
    [SerializeField] private SparksData m_SparkData;
    // Start is called before the first frame update
    void Start()
    {
        m_SparkData = GameObject.FindObjectOfType<SparksData>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MergeObject(GameObject currentDraggingSpark, GameObject collidedSpark)
    {
        currentDraggingSpark.GetComponent<BoxCollider2D>().enabled = false;
        collidedSpark.GetComponent<BoxCollider2D>().enabled = false;

        if (currentDraggingSpark.GetComponent<SparksController>().m_SparkType != SparksType.Star && 
            collidedSpark.GetComponent<SparksController>().m_SparkType != SparksType.Star)
        {
            Destroy(collidedSpark);
            GameObject newSparks = Instantiate(EmergedSparks(currentDraggingSpark.GetComponent<SparksController>().m_SparkType), currentDraggingSpark.transform.position, Quaternion.identity);
            newSparks.name = m_SparkData.m_SparksContainer[1].name;
            Destroy(currentDraggingSpark);
        }

    }

    public GameObject EmergedSparks(SparksType type)
    {
        switch (type)
        {
            case SparksType.Rock:
                return m_SparkData.m_SparksContainer[1];
            case SparksType.MoonGold:
                return m_SparkData.m_SparksContainer[2];
            case SparksType.MoonWhite:
                return m_SparkData.m_SparksContainer[3];
            case SparksType.HeartRed:
                return m_SparkData.m_SparksContainer[4];
            case SparksType.HeartBlack:
                return m_SparkData.m_SparksContainer[5];
            case SparksType.AppleRed:
                return m_SparkData.m_SparksContainer[6];
            case SparksType.AppleBlack:
                return m_SparkData.m_SparksContainer[7];
            case SparksType.CherryRed:
                return m_SparkData.m_SparksContainer[8];
            case SparksType.CherryBlack:
                return m_SparkData.m_SparksContainer[9];
            case SparksType.GemRed:
                return m_SparkData.m_SparksContainer[10];
            case SparksType.GemGreen:
                return m_SparkData.m_SparksContainer[11];
            default:
                return null;
        }
    }

    public void CalculateScore(int sparkValueDragging, int collidedSparkValue)
    {
        InGameScoreText += sparkValueDragging * collidedSparkValue;
    }
}
