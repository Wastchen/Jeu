using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    private Transform m_enemyTransform = null;
    [SerializeField]
    private SpriteRenderer m_enemySprite = null;
    [SerializeField]
    private Animator m_enemyAnimator = null;
    public int moveSpeed = 3;
    private float deathTimer = 1f;
    public bool goingRight = false;
    public bool goingLeft = true;
    private bool wasHit = false;
    void Update()
    {
        Move();
        Death();
    }

    private void Move()
    {
        if (wasHit == false)
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

    private void Death()
    {
        if (wasHit)
        {
            deathTimer -= Time.deltaTime;

            if (deathTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            wasHit = true;
            m_enemyAnimator.SetBool("isHit", true);
        }
    }
}
