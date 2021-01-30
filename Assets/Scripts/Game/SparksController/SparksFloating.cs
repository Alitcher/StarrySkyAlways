using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksFloating : MonoBehaviour
{
    private GameObject m_SelfReference;
    [SerializeField] private Vector3 m_OriginalPos;
    [SerializeField] private Vector3[] m_TargetPos;
    [SerializeField] private float m_OffsetValue ;
    [SerializeField] private float m_MaxValueX, m_MaxValueY, m_MinValueX, m_MinValueY;
    [SerializeField] private float m_TotalLerpTime = 2f, m_LerpPercent;
    [SerializeField][Range(0, 2)] private float m_CurrentLerpTime = 0f;

    [SerializeField] private AnimationCurve m_FlyingCurve;


    void Start()
    {
        m_MaxValueX = RandomOffset(m_OriginalPos).x;
        m_MaxValueY = RandomOffset(m_OriginalPos).y;
        m_MinValueX = -m_MaxValueX;
        m_MinValueY = -m_MaxValueY;
    }

    void Awake()
    {
        m_SelfReference = this.gameObject;
        m_OriginalPos = m_SelfReference.transform.position;
        m_TargetPos = new Vector3[6];
        for (int i = 0; i < m_TargetPos.Length; i++)
        {
            m_TargetPos[i] = RandomOffset(m_OriginalPos);

        }
    }

    void Update()
    {
        FloatSparks();
    }

    private void FloatSparks()
    {
        bool isReverse = false;

        if (m_CurrentLerpTime > m_TotalLerpTime)
        {
            m_CurrentLerpTime = m_TotalLerpTime;
        }
        else
        {
            if (!isReverse)
            m_CurrentLerpTime += Time.deltaTime;
        }
        m_LerpPercent = m_CurrentLerpTime / m_TotalLerpTime;

        if (m_LerpPercent <= 1 && !isReverse)
        {
            m_SelfReference.transform.position = Vector3.Lerp(m_OriginalPos, m_TargetPos[Random.Range(0,5)], m_LerpPercent);
        }
        else if(isReverse)
        {
           m_SelfReference.transform.position = Vector3.Lerp(m_TargetPos[Random.Range(0, 5)], m_OriginalPos, m_LerpPercent);
        }


    }

    private void MoveUp()
    { }

    private void MoveDown()
    { }

    private void MoveRight()
    { }
    private void MoveLeft()
    { }
    private Vector3 RandomOffset(Vector3 referenceTrans)
    {
        return new Vector3(referenceTrans.x + m_OffsetValue,
                           referenceTrans.y + m_OffsetValue,
                           referenceTrans.z);
    }
}
//do
//{
//}
//while (m_SelfReference.transform.position.x <= m_MinValueX);

//if (m_SelfReference.transform.position.x <= m_MinValueX)
//{
//    m_SelfReference.transform.position += new Vector3(m_OffsetValue * Time.deltaTime, 0, 0);
//    print("moveright");
//}
//else if (m_SelfReference.transform.position.x >= m_MaxValueX)
//{
//    m_SelfReference.transform.position -= new Vector3(m_OffsetValue * Time.deltaTime, 0, 0);
//    print("moveleft");
//}
//else
//{
//    m_SelfReference.transform.position += new Vector3(m_OffsetValue * Time.deltaTime, 0, 0);
//}


//if (m_SelfReference.transform.position.y <= m_MinValueX)
//{
//    m_SelfReference.transform.position += new Vector3(0, m_OffsetValue * Time.deltaTime, 0);

//    print("moveup");
//}
//else if (m_SelfReference.transform.position.y >= m_MaxValueY)
//{
//    m_SelfReference.transform.position -= new Vector3(0, m_OffsetValue * Time.deltaTime, 0);
//    print("movedown");
//}
//else
//{
//    m_SelfReference.transform.position += new Vector3(0, m_OffsetValue * Time.deltaTime, 0);
//}