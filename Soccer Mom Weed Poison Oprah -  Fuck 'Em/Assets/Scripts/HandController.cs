using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    //Public Variables
    

    //Private Variables
    private Vector3 m_startingPosition;
    private Vector3 m_endPosition; // = new Vector3(0, 0, 0);
    private float m_yDiff, m_percentTravelled;
    public const float m_timeToEnd = 10.0f;
    public const float m_TimeBeforeReach = 2;
    private float m_TimeElapsed;

    private bool reaching = false;
    // Start is called before the first frame update
    void Start()
    {
        m_endPosition = new Vector3(0, 0, 0);
        m_startingPosition = transform.position;
        m_yDiff = m_startingPosition.y - m_endPosition.y;
        m_TimeElapsed = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.A))
        {
            slap();
        }
        if(Input.GetKeyDown(KeyCode.B))
        {
            premiumCandy();
        }
        if (!StoreScript.storeActive)
        {
            m_TimeElapsed += Time.deltaTime;
            m_percentTravelled = (m_TimeElapsed - m_TimeBeforeReach) / m_timeToEnd;
            if (m_percentTravelled >= 1) //Game Over Here
            {
                GameState.Instance.InvokeGameOver();
            }
            else
            {
                Vector3 newPos = new Vector3(0, m_startingPosition.y - (m_yDiff * m_percentTravelled), 0);
                transform.position = newPos;
            }
        } 
    }
    public void slap()
    {
        if (m_TimeElapsed > m_TimeBeforeReach)
        {
            //m_TimeElapsed = m_TimeElapsed - m_timeToEnd / 10;
            transform.position = m_startingPosition;
            m_TimeElapsed = 0.0f; //resets to beginning of initial wait time
        }
    }

    public void premiumCandy()
    {
        transform.position = m_startingPosition;
        m_TimeElapsed = 0 - m_TimeBeforeReach; //resets to beginning of initial wait time * 2;
    }
}
