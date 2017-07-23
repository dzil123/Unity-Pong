using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {
	
	public bool m_begin_auto = false;
	
	public float m_speed = 1.0f;
	public float m_rotation = 35f;
	
	[HideInInspector]
	public float m_angle;
	
	[HideInInspector]
	public Rigidbody2D m_rigidbody;
	
	void Awake () { // Awake comes before start
		m_rigidbody = GetComponent<Rigidbody2D>();
	}
	
	void Start () {
		if (m_begin_auto) {
			begin();
		}
		
	}
	
	public void begin() {
		ChangeRotation(m_rotation, m_speed);
	}
	
	public void ChangeRotation (float angle, float speed) {
		//m_rigidbody.rotation = angle; //Quaternion.Euler(0, 0, angle);
		//m_rigidbody.AddForce(transform.up * speed);
		//m_rigidbody.rotation = angle;
		
		m_rigidbody.velocity = Vector3.zero;
		
		m_angle = angle;
		
		angle = -1.0f * angle;

		if ( angle < 0f) {
			angle += 360f;
		}
		
		transform.eulerAngles = new Vector3(0, 0, angle);
		
		m_rigidbody.AddForce(transform.up * speed);
	}
}

