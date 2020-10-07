using UnityEngine;

public class Goal : MonoBehaviour
{
    public bool IsRightGoal = true;
    private ScoreKeeper _scoreKeeper;

    private void Start()
    {
        _scoreKeeper = GameObject.FindObjectOfType<ScoreKeeper>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            _scoreKeeper.AddScore(!IsRightGoal);
            Destroy(collision.gameObject);
        }
    }
}
