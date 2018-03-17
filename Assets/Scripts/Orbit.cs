using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Wise { Clockwise, Anticlockwise }
public delegate void Finish(Transform t);

public class Orbit : MonoBehaviour {

	[SerializeField]
	Transform Target;

	[SerializeField]
	float AngularSpeed = 3f;

	[SerializeField]
	float SmoothAngular = 1f;

	public bool isRotating = false;

	void Start () {
		isRotating = false;
	}

	public void StartRotate(float increase, Wise wise, Finish handler) {
		if(!isRotating)
			this.StartCoroutine (Rotate (increase, wise, handler));
	}

	public IEnumerator Rotate(float increase, Wise wise, Finish handler) {
		isRotating = true;

		increase -= 1f;
		Vector2 dir = wise == Wise.Clockwise ? Vector2.up : Vector2.down;

		while (increase > Target.rotation.eulerAngles.y) {
			float angularLerp = Mathf.Lerp(AngularSpeed, 0, Time.deltaTime * 0.2f);

			transform.RotateAround (Target.position, dir, AngularSpeed);
			increase -= AngularSpeed;
			yield return new WaitForFixedUpdate ();
		}

		isRotating = false;
		handler (transform);
	}
}

