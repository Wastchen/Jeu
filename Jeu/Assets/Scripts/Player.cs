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

    public int moveSpeed = 3;
    public int jumpHeight = 30;
    private bool canJump = true;

    void Update()
    {
        Move();
        Jump();
        Attack();
        // Kev veut pas download Tortoise Git faque i perd son temp comme un asti de retard
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.A))
        {
            m_playerTransform.position = m_playerTransform.position += (transform.right * -moveSpeed * Time.deltaTime);
            m_playerSprite.flipX = true;
            m_playerAnimator.SetBool("isRunning", true);

            if (Input.GetKeyDown(KeyCode.LeftShift))
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
            m_playerTransform.position = m_playerTransform.position += (transform.right * moveSpeed * Time.deltaTime);
            m_playerSprite.flipX = false;
            m_playerAnimator.SetBool("isRunning", true);

            if (Input.GetKeyDown(KeyCode.LeftShift))
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

    private void Jump()
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

    private void Attack()
    {
        if (Input.GetKey(KeyCode.Mouse0))
        {
            m_playerAnimator.SetBool("isAttacking", true);
        }
        else
        {
            m_playerAnimator.SetBool("isAttacking", false);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        canJump = true;
    }
}
