using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour {
	[SerializeField]
	public Orbit PlayerCamera;

	[SerializeField]
	public float Velocity = 3f;

	[SerializeField, Range(0f,1f)]
	public float JumpForce = 0.5f;

	Rigidbody rb;
	BoxCollider platform;

	void Start () {
		rb = GetComponent<Rigidbody>();
		platform = null;
	}

	void FixedUpdate () {
		Move ();
	}

	void Move() {
		if (Input.GetAxisRaw ("Horizontal") > 0) {
			Vector3 to = Vector3.right * Velocity * Time.deltaTime;
			to.x += transform.position.x;
			to.y += transform.position.y;

			rb.MovePosition (to);

		} else if (Input.GetAxisRaw ("Horizontal") < 0) {
			Vector3 to = Vector3.left * Velocity * Time.deltaTime;
			to.x += transform.position.x;
			to.y += transform.position.y;

			rb.MovePosition (to);
		}

		if (Input.GetButtonDown ("Jump")) {
			if (Physics.Raycast (transform.position, Vector3.down, 1f, 1 << LayerMask.NameToLayer ("Platform")))
				rb.velocity = Vector2.up * JumpForce * 10;
		}

		if (Input.GetKeyDown ("down") && platform)
			ChangeLayer ();

		if (Input.GetKeyDown (KeyCode.F) && !PlayerCamera.isRotating) {
			PlayerCamera.StartRotate (90f, Wise.Clockwise, delegate(Transform t) {
				Debug.Log("SD");
			});
		}
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
			platform = colider.gameObject.GetComponent<BoxCollider> ();
	}
}