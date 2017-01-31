using UnityEngine;

public class BallBehavior : MonoBehaviour
{
    public Vector2 velocity;
    public Rigidbody2D rb;
    public float paddleOomph;

    private float _startVelocity;

    // Use this for initialization
    void Start()
    {
        rb.velocity = velocity;
        _startVelocity = velocity.magnitude;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.collider.CompareTag("Wall"))
        {
            //velocity.x = rb.velocity.x;
            //velocity.y = (rb.velocity.y / 2.0f) + (coll.collider.attachedRigidbody.velocity.y / 3.0f);
            var wall = coll.collider.GetComponent<WallBehavior>();
            rb.velocity = velocity = new Vector2(velocity.x * wall.xBounce, velocity.y * wall.yBounce);
            if (wall.absorbsEnergy && rb.velocity.magnitude > _startVelocity)
            {
                rb.AddForce(-rb.velocity * 0.20f, ForceMode2D.Impulse);
                if (rb.velocity.magnitude < _startVelocity)
                {
                    Debug.Log("current:" + rb.velocity.magnitude);
                    Debug.Log("start:" + _startVelocity);
                    Debug.Log("diff:" + (_startVelocity - rb.velocity.magnitude));
                    // too slow, speed it up to the minimum speed
                    var velocityAsPercentageOfDesired = rb.velocity.magnitude / _startVelocity;
                    var velocityPercentageShortfall = 1 - velocityAsPercentageOfDesired;
                    // increase by that amount by adding force in same direction of travel
                    Debug.Log("adding:" + ((_startVelocity * velocityPercentageShortfall)));
                    rb.AddForce(rb.velocity.normalized * (_startVelocity * velocityPercentageShortfall), ForceMode2D.Impulse);
                    Debug.Log("now:" + rb.velocity.magnitude);
                }
                velocity = rb.velocity;
            }
        } 
        else if (coll.collider.CompareTag("Paddle"))
        {
            velocity = new Vector2(-velocity.x , velocity.y);
            rb.velocity = velocity;
            rb.AddForce(velocity.normalized * paddleOomph, ForceMode2D.Impulse);
            velocity = rb.velocity;
        }
    }
}
