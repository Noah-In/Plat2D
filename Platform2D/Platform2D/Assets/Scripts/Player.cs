using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Life life;
    Animator animator;
    AudioSource jumpSfx;

    [Header("Stats")]
    [SerializeField] float speed;
    [SerializeField] float jumpSpeed;
    public int coins;

    [Header("GROUND CHECK ")]
    [SerializeField] Transform rayOrigin;
    [SerializeField] float rayLenght;
    [SerializeField] float boxWidth;
    [SerializeField] LayerMask groundLayer;

    [Header("SHOOT")]
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform firepoint;

    [Header("Hud")]
    [SerializeField] TextMeshProUGUI coinsText;

    bool isGrounded;
    bool canDoubleJump;
    bool isFlipped;
    bool isDead;

    Vector3 respawnPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        life = GetComponent<Life>();
        animator = GetComponent<Animator>();
        jumpSfx = GetComponent<AudioSource>();

        respawnPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("IsDead", isDead);

        if (life.healthPoints <= 0 && !isDead) 
        {
            StartCoroutine(Death());
            isDead = true;
        }

        if (isDead)
        {
            return;
        }

        GroundCheck();
        Movement();
        Jump();
        Flip();
        Shoot();

        coinsText.text = coins.ToString();
    }

    void Shoot()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Instantiate(bulletPrefab, firepoint.position, transform.rotation);
        }
    }

    void Flip()
    {
        if (Input.GetAxis("Horizontal") < 0 && isFlipped == false)
        {
            transform.Rotate(0, 180, 0);
            isFlipped = true;
        }
        if (Input.GetAxis("Horizontal") > 0 && isFlipped == true)
        {
            transform.Rotate(0, 180, 0);
            isFlipped = false;
        }
    }

    void Movement()
    {
        rb.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, rb.velocity.y);
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            jumpSfx.Play();
        }
        else if (canDoubleJump && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpSpeed);
            canDoubleJump = false;
            jumpSfx.Play();
        }
    }

    private void GroundCheck()
    {
        Debug.DrawRay(rayOrigin.position + Vector3.left * boxWidth / 2, Vector2.down * rayLenght, Color.blue);
        Debug.DrawRay(rayOrigin.position + Vector3.right * boxWidth / 2, Vector2.down * rayLenght, Color.blue);

        if (Physics2D.BoxCast(rayOrigin.position, new Vector2(boxWidth, rayLenght), 0, Vector2.down, rayLenght, groundLayer).collider != null)
        {
            isGrounded = true;
            canDoubleJump = true;
        }

        else
        {
            isGrounded = false;
        }
    }

    IEnumerator Death()
    {
        rb.velocity = Vector2.zero;
        animator.SetBool("IsDead", true);
        yield return new WaitForSeconds(2);
        //SceneManager.LoadScene(0);
        isDead = false;
        life.healthPoints = 5;
        animator.SetBool("IsDead", false);
        transform.position = respawnPos; 
        //yield return new WaitForSeconds(1);
        //animator.ResetTrigger("Revive"); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Checkpoint"))
        {
            respawnPos = collision.transform.position;
        }
    }
}
