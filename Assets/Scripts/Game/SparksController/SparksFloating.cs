using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SparksFloating : MonoBehaviour
{
    private GameObject m_SelfReference;

    [SerializeField] private Vector3 m_OriginalPos;
    [SerializeField] private Vector2[] m_TargetPos;
    [SerializeField] private float m_OffsetValue ;
    [SerializeField] private float m_MoveSpeed = 2f;

    [SerializeField] private AnimationCurve m_FlyingCurve;
    [SerializeField] private int m_PosIndex;


    void Start()
    {
    }

    void Awake()
    {
        m_SelfReference = this.gameObject;
        m_OriginalPos = m_SelfReference.transform.position;
        m_TargetPos = new Vector2[6];
        for (int i = 0; i < m_TargetPos.Length; i++)
        {
            m_TargetPos[i] = RandomOffset(m_OriginalPos);
        }

        m_PosIndex = Random.Range(0, m_TargetPos.Length);
    }

    void Update()
    {
        FloatSparks();
    }

    private void FloatSparks()
    {
        int indextmp = m_PosIndex;

        m_SelfReference.transform.position = Vector2.MoveTowards(m_SelfReference.transform.position,
                                                                 m_TargetPos[m_PosIndex],
                                                                 m_MoveSpeed * Time.deltaTime);
    }

    private bool CheckFinishMove(Vector2 current, Vector2 target)
    {
        return current.x == target.x && current.y == target.y;
    }


    private Vector2 RandomOffset(Vector2 referenceTrans)
    {
        return new Vector2(Random.Range(referenceTrans.x + m_OffsetValue / 4, referenceTrans.x + m_OffsetValue),
                           Random.Range(referenceTrans.y + m_OffsetValue / 4, referenceTrans.y + m_OffsetValue));
    }
}
