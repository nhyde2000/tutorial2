using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public float jump;
    public TextMeshProUGUI score;
    private int scoreValue;
    public GameObject WinTextObject;
    public GameObject LoseTextObject;
    public TextMeshProUGUI lives;
    private int livesValue;
    private bool isOnGround;
    public Transform groundcheck;
    public float checkRadius;
    public LayerMask allGround;
    public Animator animator;
    private bool facingRight = true;
   void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        scoreValue = 0;

        rd2d = GetComponent<Rigidbody2D>();
        livesValue = 3;

        SetScoreText();
        WinTextObject.SetActive(false);

        SetScoreText();
        LoseTextObject.SetActive(false);
    }
      void SetScoreText()
    {
        score.text = "Score: " + scoreValue.ToString();
        if (scoreValue >= 8)
        {
            SoundManager.PlaySound("WinFantasia");
            WinTextObject.SetActive(true);
            gameObject.SetActive(false);
            
        }
        score.text = "Score: " + scoreValue.ToString();
        if (scoreValue == 4)
        {
            livesValue = 3;
            transform.position = new Vector2(100f, 0.5f);
        }

        lives.text = "Lives: " + livesValue.ToString();
        if (livesValue == 0)
        {
            LoseTextObject.SetActive(true);
            gameObject.SetActive(false);
        }
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        isOnGround = Physics2D.OverlapCircle(groundcheck.position, checkRadius, allGround);
        animator.SetFloat("HorizontalValue", Mathf.Abs(Input.GetAxis("Horizontal")));
        animator.SetFloat("VerticalValue", Input.GetAxis("Vertical"));
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement <0)
        {
            Flip();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;
            score.text = "Score: " + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            SetScoreText();
        }
       if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            lives.text = "Lives: " + livesValue.ToString();
            Destroy(collision.collider.gameObject);
            SetScoreText();
        } 
    }

  private void OnCollisionStay2D(Collision2D collision)
    {
        
        if (collision.collider.tag == "Ground" && isOnGround)
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0f, jump), ForceMode2D.Impulse);
            }
        }
    }
    
}