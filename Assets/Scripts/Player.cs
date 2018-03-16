using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	public float Velocity = 3f;

	[SerializeField, Range(0f,1f)]
	public float JumpForce = 0.5f;

	[SerializeField]
	public Transform JumpCastEnd;

	Rigidbody2D rb;
	BoxCollider2D platform;

	void Start () {
		rb = GetComponent<Rigidbody2D>();
		platform = null;
	}

	void FixedUpdate () {
		Move ();
	}

	void Move() {
		if (Input.GetAxisRaw ("Horizontal") > 0) {
			Vector2 to = Vector2.right * Velocity * Time.deltaTime;
			to.x += transform.position.x;
			to.y += transform.position.y;

			rb.MovePosition (to);

		} else if (Input.GetAxisRaw ("Horizontal") < 0) {
			Vector2 to = Vector2.left * Velocity * Time.deltaTime;
			to.x += transform.position.x;
			to.y += transform.position.y;

			rb.MovePosition (to);
		}

		if (Input.GetButtonDown ("Jump") &&
			Physics2D.Raycast(transform.position, JumpCastEnd.position, 100f, LayerMask.NameToLayer("Platform"))) {

			rb.velocity = Vector2.up * JumpForce * 10;
		}

		if (Input.GetKeyDown ("down") && platform)
			ChangeLayer ();
	}

	void ChangeLayer() {
		transform.Translate (0, 0, -0.125f);
		platform.enabled = false;
	}

	void OnCollisionEnter2D(Collision2D colider) {
		if (platform) {
			transform.position = new Vector3 (transform.position.x, transform.position.y, colider.transform.position.z);
			platform.enabled = true;
			platform = null;
		}

		if (colider.gameObject.tag == "Platform")
			platform = colider.gameObject.GetComponent<BoxCollider2D> ();
	}
}