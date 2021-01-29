using UnityEngine;

public class SparksController : MonoBehaviour
{
    private float distance = 10f;
    private Vector3 MousePosition;
    private GameObject m_CollidedSpark;
    [SerializeField] private GameObject m_SparkIconPreview, m_GiftIcon;
    public SparksType m_SparkType;
    public int SparkValue;

    private SparksData m_SparkData;
    private GameControlManager m_Controller;

    private void Awake()
    {
        m_SparkIconPreview = (m_SparkType != SparksType.StarLv12) ? transform.GetChild(0).gameObject : null;
        m_GiftIcon = (m_SparkType != SparksType.StarLv12) ? transform.GetChild(1).gameObject : transform.GetChild(0).gameObject;
        m_GiftIcon.SetActive(false);
        m_SparkIconPreview?.SetActive(false);
        m_SparkData = GameObject.FindObjectOfType<SparksData>();
        m_Controller = GameObject.FindObjectOfType<GameControlManager>();
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
        MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosTobe = Camera.main.ScreenToWorldPoint(MousePosition);
        transform.position = objPosTobe;
        IsDragging = true;
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
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_SparkIconPreview?.SetActive(false);
        m_GiftIcon.SetActive(false);
        m_CollidedSpark = null;
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

}