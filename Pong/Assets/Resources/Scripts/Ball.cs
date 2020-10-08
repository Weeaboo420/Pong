﻿using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool MovingRight, MovingUp = true;    
    private AudioSource _audioSource;    
    private GameObject _lastPaddle, _lastBounds;
    private float _currentSpeed;    

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _currentSpeed = Globals.BallSpeed;
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

    private void Update()
    {
        Vector2 myPos = transform.position;

        if(MovingRight)
        {
            myPos.x += _currentSpeed * Time.deltaTime;
        } else
        {
            myPos.x -= _currentSpeed * Time.deltaTime;
        }

        if(MovingUp)
        {
            myPos.y += _currentSpeed * Time.deltaTime;
        } else
        {
            myPos.y -= _currentSpeed * Time.deltaTime;
        }

        transform.position = myPos;
    }

}
