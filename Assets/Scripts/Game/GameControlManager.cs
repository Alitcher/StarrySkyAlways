using UnityEngine;

public class GameControlManager : MonoBehaviour
{
    public static int InGameScoreCount;
    public static float InGameTimeCount, SpawnSparksTimeCount, PlayerPlaysTimeCount;
    public static bool IsGameOver;
    public GameObject SpawnFieldEndlessLevelPrefab;

    [SerializeField] private SparksType m_SparkType;
    [SerializeField] private SparksData m_SparkData;
    [SerializeField] private float m_TimeCountDefault, m_SpawnSparksTimeCount;
    [SerializeField] private int m_SpawnAmount;
    [SerializeField] private int[] m_SparksTypeSceneAmount;
    [SerializeField] private GameObject[] m_SparksInGameCount;

    private GameObject SpawnFieldEndlessLevelScene;
    private SpawnFieldManager m_FieldManager;
    private GameProgress m_Progress;
    // Start is called before the first frame update

    private void Awake()
    {
        InGameTimeCount = m_TimeCountDefault;
        SpawnSparksTimeCount = m_SpawnSparksTimeCount;
        IsGameOver = false;
        SpawnFieldEndlessLevelScene = Instantiate(SpawnFieldEndlessLevelPrefab, Vector3.zero, Quaternion.identity);
        SpawnFieldEndlessLevelScene.name = SpawnFieldEndlessLevelPrefab.name;
        m_FieldManager = SpawnFieldEndlessLevelScene.gameObject.GetComponent<SpawnFieldManager>();
        PlayerPlaysTimeCount = 0f;

    }
    void Start()
    {
        m_SparkData = GameObject.FindObjectOfType<SparksData>();
        m_SparksInGameCount = new GameObject[SpawnFieldEndlessLevelScene.gameObject.GetComponent<SpawnFieldManager>().SpawnPointContainer.Length];
        SpawnSparks();
    }

    private void CheckGameState()
    {
        if (PlayerPlaysTimeCount > 80f)
            m_Progress = GameProgress.Late;
        else if (PlayerPlaysTimeCount <= 80f && PlayerPlaysTimeCount > 30f)
            m_Progress = GameProgress.Middle;
        else
            m_Progress = GameProgress.Early;

    }

    void Update()
    {
        if (IsGameOver)
            return;

        PlayerPlaysTimeCount += Time.deltaTime;
        CheckGameState();

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

    public int m_SparkIndex(GameProgress progress)
    {
        int tmp = 0;
        switch (progress)
        {
            case GameProgress.Early:
                tmp = Random.Range(0, m_SparkData.SparksContainer.Length / 4);
                break ;
            case GameProgress.Middle:
                tmp = Random.Range(0, m_SparkData.SparksContainer.Length / 2);
                break;
            case GameProgress.Late:
                tmp = Random.Range(m_SparkData.SparksContainer.Length / 2, m_SparkData.SparksContainer.Length);
                break;
            default:
                break;
        }
        return tmp;
    }



    private void SpawnSparks()
    {

        for (int i = 0; i< SpawnFieldEndlessLevelScene.gameObject.GetComponent<SpawnFieldManager>().SpawnPointContainer.Length; i++)
        {
            GameObject sparkGO = null;

            if (SpawnFieldEndlessLevelScene.gameObject.GetComponent<SpawnFieldManager>().SpawnPointContainer[i].transform.childCount == 0)
            {
                sparkGO = Instantiate(m_SparkData.SparksContainer[m_SparkIndex(m_Progress)],
                          SpawnFieldEndlessLevelScene.gameObject.GetComponent<SpawnFieldManager>().SpawnPointContainer[i].transform.position,
                          Quaternion.identity);
                m_SparksInGameCount[i] = sparkGO;
                sparkGO.name = m_SparkData.SparksContainer[0].name;
                sparkGO.transform.SetParent(SpawnFieldEndlessLevelScene.gameObject.GetComponent<SpawnFieldManager>().SpawnPointContainer[i].transform);
            }
        }

    }


    public void MergeObject(GameObject currentDraggingSpark, GameObject collidedSpark)
    {
        currentDraggingSpark.GetComponent<BoxCollider2D>().enabled = false;
        collidedSpark.GetComponent<BoxCollider2D>().enabled = false;

        if (currentDraggingSpark.GetComponent<SparksController>().m_SparkType != SparksType.StarLv12 && 
            collidedSpark.GetComponent<SparksController>().m_SparkType != SparksType.StarLv12)
        {
            GameObject newSparks = Instantiate(EmergedSparks(currentDraggingSpark.GetComponent<SparksController>().m_SparkType), currentDraggingSpark.transform.position, Quaternion.identity);
            newSparks.name = m_SparkData.SparksContainer[1].name;
            newSparks.transform.SetParent(collidedSpark.transform.parent);
            Destroy(collidedSpark);
            Destroy(currentDraggingSpark);
        }

    }

    public void GiveSparkToCharacter(GameObject currentDraggingSpark)
    {
        currentDraggingSpark.GetComponent<BoxCollider2D>().enabled = false;
        IncrementTime(currentDraggingSpark);
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

    public void IncrementTime(GameObject currentDraggingSpark)
    {
        float defaultIncrementValue = 3;
        InGameTimeCount += defaultIncrementValue + 2f*((int)currentDraggingSpark.GetComponent<SparksController>().m_SparkType);
    }
}
