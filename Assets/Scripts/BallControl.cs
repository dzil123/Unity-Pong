using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour {
	
	public bool m_begin_auto = false;
	
	public float m_speed = 1.0f;
	
	[HideInInspector]
	public float m_rotation = 35f;
	
	[HideInInspector]
	public float m_angle;
	
	private Rigidbody2D m_rigidbody;
	
	private SpriteRenderer m_spriterenderer;
	
	void Awake () { // Awake comes before start
		m_rigidbody = GetComponent<Rigidbody2D>();
		m_spriterenderer = GetComponent<SpriteRenderer>();
	}
	
	void Start () {
		if (m_begin_auto) {
			Begin();
		}
		
	}
	
	public void Begin() {
		ChangeRotation(m_rotation, m_speed);
	}
	
	public void ChangeColor(Color color) {
		m_spriterenderer.color = color;
	}
	
	public void ChangeSpeed() {
		ChangeSpeed(m_speed);
	}
	
	public void ChangeSpeed (float speed) {
		m_rigidbody.velocity = Vector3.zero;
		m_rigidbody.AddForce(transform.up * speed);
	}
	
	public void ChangeAngle (float angle) {
		//m_rigidbody.rotation = angle; //Quaternion.Euler(0, 0, angle);
		//m_rigidbody.AddForce(transform.up * speed);
		//m_rigidbody.rotation = angle;
		
		m_angle = angle;
		
		angle = -1.0f * angle;

		if ( angle < 0f) {
			angle += 360f;
		}
		
		transform.eulerAngles = new Vector3(0, 0, angle);
	}
	
	public void ChangeRotation(float angle, float speed) {
		ChangeAngle(angle);
		ChangeSpeed(speed);
	}
	
	public void StopRotation() {
		//m_rigid
	}
	
}

