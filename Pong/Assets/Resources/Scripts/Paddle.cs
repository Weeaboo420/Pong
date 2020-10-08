using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField]
    private bool _isRightPaddle = true;
    private bool _movingUp;

    public bool GetMovingUp()
    {
        return _movingUp;
    }

    private void Update()
    {
        Vector2 myPos = transform.position;

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

    }

}
