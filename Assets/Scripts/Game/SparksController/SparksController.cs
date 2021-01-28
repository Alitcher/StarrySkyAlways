using UnityEngine;

public class SparksController : MonoBehaviour
{
    private float distance = 10f;
    public static Vector3[] startPos;
    private Vector3 MousePosition;
    [SerializeField] private GameObject m_SparkIconPreview;
    [SerializeField] private SparksType m_SparkType;
    [SerializeField] private SparksData m_SparkData;

    private void Awake()
    {
        m_SparkIconPreview.SetActive(false);
        m_SparkData = GameObject.FindObjectOfType<SparksData>();

    }
    private void OnMouseDown()
    {
        if (this.name == "Spark_Rock_1")
        {

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

    private void OnMouseOver()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == this.name)
        {
            if (IsDragging)
            {
                m_SparkIconPreview.SetActive(true);
                print("spark matches");
                IsCollidingWiththeMatched = true;
            }
            else if(!IsDragging && IsReleasingObj)
            {
                //Destroy(this.gameObject);
                Destroy(collision.gameObject);
                GameObject newSparks = Instantiate(m_SparkData.m_SparksContainer[1], this.transform.position, Quaternion.identity);
                newSparks.name = m_SparkData.m_SparksContainer[1].name;
            }
        }


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        m_SparkIconPreview.SetActive(false);
    }

    private void OnMouseUp()
    {
        IsDragging = false;
        IsReleasingObj = true;
    }
}