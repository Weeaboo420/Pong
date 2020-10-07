using UnityEngine;

public class Paddle : MonoBehaviour
{
    public bool IsRightPaddle = true;

    private void Update()
    {
        Vector2 myPos = transform.position;

        if (IsRightPaddle)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                myPos.y += Globals.PaddleSpeed * Time.deltaTime;
            }

            else if (Input.GetKey(KeyCode.DownArrow))
            {
                myPos.y -= Globals.PaddleSpeed * Time.deltaTime;
            }
        }

        else
        {
            if (Input.GetKey(KeyCode.W))
            {
                myPos.y += Globals.PaddleSpeed * Time.deltaTime;
            }

            else if (Input.GetKey(KeyCode.S))
            {
                myPos.y -= Globals.PaddleSpeed * Time.deltaTime;
            }
        }

        myPos.y = Mathf.Clamp(myPos.y, -Globals.YLimit, Globals.YLimit); //Limit y position

        transform.position = myPos;

    }

}
