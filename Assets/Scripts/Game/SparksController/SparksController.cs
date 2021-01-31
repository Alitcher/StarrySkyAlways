using UnityEngine;

public class SparksController : MonoBehaviour
{
    private float distance = 10f;
    private Vector3 MousePosition;
    private GameObject m_CollidedSpark;
    [SerializeField] private GameObject m_SparkIconPreview, m_GiftIcon;
    public SparksType m_SparkType;
    public int SparkValue;

    public ParticleSystem PermanentStar;
    public ParticleSystem[] PermanentStarSystem;
    public ParticleSystem EmergeSystem;
    public ParticleSystem ExpireSystem;

    public Collider2D StarCollider;

    private float DestroyTimer;
    private bool ReadyToDestroy = false;

    private SparksData m_SparkData;
    private GameControlManager m_Controller;

    Vector3 PullPosition;
    float GravityStack;

    private void Awake()
    {
        m_SparkIconPreview = (m_SparkType != SparksType.StarLv12) ? transform.GetChild(0).gameObject : null;
        m_GiftIcon = (m_SparkType != SparksType.StarLv12) ? transform.GetChild(1).gameObject : transform.GetChild(0).gameObject;
        m_GiftIcon.SetActive(false);
        m_SparkIconPreview?.SetActive(false);
        m_SparkData = GameObject.FindObjectOfType<SparksData>();
        m_Controller = GameObject.FindObjectOfType<GameControlManager>();
        DestroyTimer = 5f;
        GravityStack = 0f;
        ReadyToDestroy = false;
        AssignSparksValue();
    }

    private void AssignSparksValue()
    {
        switch (m_SparkType)
        {
            case SparksType.StarLv1:
                SparkValue = 1;
                break;
            case SparksType.StarLv2:
                SparkValue = 2;
                break;
            case SparksType.StarLv3:
                SparkValue = 4;
                break;
            case SparksType.StarLv4:
                SparkValue = 8;
                break;
            case SparksType.StarLv5:
                SparkValue = 16;
                break;
            case SparksType.StarLv6:
                SparkValue = 32;
                break;
            case SparksType.StarLv7:
                SparkValue = 64;
                break;
            case SparksType.StarLv8:
                SparkValue = 128;
                break;
            case SparksType.StarLv9:
                SparkValue = 256;
                break;
            case SparksType.StarLv10:
                SparkValue = 512;
                break;
            case SparksType.StarLv11:
                SparkValue = 1024;
                break;
            case SparksType.StarLv12:
                SparkValue = 2048;
                break;
            default:
                break;
        }
    }

    private bool IsDragging, IsCollidingWiththeMatched;

    private void OnMouseDrag()
    {
        if (ReadyToDestroy == false)
        {
            MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
            Vector3 objPosTobe = Camera.main.ScreenToWorldPoint(MousePosition);
            transform.position = objPosTobe;
            IsDragging = true;
            GravityStack = 0f;
        }
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == this.tag)
        {
            if (IsDragging)
            {
                m_CollidedSpark = collision.gameObject;
                m_SparkIconPreview?.SetActive(true);
                print("spark matches");
                IsCollidingWiththeMatched = true;
            }
        }

        if (collision.tag == "DepressedCharacter" && IsDragging)
        {
            m_GiftIcon.SetActive(true);
            m_CollidedSpark = this.gameObject;
        }

        if (collision.tag == "Blackhole")
        {
            m_Controller.CalculateScore(-SparkValue);
            m_Controller.LostSpark();
            m_SparkIconPreview?.SetActive(false);
            m_GiftIcon.SetActive(false);
            SparkExpire();
        }

        if (collision.tag == "EventHorizon")
        {
            GravityStack += 1f;
            PullPosition = Vector3.MoveTowards(this.transform.position, m_Controller.BlackHoleObject.transform.position, 0.00005f * GravityStack * m_Controller.BlackHolePower);
            this.transform.position = PullPosition;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_SparkIconPreview?.SetActive(false);
        m_GiftIcon.SetActive(false);
        m_CollidedSpark = null;

        if (collision.tag == "EventHorizon")
        {
            GravityStack = 0f;
        }
    }

    private void OnMouseUp()
    {
        IsDragging = false;

        if (m_SparkType != SparksType.StarLv12)
        {
            if (m_SparkIconPreview.activeSelf && null != m_CollidedSpark)
            {
                m_Controller.CalculateScore(SparkValue);
                m_Controller.MergeObject(this.gameObject, m_CollidedSpark);
            }
        }

        if (m_GiftIcon.activeSelf)
        {
            m_Controller.CalculateScore(SparkValue);
            m_Controller.GiveSparkToCharacter(this.gameObject);
        }

    }

    private void Update()
    {
        if (m_Controller.GameOverStats == true)
        {
            if (ReadyToDestroy == false)
            {
                SparkExpire();
            }
        }
        if (ReadyToDestroy == true)
        {
            DestroyTimer -= Time.deltaTime;
        }
        if (DestroyTimer <= 0)
        {
            Destroy(this);
        }
    }

    public void SparkExpire()
    {
        PermanentStar.Clear();
        for(int i = 0; i < PermanentStarSystem.Length; i++)
        {
            if (PermanentStarSystem[i] != null)
            {
                PermanentStarSystem[i].Stop();
            }
        }
        EmergeSystem.Stop();
        ExpireSystem.gameObject.SetActive(true);
        ExpireSystem.Play();
        StarCollider.enabled = false;
        DestroyTimer = 5f;
        ReadyToDestroy = true;
    }

}