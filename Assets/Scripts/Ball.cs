using UnityEngine;

public class Ball : MonoBehaviour
{
    // config parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xVelocity = 2f; // ball x velocity
    [SerializeField] float yVelocity = 15f; // ball y velocity
    [SerializeField] AudioClip[] ballSounds; // audio
    [SerializeField] float randomFactor = 0.2f;

    // cashed component references
    AudioSource myAudioSource;
    Rigidbody2D myRigidBody;

    // state
    Vector2 paddleToBallVector;
    bool hasStarted = false; 
    float t0 = 0f;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallVector = transform.position - paddle1.transform.position; // space between paddle and ball
        myAudioSource = GetComponent<AudioSource>();
        myRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        LaunchOnMouseClick();
        CheckMouseClick();
    }

    private void LaunchOnMouseClick()
    {

       if (Input.GetMouseButton(0))
        {
            hasStarted = true;
            // GetComponent<Rigidbody2D>().velocity = new Vector2(xVelocity, yVelocity);
        }
        else
        {
            if (!hasStarted)
            {
                LockBallToPaddle();
            }
        }
    }
    // locking ball to paddle
    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void CheckMouseClick()
    {
        t0 = Time.time;
        if (Input.GetMouseButtonUp(0) && (Time.time - t0) > 2.0f)
        {
            myRigidBody.velocity = new Vector2(xVelocity * 2, yVelocity * 2);
            Debug.Log(Time.time - t0);

        }
        else if(Input.GetMouseButtonUp(0) && (Time.time - t0) < 2.0f)
        {
            myRigidBody.velocity = new Vector2(xVelocity, yVelocity);
            Debug.Log(Time.time - t0);
        }

    }

    private void OnCollisionEnter2D()
    {
        Vector2 velocityTweak = new Vector2(Random.Range(0f, randomFactor), Random.Range(0f, randomFactor));

        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigidBody.velocity += velocityTweak;
        }
    }
}
