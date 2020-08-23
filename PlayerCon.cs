using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCon : MonoBehaviour
{
    public AudioClip deathClip;
    public float jumpForce = 1f;

    private int jumpCount = 0;
    private float downhill = 0;
    private bool flying = false;
    private bool isGrounded = false;
    private bool isDead = false;

    private Rigidbody2D playerRigidbody;
    private Animator animator;
    private AudioSource playerAudio;

    // Start is called before the first frame update
    void Start()
    {
        playerRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead) return;
        //사용자입력을 감지하고 점프를 실행한다.
        if(Input.GetMouseButtonDown(0) && jumpCount < 2)
        {
            jumpCount++;
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.AddForce(new Vector2(0, jumpForce));
            playerAudio.Play();
        }
        else if(Input.GetMouseButtonUp(0) && playerRigidbody.velocity.y > 0)
        {
            // 플레이어가 마우스버튼을 뗐을때 점프속도를 절반으로 줄여준다.
            playerRigidbody.velocity *= .5f;
        }
        if (Input.GetKey(KeyCode.Space) && !isGrounded && playerRigidbody.velocity.y < 0 && GameManager.instance.time > 0)
        {
            flying = true;
            playerRigidbody.gravityScale = 0.5f;
            playerRigidbody.transform.position += new Vector3(0.013f, 0, 0);
            GameManager.instance.Downhill(-Time.deltaTime);
        }
        else
        {
            flying = false;
            playerRigidbody.gravityScale = 3f;
        }
        if (isGrounded)
        {
            transform.Translate(-0.02f, 0, 0);
        }


        animator.SetBool("Flying", flying);
        animator.SetBool("Grounded", isGrounded);
    }

    private void Die()
    {
        animator.SetTrigger("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play();

        playerRigidbody.velocity = Vector2.zero;
        isDead = true;

        GameManager.instance.OnPlayerDead();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Dead") && !isDead)
        {
            Die();
        }
        if(other.CompareTag("Star"))
        {
            GameManager.instance.Downhill(1f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.contacts[0].normal.y > .7f)
        {
            isGrounded = true;
            jumpCount = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isGrounded = false;
    }
}
