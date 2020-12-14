using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform m_enemyTransform = null;
    [SerializeField]
    private SpriteRenderer m_enemySprite = null;
    public int health = 50;
    public int moveSpeed = 3;
    public bool goingRight = false;
    public bool goingLeft = true;
    void Update()
    {
        Move();
    }

    private void Move()
    {
        if (m_enemyTransform.position.x > 0 && goingLeft == true)
        {
            m_enemyTransform.position = m_enemyTransform.position += (transform.right * -moveSpeed * Time.deltaTime);
            m_enemySprite.flipX = false;
        }
        else
        {
            goingLeft = false;
            goingRight = true;
        }

        if (m_enemyTransform.position.x < 8 && goingRight == true)
        {
            m_enemyTransform.position = m_enemyTransform.position += (transform.right * moveSpeed * Time.deltaTime);
            m_enemySprite.flipX = true;
        }
        else
        {
            goingLeft = true;
            goingRight = false;
        }
    }
}
