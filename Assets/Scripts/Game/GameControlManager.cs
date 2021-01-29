using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameControlManager : MonoBehaviour
{
    public static int InGameScoreCount;
    public static float InGameTimeCount, SpawnSparksTimeCount;
    public static bool IsGameOver;
    [SerializeField] private SparksType m_SparkType;
    [SerializeField] private SparksData m_SparkData;
    [SerializeField] private float m_TimeCountDefault, m_SpawnSparksTimeCount;
    [SerializeField] private int m_SpawnAmount;
    [SerializeField] private int[] m_SparksTypeSceneAmount;

    [SerializeField] private Transform m_SparkSpawnPoint;
    // Start is called before the first frame update

    private void Awake()
    {
        InGameTimeCount = m_TimeCountDefault;
        SpawnSparksTimeCount = m_SpawnSparksTimeCount;
        IsGameOver = false;
    }
    void Start()
    {
        m_SparkData = GameObject.FindObjectOfType<SparksData>();
    }

    // Update is called once per frame
    void Update()
    {
        if (IsGameOver)
            return;


        if (InGameTimeCount > 0f)
            InGameTimeCount -= Time.deltaTime;
        else
        {
            InGameTimeCount = 0f;
            IsGameOver = true;

        }

        if (SpawnSparksTimeCount <= 0)
        {
            SpawnSparks();
            SpawnSparksTimeCount = m_SpawnSparksTimeCount;
        }
        else
        {
            SpawnSparksTimeCount -= Time.deltaTime;
        }

    }

    public int m_SparkInSceneLimit()
    {
        int tmp = 0;
        for (int i = 0; i < m_SparksTypeSceneAmount.Length; i++)
        {
            tmp += m_SparksTypeSceneAmount[i];
        }
        return tmp;
    }

    private void SpawnSparks()
    {
        Debug.Log(m_SparkInSceneLimit());
        if (m_SparkSpawnPoint.transform.childCount < m_SparkInSceneLimit())
        {
            for (int i = 0; i < m_SpawnAmount; i++)
            {
                GameObject sparkGO = Instantiate(m_SparkData.SparksContainer[0], m_SparkSpawnPoint.position, Quaternion.identity);
                sparkGO.name = m_SparkData.SparksContainer[0].name;
                sparkGO.transform.SetParent(m_SparkSpawnPoint);
            }
            print("SpawnSparks()");
        }

    }


    public void MergeObject(GameObject currentDraggingSpark, GameObject collidedSpark)
    {
        currentDraggingSpark.GetComponent<BoxCollider2D>().enabled = false;
        collidedSpark.GetComponent<BoxCollider2D>().enabled = false;

        if (currentDraggingSpark.GetComponent<SparksController>().m_SparkType != SparksType.StarLv12 && 
            collidedSpark.GetComponent<SparksController>().m_SparkType != SparksType.StarLv12)
        {
            Destroy(collidedSpark);
            GameObject newSparks = Instantiate(EmergedSparks(currentDraggingSpark.GetComponent<SparksController>().m_SparkType), currentDraggingSpark.transform.position, Quaternion.identity);
            newSparks.name = m_SparkData.SparksContainer[1].name;
            newSparks.transform.SetParent(m_SparkSpawnPoint);
            Destroy(currentDraggingSpark);
        }

    }

    public void GiveSparkToCharacter(GameObject currentDraggingSpark)
    {
        currentDraggingSpark.GetComponent<BoxCollider2D>().enabled = false;
        Destroy(currentDraggingSpark.gameObject);
    }

    public GameObject EmergedSparks(SparksType type)
    {
        switch (type)
        {
            case SparksType.StarLv1:
                return m_SparkData.SparksContainer[1];
            case SparksType.StarLv2:
                return m_SparkData.SparksContainer[2];
            case SparksType.StarLv3:
                return m_SparkData.SparksContainer[3];
            case SparksType.StarLv4:
                return m_SparkData.SparksContainer[4];
            case SparksType.StarLv5:
                return m_SparkData.SparksContainer[5];
            case SparksType.StarLv6:
                return m_SparkData.SparksContainer[6];
            case SparksType.StarLv7:
                return m_SparkData.SparksContainer[7];
            case SparksType.StarLv8:
                return m_SparkData.SparksContainer[8];
            case SparksType.StarLv9:
                return m_SparkData.SparksContainer[9];
            case SparksType.StarLv10:
                return m_SparkData.SparksContainer[10];
            case SparksType.StarLv11:
                return m_SparkData.SparksContainer[11];
            default:
                return null;
        }
    }

    public void CalculateScore(int sparkValueDragging)
    {
        InGameScoreCount += sparkValueDragging;
    }
}
