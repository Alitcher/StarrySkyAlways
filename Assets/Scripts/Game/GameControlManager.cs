using UnityEngine;

public class GameControlManager : MonoBehaviour
{
    public static int InGameScoreCount;
    public static float InGameTimeCount, SpawnSparksTimeCount, RespawnTimeCount, PlayerPlaysTimeCount, BlackHoleDuration, BlackHoleWarningDuration, BlackHoleDelay;
    public static bool IsGameOver;
    public GameObject SpawnFieldEndlessLevelPrefab;
    public Transform DestroyGroup;
    public Transform[] BoundObject;
    public AudioSource[] Sound;

    public GameObject BlackHoleObject;
    public GameObject blackHoleWarningObject;
    public ParticleSystem BlackHoleExpireVFX;
    public float BlackHolePower;

    bool fullSpark = false;
    public bool GameOverStats;
    int boundIndex;
    int starIndex;
    int Loop;
    Vector2 randomPoint;
    float randomNumber;
    Collider2D[] hitColliderContainer;

    bool BlackHoleImminent;
    bool BlackHoleActive;

    [SerializeField] private SparksType m_SparkType;
    [SerializeField] private SparksData m_SparkData;
    [SerializeField] private float m_TimeCountDefault, m_SpawnSparksTimeCount, m_RespawnDelay, m_blackHoleDurationMin, m_BlackHoleDurationMax, m_BlackHoleWarningDuration, m_BlackHoleDelayMin, m_BlackHoleDelayMax;
    [SerializeField] private int m_SpawnAmount;
    [SerializeField] private int[] m_SparksTypeSceneAmount;

    private GameObject SpawnFieldEndlessLevelScene;
    private SpawnFieldManager m_FieldManager;
    private GameProgress m_Progress;
    private int starCount;
    // Start is called before the first frame update

    private void Awake()
    {
        InGameTimeCount = m_TimeCountDefault;
        SpawnSparksTimeCount = m_SpawnSparksTimeCount;
        IsGameOver = false;
        GameOverStats = false;
        fullSpark = false;
        SpawnFieldEndlessLevelScene = Instantiate(SpawnFieldEndlessLevelPrefab, Vector3.zero, Quaternion.identity);
        SpawnFieldEndlessLevelScene.name = SpawnFieldEndlessLevelPrefab.name;
        m_FieldManager = SpawnFieldEndlessLevelScene.gameObject.GetComponent<SpawnFieldManager>();
        PlayerPlaysTimeCount = 0f;
        BlackHoleDelay = Random.Range(m_BlackHoleDelayMin, m_BlackHoleDelayMax);
        BlackHoleImminent = false;
        BlackHoleActive = false;
        BlackHolePower = 1f;

    }
    void Start()
    {
        m_SparkData = GameObject.FindObjectOfType<SparksData>();
        m_RespawnDelay = RespawnTimeCount;
        SpawnSparks();
    }

    private void CheckGameState()
    {
        if (PlayerPlaysTimeCount > 120f)
            m_Progress = GameProgress.Late;
        else if (PlayerPlaysTimeCount <= 120f && PlayerPlaysTimeCount > 60f)
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
            GameOverStats = true;

        }

        if (RespawnTimeCount <= 0 && BlackHoleActive == false)
        {
            if (fullSpark == false && BlackHoleActive == false)
            {
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
        }
        else
        {
            RespawnTimeCount -= Time.deltaTime;     
        }

        if (BlackHoleDelay <= 0)
        {
            if (BlackHoleImminent == false)
            {
                blackHoleWarningObject.SetActive(true);
                BlackHoleWarningDuration = m_BlackHoleWarningDuration;
                BlackHoleImminent = true;
            }
        }
        else
        {
            BlackHoleDelay -= Time.deltaTime;
        }

        if (BlackHoleImminent == true)
        {
            if (BlackHoleWarningDuration <= 0)
            {
                if (BlackHoleActive == false)
                {
                    BlackHoleObject.SetActive(true);
                    blackHoleWarningObject.SetActive(false);
                    BlackHoleDuration = Random.Range(m_blackHoleDurationMin, m_BlackHoleDurationMax);
                    BlackHoleActive = true;
                }
            }
            else
            {
                BlackHoleWarningDuration -= Time.deltaTime;
            }

        }
        if (BlackHoleActive == true)
        {
            if (BlackHoleDuration > 0)
                BlackHoleDuration -= Time.deltaTime;

            if (BlackHoleDuration <= 0)
            {
                BlackHoleObject.SetActive(false);
                BlackHoleDelay = Random.Range(m_BlackHoleDelayMin, m_BlackHoleDelayMax);
                BlackHoleImminent = false;
                BlackHoleActive = false;
                BlackHolePower += 0.5f;
                BlackHoleExpireVFX.transform.localPosition = BlackHoleObject.transform.localPosition;
                BlackHoleExpireVFX.Play();
                Vector3 rndPoint2D = RandomPointInBounds(BoundObject[boundIndex].localPosition, BoundObject[boundIndex].localScale.x, BoundObject[boundIndex].localScale.y, 0);

                BlackHoleObject.transform.localPosition = rndPoint2D;
                blackHoleWarningObject.transform.localPosition = rndPoint2D;
            }

        }

    }

    public int m_SparkIndex(GameProgress progress)
    {
        int tmp = Random.Range(0, 100);
        int index = 0;
        switch (progress)
        {
            case GameProgress.Early:
                if (tmp <= 45)
                    index = 0;
                else if (tmp > 45 && tmp <= 75)
                    index = 1;
                else if (tmp > 75 && tmp <= 90)
                    index = 2;
                else if (tmp > 90 && tmp <= 100)
                    index = 3;
                break;
            case GameProgress.Middle:
                if (tmp <= 30)
                    index = 0;
                else if (tmp > 30 && tmp <= 55)
                    index = 1;
                else if (tmp > 55 && tmp <= 70)
                    index = 2;
                else if (tmp > 70 && tmp <= 80)
                    index = 3;
                else if (tmp > 80 && tmp <= 90)
                    index = 4;
                else if (tmp > 90 && tmp <= 97)
                    index = 5;
                else if (tmp > 97 && tmp <= 100)
                    index = 6;
                break;
            case GameProgress.Late:
                if (tmp <= 26)
                    index = 0;
                else if (tmp > 26 && tmp <= 42)
                    index = 1;
                else if (tmp > 42 && tmp <= 58)
                    index = 2;
                else if (tmp > 58 && tmp <= 74)
                    index = 3;
                else if (tmp > 74 && tmp <= 82)
                    index = 4;
                else if (tmp > 82 && tmp <= 90)
                    index = 5;
                else if (tmp > 90 && tmp <= 94)
                    index = 6;
                else if (tmp > 94 && tmp <= 98)
                    index = 7;
                else if (tmp > 98 && tmp <= 100)
                    index = 8;
                break;
            default:
                break;
        }
        return index;
    }



    private void SpawnSparks()
    {
        GameObject sparkGO = null;

        randomNumber = Random.Range(0f, 100f);

        starIndex = m_SparkIndex(m_Progress);

        if (randomNumber <= 5f)
            boundIndex = 0;
        else if (randomNumber > 5f && randomNumber <= 10f)
            boundIndex = 1;
        else if (randomNumber > 10f && randomNumber <= 52f)
            boundIndex = 2;
        else if (randomNumber > 52f && randomNumber <= 100f)
            boundIndex = 3;

        Vector3 rndPoint2D = RandomPointInBounds(BoundObject[boundIndex].localPosition, BoundObject[boundIndex].localScale.x, BoundObject[boundIndex].localScale.y, starIndex);

        sparkGO = Instantiate(m_SparkData.SparksContainer[starIndex]);
        sparkGO.transform.position = rndPoint2D;
        sparkGO.transform.SetParent(SpawnFieldEndlessLevelScene.transform, true);

        starCount += 1;

        if (starCount >= m_SpawnAmount + (Loop * m_SpawnAmount / 10))
        {
            fullSpark = true;
        }
        else if (starCount >= 50)
        {
            fullSpark = true;
        }

    }

    private Vector3 RandomPointInBounds(Vector3 center, float boxX, float boxY, int StarPower)
    {
        Vector3 randomPos = new Vector3(
            Random.Range(-boxX / 2, boxX / 2),
            Random.Range(-boxY / 2, boxY / 2),
            1f
        );
        randomPos = randomPos + center;

        randomPoint = new Vector2(randomPos.x, randomPos.y);   
        int hitColliders = Physics2D.OverlapCircleNonAlloc(randomPoint, 0.5f + (1f * StarPower), hitColliderContainer);

        if (hitColliders == 0)
        {
            return randomPos;
        }
        else
        {
            return RandomPointInBounds(center, boxX, boxY, StarPower);
        }
    }


    public void MergeObject(GameObject currentDraggingSpark, GameObject collidedSpark)
    {
        Sound[1].Play();
        currentDraggingSpark.GetComponent<BoxCollider2D>().enabled = false;
        collidedSpark.GetComponent<BoxCollider2D>().enabled = false;

        if (currentDraggingSpark.GetComponent<SparksController>().m_SparkType != SparksType.StarLv12 && 
            collidedSpark.GetComponent<SparksController>().m_SparkType != SparksType.StarLv12)
        {
            GameObject newSparks = Instantiate(EmergedSparks(currentDraggingSpark.GetComponent<SparksController>().m_SparkType), currentDraggingSpark.transform.position, Quaternion.identity);
            newSparks.name = m_SparkData.SparksContainer[1].name;
            newSparks.transform.SetParent(collidedSpark.transform.parent);

            collidedSpark.GetComponent<SparksController>().SparkExpire();
            collidedSpark.transform.SetParent(DestroyGroup, true);
            currentDraggingSpark.GetComponent<SparksController>().SparkExpire();
            currentDraggingSpark.transform.SetParent(DestroyGroup, true);

            LostSpark();
        }

    }

    public void GiveSparkToCharacter(GameObject currentDraggingSpark)
    {
        Sound[2].Play();
        currentDraggingSpark.GetComponent<BoxCollider2D>().enabled = false;
        IncrementTime(currentDraggingSpark);
        currentDraggingSpark.GetComponent<SparksController>().SparkExpire();
        currentDraggingSpark.transform.SetParent(DestroyGroup, true);

        LostSpark();
    }

    public void LostSpark()
    {
        starCount -= 1;
        if (starCount <= (m_SpawnAmount / 5) + (Loop / 2))
        {
            fullSpark = false;
            RespawnTimeCount = m_RespawnDelay;
            Loop += 1;
        }
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
        if (InGameScoreCount < 0) InGameScoreCount = 0;
    }

    public void IncrementTime(GameObject currentDraggingSpark)
    {
        float defaultIncrementValue = 3;
        InGameTimeCount += defaultIncrementValue + 2f*((int)currentDraggingSpark.GetComponent<SparksController>().m_SparkType);
        if (InGameTimeCount > 60f) InGameTimeCount = 60f;
    }
}
