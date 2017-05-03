using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BunnyController : MonoBehaviour {

	private Rigidbody2D myRigidbody;
	private Animator myAnim;
	public float bunnyJumpForce = 500f;
	private float bunnyHurtTime = -1;
	private Collider2D myCollider;
	public Text scoreText;
	private float startTime;
	private int jumpsLeft = 2;
	public AudioSource jumpSfx;
	public AudioSource deathSfx;

	// Use this for initialization
	void Start () {
		myRigidbody = GetComponent<Rigidbody2D>();
		myAnim = GetComponent<Animator> ();
		myCollider = GetComponent<Collider2D> ();

		startTime = Time.time;
	}
	
	// Update is called once per frame
	void Update () {

		if (bunnyHurtTime == -1) {
			if (Input.GetButtonUp ("Jump")&&jumpsLeft > 0) {
				if (myRigidbody.velocity.y < 0) {
					myRigidbody.velocity = Vector2.zero;
				}
				if (jumpsLeft == 1) {
					myRigidbody.AddForce (transform.up * bunnyJumpForce * 0.75f);
				} else {
					myRigidbody.AddForce (transform.up * bunnyJumpForce);
				}
				jumpsLeft--;
				jumpSfx.Play();
			}
			myAnim.SetFloat ("vVelocity", myRigidbody.velocity.y);
			scoreText.text = (Time.time - startTime).ToString ("0.0");
		} else {
			if (Time.time > bunnyHurtTime + 2) {
				SceneManager.LoadScene (SceneManager.GetActiveScene().name, LoadSceneMode.Single);
				bunnyHurtTime = -1;
			}
		}
	}

	void OnCollisionEnter2D(Collision2D collision){
		if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Enemy")) {

			foreach (MoveLeft moveLefter in FindObjectsOfType<MoveLeft>()) {
				moveLefter.enabled = false;
			}

			foreach (PrefabSpawner prefabSpawner in FindObjectsOfType<PrefabSpawner>()) {
				prefabSpawner.enabled = false;
			}

			bunnyHurtTime = Time.time;
			myAnim.SetBool ("bunnyHurt", true);
			myRigidbody.velocity = Vector2.zero;
			myRigidbody.AddForce (transform.up * bunnyJumpForce);
			myCollider.enabled = false;

			deathSfx.Play();
		} else if (collision.collider.gameObject.layer == LayerMask.NameToLayer ("Ground")) {
			jumpsLeft = 2;
		}
	}
}
