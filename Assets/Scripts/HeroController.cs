using UnityEngine;
using System.Collections;

public class HeroController : MonoBehaviour {

    private Animator _animator;
    private float _move;
    private float _jump;

	// Use this for initialization
	void Start () {
        this._animator = gameObject.GetComponent<Animator>();
        this._move = 0f;
        this._jump = 0f;

       
	
	}
	
	// Update is called once per frame
	void Update () {
        this._move = Input.GetAxis("Horizontal");
        this._jump = Input.GetAxis("Vertical");

        //walking
        if (this._move != 0)
        {
            this._animator.SetInteger("Anim_state", 1);
        }

        else { 
        if(this._jump>0)
        {
            this._animator.SetInteger("Anim_state", 2);
        }
        
            //idle
            this._animator.SetInteger("Anim_state", 0);
        }

    }
}
