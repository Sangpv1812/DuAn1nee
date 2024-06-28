using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f; // Tốc độ di chuyển của nhân vật
    public float jumpForce = 10f; // Lực nhảy
    public Transform groundCheck; // Điểm kiểm tra chạm đất
    public float checkRadius = 0.1f; // Bán kính kiểm tra chạm đất
    public LayerMask whatIsGround; // Lớp để kiểm tra chạm đất

    private Rigidbody2D rb;
    private bool isGrounded;
    private bool jumpRequest;

    private bool facingRight = true; // Hướng nhân vật đang quay mặt

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        // Kiểm tra nếu nhân vật đang chạm đất
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // Lấy đầu vào từ bàn phím
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        // Kiểm tra nhảy
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            jumpRequest = true;
        }

        // Đảo chiều nhân vật khi đổi hướng
        if (facingRight == false && moveInput > 0)
        {
            Flip();
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        // Thực hiện nhảy
        if (jumpRequest)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            jumpRequest = false;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        Vector3 scaler = transform.localScale;
        scaler.x *= -1;
        transform.localScale = scaler;
    }
}
