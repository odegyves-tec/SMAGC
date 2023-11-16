using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTransform : MonoBehaviour
{
    public Animator anim;

    public Camera cam;
    public float speed;
    public float rotationSpeed;

    public List<Transform> goals;
    private int m_currentGoalIndex = 0;

    private Transform m_transform;
    private Vector3 m_destinationPosition;

    private void Start()
    {
        m_transform = GetComponent<Transform>();
        m_destinationPosition = m_transform.position;
    }

    private void Update()
    {
        m_destinationPosition = goals[m_currentGoalIndex].position;

        Vector3 currentPosition = m_transform.position;
        Vector3 displacementVector = m_destinationPosition - currentPosition;

        bool isWalking = displacementVector.magnitude >= 1.0f;
        anim.SetBool("isWalking", isWalking);

        if (!isWalking)
        {
            if (m_currentGoalIndex < goals.Count - 1)
            {
                m_currentGoalIndex++;
            }
            return;
        }

        Vector3 currentDirection = m_transform.forward;
        Vector3 targetDirection = Vector3.Normalize(displacementVector);
        Vector3 direction = Vector3.RotateTowards(
            currentDirection,
            targetDirection,
            rotationSpeed * Time.deltaTime, 0.0f);

        m_transform.rotation = Quaternion.LookRotation(direction);
        m_transform.position += direction * speed * Time.deltaTime;
    }
}
