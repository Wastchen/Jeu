using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private Transform m_playerTransform = null;
    [SerializeField]
    private SpriteRenderer m_playerSprite = null;
    [SerializeField]
    private Animator m_playerAnimator = null;
    [SerializeField]
    private Rigidbody2D m_playerRigidbody = null;
    [SerializeField]
    private PlayerStats m_stats = null;
    [SerializeField]
    private GameObject m_swordRight = null;
    [SerializeField]
    private GameObject m_swordLeft = null;

    public int moveSpeed = 3;
    public int jumpHeight = 30;
    public int pushBack = 500;
    private float resetTimer = 0.2f;
    private bool lookingRight = false;
    private bool isAttacking = false;
    private bool wasHit = false;
    private bool canJump = true;

    void Update()
    {
        Attack();
        Move();
        Jump();
        ResetHit();
    }

    private void Move()
    {
        if (Attack() == false)
        {
            if (Input.GetKey(KeyCode.A))
            {
                lookingRight = false;
                m_playerTransform.position = m_playerTransform.position += (transform.right * -moveSpeed * Time.deltaTime);
                m_playerSprite.flipX = true;
                m_playerAnimator.SetBool("isRunning", true);

                if (Input.GetKeyDown(KeyCode.S))
                {
                    m_playerAnimator.SetBool("isSliding", true);
                }
                else
                {
                    m_playerAnimator.SetBool("isSliding", false);
                }
            }
            else if (Input.GetKey(KeyCode.D))
            {
                lookingRight = true;
                m_playerTransform.position = m_playerTransform.position += (transform.right * moveSpeed * Time.deltaTime);
                m_playerSprite.flipX = false;
                m_playerAnimator.SetBool("isRunning", true);

                if (Input.GetKeyDown(KeyCode.S))
                {
                    m_playerAnimator.SetBool("isSliding", true);
                }
                else
                {
                    m_playerAnimator.SetBool("isSliding", false);
                }
            }
            else
            {
                m_playerAnimator.SetBool("isRunning", false);
            }
        }
    }

    private void Jump()
    {
        if (Attack() == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) && canJump == true)
            {
                m_playerRigidbody.AddForce(Vector2.up * jumpHeight);
                m_playerAnimator.SetBool("isJumping", true);
                canJump = false;
            }
            else
            {
                m_playerAnimator.SetBool("isJumping", false);
            }
        }
    }

    private bool Attack()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            isAttacking = true;
            if (lookingRight)
            {
                m_swordRight.SetActive(true);
            }
            else
            {
                m_swordLeft.SetActive(true);
            }

            m_playerAnimator.SetBool("isAttacking", true);

            return true;
        }
        else
        {
            isAttacking = false;
            m_swordRight.SetActive(false);
            m_swordLeft.SetActive(false);
            m_playerAnimator.SetBool("isAttacking", false);

            return false;
        }
    }

    private void ResetHit()
    {
        if (wasHit)
        {
            resetTimer -= Time.deltaTime;

            if (resetTimer <= 0)
            {
                wasHit = false;
                m_playerAnimator.SetBool("isHit", false);
                resetTimer = 0.2f;

            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8 && isAttacking == false)
        {
            m_stats.Hp -= 25;
            wasHit = true;
            m_playerAnimator.SetBool("isHit", true);

        }
    }
}
