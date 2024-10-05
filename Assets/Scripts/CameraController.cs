using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class CameraController : MonoBehaviour
{
    private Vector3 m_Translation;
    public float MoveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        m_Translation = Vector3.zero;
    }

    // Update is called once per frame
    void Update()
    {
        m_Translation = Vector3.zero;
        CalculateMovement();
        ApplyMovement();
    }

    private void FixedUpdate()
    {
        
    }

    private void CalculateMovement()
    {
        float vertical = Input.GetAxis("Vertical");
        if(vertical != 0.0f)
        { 
            m_Translation += ((vertical * Vector3.forward) * MoveSpeed);
        }

        float horizontal = Input.GetAxis("Horizontal");
        if (horizontal != 0.0f)
        {
            m_Translation += ((horizontal * Vector3.right) * MoveSpeed);
        }
    }

    private void ApplyMovement()
    {
        if(m_Translation != Vector3.zero)
        {
            transform.position += (m_Translation * Time.deltaTime);
        }
    }
}
