using UnityEngine;

public class SparksController : MonoBehaviour
{
    private float distance = 10f;
    private Vector3 MousePosition;
    private GameObject m_CollidedSpark;
    [SerializeField] private GameObject m_SparkIconPreview;
    public SparksType m_SparkType;
    public int SparkValue;

    private SparksData m_SparkData;
    private GameControlManager m_Controller;

    private void Awake()
    {
        m_SparkIconPreview = (m_SparkType != SparksType.Star) ? transform.GetChild(0).gameObject : null;
        m_SparkIconPreview?.SetActive(false);
        m_SparkData = GameObject.FindObjectOfType<SparksData>();
        m_Controller = GameObject.FindObjectOfType<GameControlManager>();
        AssignSparksValue();
    }

    private void AssignSparksValue()
    {
        switch (m_SparkType)
        {
            case SparksType.Rock:
                SparkValue = 1;
                break;
            case SparksType.MoonGold:
                SparkValue = 2;
                break;
            case SparksType.MoonWhite:
                SparkValue = 4;
                break;
            case SparksType.HeartRed:
                SparkValue = 8;
                break;
            case SparksType.HeartBlack:
                SparkValue = 16;
                break;
            case SparksType.AppleRed:
                SparkValue = 32;
                break;
            case SparksType.AppleBlack:
                SparkValue = 64;
                break;
            case SparksType.CherryRed:
                SparkValue = 128;
                break;
            case SparksType.CherryBlack:
                SparkValue = 256;
                break;
            case SparksType.GemRed:
                SparkValue = 512;
                break;
            case SparksType.GemGreen:
                SparkValue = 1024;
                break;
            case SparksType.Star:
                SparkValue = 2048;
                break;
            default:
                break;
        }
    }

    private bool IsDragging, IsReleasingObj, IsCollidingWiththeMatched;

    private void OnMouseDrag()
    {
        MousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, distance);
        Vector3 objPosTobe = Camera.main.ScreenToWorldPoint(MousePosition);
        transform.position = objPosTobe;
        IsDragging = true;
    }

    public void OnTriggerEnter2D(Collider2D collision)
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


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_SparkIconPreview?.SetActive(false);
        m_CollidedSpark = null;
    }

    private void OnMouseUp()
    {
        IsDragging = false;
        IsReleasingObj = true;

        if (m_SparkType != SparksType.Star)
        {
            if (m_SparkIconPreview.activeSelf && null != m_CollidedSpark)
            {
                m_Controller.CalculateScore(SparkValue, m_CollidedSpark.GetComponent<SparksController>().SparkValue);
                m_Controller.MergeObject(this.gameObject, m_CollidedSpark);
            }
        }

    }

}