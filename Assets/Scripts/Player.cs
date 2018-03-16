using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	[SerializeField]
	public float Velocity = 3f;

	[SerializeField, Range(0f,1f)]
	public float JumpForce = 0.5f;

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
			transform.localScale = new Vector3 (-1, 1, -1);

			Vector2 to = Vector2.right * Velocity * Time.deltaTime;
			transform.Translate (to);
		} else if (Input.GetAxisRaw ("Horizontal") < 0) {
			transform.localScale = new Vector3 (1, 1, -1);


			Vector2 to = Vector2.left * Velocity * Time.deltaTime;
			transform.Translate (to);
		}

		if (Input.GetButtonDown("Jump"))
			rb.velocity = Vector2.up * JumpForce * 10;

		if (Input.GetKeyDown ("down") && platform)
			ChangeLayer ();
	}

	void ChangeLayer()
	{
		transform.Translate (0, 0, -0.125f);
		platform.enabled = false;
	}

	void OnCollisionEnter2D(Collision2D colider)
	{
		if (platform)
		{
			transform.position = new Vector3 (transform.position.x, transform.position.y, colider.transform.position.z);
			platform.enabled = true;
			platform = null;
		}

		if (colider.gameObject.tag == "Platform")
			platform = colider.gameObject.GetComponent<BoxCollider2D> ();
	}
}