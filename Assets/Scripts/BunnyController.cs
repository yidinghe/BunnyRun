using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BunnyController : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	private Animator myAnim;
	public float bunnyJumpForce = 500f;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Jump")) {
			myRigidbody.AddForce (transform.up*bunnyJumpForce);
		}

		myAnim.SetFloat ("vVelocity", myRigidbody.velocity.y);
	}

	void OnCollisionEnter2D(Collision2D collision){
		if(collision.collider.gameObject.layer == LayerMask.NameToLayer("Enemy")){
			SceneManager.LoadScene (SceneManager.GetActiveScene().name, LoadSceneMode.Single);
		}
	}
}
