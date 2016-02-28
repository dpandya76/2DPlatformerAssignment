using UnityEngine;
using System.Collections;


//velocity range utility class
[System.Serializable]
public class VelocityRange
{
    public float minimum;
    public float maximum;

    //constructor

    public VelocityRange(float minimum, float maximum)
    {
        this.minimum = minimum;
        this.maximum = maximum;

    }
}

public class HeroController : MonoBehaviour {
    //public instance variables
    public VelocityRange velocityRange;
    public float moveForce;
    public float jumpForce;
    public Transform groundCheck;
    public Transform Camera;
    public GameController gameController;

    // Private instance variables
    private Animator _animator;
    private float _move;
    private float _jump;
    private bool _facingRight;
    private Transform _transform;
    private Rigidbody2D _rigidBody2D;
    private bool _isGrounded;

	// Use this for initialization
	void Start () {
        this.velocityRange = new VelocityRange(300f, 5000f);
 

        this._transform = gameObject.GetComponent<Transform>();
        this._animator = gameObject.GetComponent<Animator>();
        this._rigidBody2D = gameObject.GetComponent<Rigidbody2D>();
        this._move = 0f;
        this._jump = 0f;
        this._facingRight = true;
        //the hero placed in start postion
        this.spawn();
        
       
	
	}
	

	// Update is called once per frame
	void FixedUpdate () {

        Vector3 currentPosition = new Vector3(this._transform.position.x, this._transform.position.y, -10f);
        this.Camera.position = currentPosition;
        

        this._isGrounded = Physics2D.Linecast(this._transform.position,
                                                this.groundCheck.position,
                                                    1<<LayerMask.NameToLayer("Ground"));

        Debug.DrawLine(this._transform.position, this.groundCheck.position);


        float forceX = 0f;
        float forceY = 0f;

        //absolute vaue for velocity
        float absValX = Mathf.Abs(this._rigidBody2D.velocity.x);
        float absValY = Mathf.Abs(this._rigidBody2D.velocity.y);

       //ensure if grounded
        if (this._isGrounded)
        {
            // gets a number between -1 to 1 for both Horizontal and Vertical Axes
            this._move = Input.GetAxis("Horizontal");
            this._jump = Input.GetAxis("Vertical");

            if (this._move != 0)
            {
                if (this._move > 0)
                {
                    // movement force
                    if (absValX < this.velocityRange.maximum)
                    {
                        forceX = this.moveForce;
                    }
                    this._facingRight = true;
                    this._flip();
                }
                if (this._move < 0)
                {
                    // movement force
                    if (absValX < this.velocityRange.maximum)
                    {
                        forceX = -this.moveForce;
                    }
                    this._facingRight = false;
                    this._flip();
                }

                // call the walk clip
                this._animator.SetInteger("Anim_state", 1);
            }
            else {

                // set default animation state to "idle"
                this._animator.SetInteger("Anim_state", 0);
            }

            if (this._jump > 0)
            {
                // jump force
                if (absValY < this.velocityRange.maximum)
                {
                    //this._jumpSound.Play();
                    forceY = this.jumpForce;
                }

            }
        }
        else {
            // call the "jump" clip
            this._animator.SetInteger("Anim_state", 2);
        }

        // Apply the forces to the player
        this._rigidBody2D.AddForce(new Vector2(forceX, forceY));
    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Gas"))
        {
            this.gameController.ScoreValue += 10;
            Destroy(other.gameObject);
        }



        if (other.gameObject.CompareTag("Death"))
        {
            this.spawn();
            this.gameController.LivesValue--;
        }
    }

    //private methods
    private void _flip()
    {
        if (this._facingRight)
        {
            this._transform.localScale= new Vector2(1, 1);
        }
        else
        {
            this._transform.localScale = new Vector2(-1, 1);
        }
    }

    private void spawn()
    {
        this._transform.position = new Vector3(-240f, 60f, 0);

    }
}
