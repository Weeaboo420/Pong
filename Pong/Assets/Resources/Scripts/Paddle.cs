using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private bool _isRightPaddle = true, _isCPU = false;
    private bool _movingUp;
    private Transform _ballTransform;

    //AI stuff
    private int _tick = 0;
    private float _tickTimer = 0f;
    private const float _secondsPerTick = 0.08f;
    private float _targetBallHeight;

    public bool GetMovingUp()
    {
        return _movingUp;
    }    

    private float EstimateBallHeight() //"Estimate" the height of the ball, making the AI seem more natural in its movements.
    {        
        if (_ballTransform != null)
        {
            if (_tick % 7 == 0)
            {
                return Mathf.Clamp(_ballTransform.position.y, -Globals.YLimit, Globals.YLimit);
            }

            else
            {
                return Mathf.Clamp(_ballTransform.position.y + Random.Range(-0.5f, 0.75f), -Globals.YLimit, Globals.YLimit);
            }
        }

        return transform.position.y;
    }

    private void Start()
    {
        if (_isCPU)
        {
            _targetBallHeight = transform.position.y;
        }
    }

    private void Update()
    {
        Vector2 myPos = transform.position;        

        if (!_isCPU) //If this is a player controlled paddle
        {
            if (_isRightPaddle)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    myPos.y += Globals.PaddleSpeed * Time.deltaTime;
                    _movingUp = true;
                }

                else if (Input.GetKey(KeyCode.DownArrow))
                {
                    myPos.y -= Globals.PaddleSpeed * Time.deltaTime;
                    _movingUp = false;
                }
            }

            else
            {
                if (Input.GetKey(KeyCode.W))
                {
                    myPos.y += Globals.PaddleSpeed * Time.deltaTime;
                    _movingUp = true;
                }

                else if (Input.GetKey(KeyCode.S))
                {
                    myPos.y -= Globals.PaddleSpeed * Time.deltaTime;
                    _movingUp = false;
                }
            }

            myPos.y = Mathf.Clamp(myPos.y, -Globals.YLimit, Globals.YLimit); //Limit y position
            transform.position = myPos;

        } else //If this is a computer-controlled paddle
        {

            if (_ballTransform == null)
            {
                if (GameObject.FindGameObjectWithTag("Ball") != null)
                {
                    _ballTransform = GameObject.FindGameObjectWithTag("Ball").transform;
                }
            } 
            
            else
            {
                //AI Update Tick
                _tickTimer += Time.deltaTime;
                if(_tickTimer >= _secondsPerTick)
                {
                    _tickTimer -= _secondsPerTick;
                    _tick++;

                    if(_tick % 2 == 0 || _tick % 3 == 0 || _tick % 5 == 0 || _tick % 7 == 0)
                    {
                        _targetBallHeight = EstimateBallHeight();
                    }

                }                              

            }

            //_targetBallHeight = Mathf.Lerp(EstimateBallHeight(), _targetBallHeight, Time.deltaTime * 1f); //Lerp the target ball height, otherwise the AI paddle moves kinda jankily.

            if (myPos.y < _targetBallHeight - 0.1f)
            {
                myPos.y += Globals.PaddleSpeed * Time.deltaTime;
                _movingUp = true;
            }

            else if (myPos.y > _targetBallHeight + 0.1f)
            {
                myPos.y -= Globals.PaddleSpeed * Time.deltaTime;
                _movingUp = false;
            }

            myPos.y = Mathf.Clamp(myPos.y, -Globals.YLimit, Globals.YLimit); //Limit y position
            transform.position = myPos;

        }




    }

}
