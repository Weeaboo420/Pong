using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool MovingRight, MovingUp = true;    
    private AudioSource _audioSource;    
    private GameObject _lastPaddle, _lastBounds;
    private float _currentSpeed;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentSpeed = Globals.BallSpeed;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Paddle") && _lastPaddle == null)
        {
            MovingRight = !MovingRight;
            _lastPaddle = collision.gameObject;

            MovingUp = collision.gameObject.GetComponent<Paddle>().GetMovingUp();

            _audioSource.Play();
            _audioSource.pitch = Random.Range(0.85f, 1.1f);

            _currentSpeed += 0.25f;
            _currentSpeed = Mathf.Clamp(_currentSpeed, Globals.BallSpeed, Globals.MaxBallSpeed);
        } 
        
        else if(collision.gameObject.CompareTag("Bounds") && _lastBounds == null)
        {
            MovingUp = !MovingUp;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Paddle"))
        {            
            _lastPaddle = null;
        }

        else if(collision.gameObject.CompareTag("Bounds"))
        {
            _lastBounds = null;
        }
    }

    private void FixedUpdate()
    {
        Vector2 newVelocity = _rigidbody.velocity;

        if(MovingRight)
        {
            newVelocity.x = _currentSpeed;
        } else
        {
            newVelocity.x = -_currentSpeed;
        }

        if(MovingUp)
        {
            newVelocity.y = _currentSpeed;
        } else
        {
            newVelocity.y = -_currentSpeed;
        }

        _rigidbody.velocity = newVelocity;
    }

}
