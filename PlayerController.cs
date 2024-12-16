using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator _ani;
    public Animator ani => _ani;
    Rigidbody2D _rigi;
    [SerializeField] List<PhysicsMaterial2D> _materials;
    [SerializeField] float speed, jumpForce;
    [SerializeField] bool isOnGround, isOnSlope;
    [SerializeField] float slopeAngle;
    IPlayerState _currentState;
    IPlayerState _walkState;
    IPlayerState _idleState;
    IPlayerState _jumpState;
    IPlayerState _sitDownState;
    IPlayerState _slideState;
    IPlayerState _runState;
    IPlayerState _sneakingState;
    #region check state
    bool isCorotineRunning = false;
    bool isLongState = false;
    bool isRunState = false;
    #endregion


    void Start()
    {
        _ani = GetComponentInChildren<Animator>();
        _rigi = GetComponent<Rigidbody2D>();
        _idleState = new I_IdleSate();
        _walkState = new WalkState();
        _jumpState = new JumpState();
        _sitDownState = new SitDownState();
        _slideState = new SlideState();
        _runState = new RunState();
        _sneakingState = new SneakingState();
        _currentState = _idleState;

        _rigi.sharedMaterial = _materials[0];
    }

    void Update()
    {
        Moving();
        ChangeState();
        CheckOnGround();
    }

    void Moving()
    {
        float inputX = Input.GetAxis("Horizontal");
        Debug.Log("Horizontal " + inputX);
        if (inputX != 0)
        {
            _rigi.sharedMaterial = _materials[0];
            UpdateDirectionPlayer(inputX);
            if (isOnSlope)
            {
                slopeAngle = slopeAngle * Mathf.Deg2Rad;
                float velocityX = Mathf.Cos(slopeAngle) * inputX * speed;
                float velocityY = Mathf.Sin(slopeAngle);
                velocityY -= Mathf.Abs(Physics2D.gravity.y) * Mathf.Sin(slopeAngle);
                _rigi.velocity = new Vector2(velocityX, velocityY);

                Debug.Log("Velocity " + _rigi.velocity);
            }
            else
            {
                _rigi.velocity = new Vector2(speed * inputX, this._rigi.velocity.y);
            }
        }
        else
        {
            _rigi.velocity = new Vector2(0, this._rigi.velocity.y);
            _rigi.sharedMaterial = _materials[1];
        }

        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            _rigi.AddForce(new Vector2(_rigi.velocity.x, jumpForce));
        }
    }

    void UpdateDirectionPlayer(float inputX)
    {
        Vector3 scale = transform.localScale;
        scale.x = Mathf.Sign(inputX * Mathf.Abs(transform.localScale.x));

        transform.localScale = scale;
    }

    void ChangeState()
    {
        _currentState.ExitState(this);
        if (!isLongState)
        {
            if (isOnGround)
            {
                if (_rigi.velocity.x == 0)
                {
                    _currentState = _idleState;
                    isRunState = false;
                }
                else
                {
                    if (!isRunState)
                    {
                        _currentState = _walkState;
                        StartCoroutine(SwitchRunState());
                    }
                    else
                    {
                        _currentState = _runState;
                    }
                }

                if (Input.GetKeyDown(KeyCode.C))
                {
                    isLongState = true;
                    _currentState = _sitDownState;

                }

                if (Input.GetKeyDown(KeyCode.S))
                {
                    isLongState = true;
                    _currentState = _slideState;
                }
                if (Input.GetKeyDown(KeyCode.X))
                {
                    _currentState = _sneakingState;
                    isLongState = true;
                }
            }
            else
            {
                _currentState = _jumpState;
            }
        }
        else
        {
            if (!isCorotineRunning)
                StartCoroutine(WaitLongStateComplete());
        }
        _currentState.EnterState(this);
    }

    void CheckOnGround()
    {
        Vector3 direction = Vector2.down;
        float rayLenght = 2.5f;
        int layerMask = ~LayerMask.GetMask("Player");
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, rayLenght, layerMask);
        Debug.DrawRay(transform.position, direction * rayLenght, Color.red);
        if (hit.collider != null/* && hit.collider.gameObject.CompareTag("Ground")*/)
        {
            Debug.Log("Name : " + hit.collider.name);
            Debug.DrawRay(hit.point, hit.normal * rayLenght, Color.yellow);
            isOnGround = true;
            Debug.Log("X " + hit.normal.x);
            if (hit.normal.x != 0f)
            {
                isOnSlope = true;
                slopeAngle = Vector2.Angle(hit.normal, Vector2.up);
            }
            else
            {
                isOnSlope = false;
            }
        }
        else
        {
            isOnGround = false;
        }

    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = false;
        }
    }

    public IEnumerator WaitLongStateComplete()
    {
        isCorotineRunning = true;
        AnimatorStateInfo stateInfor = _ani.GetCurrentAnimatorStateInfo(0);

        while (stateInfor.normalizedTime < 1)
        {
            stateInfor = _ani.GetCurrentAnimatorStateInfo(0);
            yield return null;
        }
        isCorotineRunning = false;
        isLongState = false;
    }

    public IEnumerator SwitchRunState()
    {
        _currentState = _walkState;
        yield return new WaitForSeconds(1);
        isRunState = true;
    }

    public void CheckStateName()
    {
        AnimatorStateInfo stateInfo = _ani.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("Idle"))
            Debug.Log("Đang ở trạng thái Idle !");
        if (stateInfo.IsName("Walk"))
            Debug.Log("Đang ở trạng thái Walk !");
        if (stateInfo.IsName("Jump"))
            Debug.Log("Đang ở trạng thái Jump !");
        if (stateInfo.IsName("SitDown"))
            Debug.Log("Đang ở trạng thái Sit Down !");

    }

}
