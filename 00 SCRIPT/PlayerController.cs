using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D _rigi;
    [SerializeField] private float speed, forceJump;
    float inputX;
    Animator animator;
    bool isJump = false;
    void Awake()
    {
        _rigi = GetComponent<Rigidbody2D>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Update()
    {
        if (!isJump)
        {
            inputX = (Input.GetAxisRaw("Horizontal"));
            if (inputX != 0)
            {
                _rigi.velocity = new Vector2(speed * inputX, _rigi.velocity.y);
                UpdateDirection(inputX);
            }
            else
            {
                _rigi.velocity = new Vector2(0, _rigi.velocity.y);
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                isJump = true;
                _rigi.AddForce(new Vector2(0, forceJump));
                animator.SetBool("walk", false);
                animator.SetBool("idle", false);
                animator.SetBool("jump", true);
            }
        }
        UpdateAnimation();
    }

    void UpdateAnimation()
    {
        if (_rigi.velocity.x != 0)
        {
            animator.SetBool("idle", false);
            animator.SetBool("walk", true);
        }
        else
        {
            animator.SetBool("walk", false);
            animator.SetBool("idle", true);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        animator.SetBool("jump", false);
        isJump = false;
        animator.SetBool("idle", true);
    }

    void UpdateDirection(float direction)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Abs(scale.x) * direction;

        transform.localScale = scale;
    }
}
