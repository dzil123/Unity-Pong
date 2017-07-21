using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallRotateTest : MonoBehaviour {
	
	public float m_speed = 1.0f;
	public float m_r_speed = 10.0f;
	
	private Rigidbody2D m_rigidbody;
	
	// Use this for initialization
	void Start () {
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_rigidbody.AddForce(transform.up * m_speed);
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		//Debug.Log(m_rigidbody.velocity.magnitude);
		//transform.position += transform.up * Time.deltaTime * m_speed;
		//Move(m_speed);
		//Rigidbody2D.AddForce(transform.up);
		//transform.Rotate(0, 0, Time.deltaTime * m_r_speed);
	}
	
	Vector3 Move (float distance) {
		Vector3 vector = transform.up * Time.deltaTime * distance;
		transform.position += vector;
		return vector;
	}
}

