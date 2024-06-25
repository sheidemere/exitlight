using UnityEngine;
using UnityEngine.InputSystem;

public class Hero : MonoBehaviour
{
    public Rigidbody2D _rigidbody;
    public Transform _groundCheck;
    public LayerMask _groundLayer;

    private float _horizontal;
    [SerializeField] private float speed;
    public float _jumpingPower = 24;
    private bool _isFacingRight = true;
    [SerializeField] private PlaySoundComponent _playSound;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playSound = GetComponent<PlaySoundComponent>();
        Cursor.visible = false;
    }

    void Update()
    {
        _rigidbody.velocity = new Vector2(_horizontal * speed, _rigidbody.velocity.y);

        _animator.SetBool("is_running", _rigidbody.velocity.x != 0);
        _animator.SetBool("is_ground", IsGrounded());
        _animator.SetFloat("vertical_velocity", _rigidbody.velocity.y);

        if (!_isFacingRight && _horizontal > 0f)
        {
            Flip();
        }
        else if (_isFacingRight && _horizontal < 0f)
        {
            Flip();
        }
    }

    private void Jump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded())
        {
            _playSound.Play("jump");
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpingPower);
        }

        if (context.canceled && _rigidbody.velocity.y > 0f)
        {
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _rigidbody.velocity.y * 0.5f);
        }

    }

    private bool IsGrounded()
    {
        return Physics2D.OverlapCircle(_groundCheck.position, 0.2f, _groundLayer);
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        _horizontal = context.ReadValue<Vector2>().x;
    }
}