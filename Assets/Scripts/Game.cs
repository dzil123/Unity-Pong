using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
	
	public BallControl m_ball;
	
	public PaddleControl m_left_paddle;
	public PaddleControl m_right_paddle;
	
	public SetText m_left_score_text;
	public SetText m_right_score_text;
	
	public float m_middle_angle	= 90f; // 90f is right, 270f is left
	public float m_range_angle	= 75f;
	
	private int m_left_score = 0;
	private int m_right_score = 0;
	
	private float m_paddle_length = 1f; // change if scale or edge changes for paddle
	
	void RestartScene () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	
	void RestartRound () {
		// idk
	}
	
	void Start () {
		SetControl(true);
		m_ball.begin();
	}
	
	void Update () {
		if ( Input.GetKeyDown(KeyCode.Escape) ) {
			Application.Quit();
		}
		UpdateScore();
		//Debug.Log(m_left_paddle.transform.InverseTransformPoint(m_ball.transform.position).y.ToString());
	}
	
	void FixedUpdate () {
	}
	
	public void UpdateScore () {
		m_left_score_text.Set( m_left_score );
		m_right_score_text.Set( m_right_score );
	}
	
	public void AddPoint ( bool side ) {
		if ( side ) {
			m_right_score += 1;
		} else { 
			m_left_score += 1;
		}
	}
	
	public PaddleControl GetPaddle(bool side) {
		if ( side ) 
		{
			//Debug.Log("right");
			return m_right_paddle;
		} 
		else 
		{
			//Debug.Log("left");
			return m_left_paddle;
		}
	}
	
	public void SetControl (bool enabled) {
		m_left_paddle.m_enabled = enabled;
		m_right_paddle.m_enabled = enabled;
	}
	
	public void SetControl (bool enabled, bool side) 
	{
		GetPaddle(side).m_enabled = enabled;
	}
	
	public IEnumerator FunctionTimer(Action func, float delay) {
		yield return new WaitForSeconds(delay);
		func();
	}
	
	public void HitEdge (bool side) { // false = left, right = true
		AddPoint(!side);
		
		SetControl(false);
		
		m_ball.GetComponent<SpriteRenderer>().color = Color.red;
		m_ball.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
		
		StartCoroutine(FunctionTimer(RestartScene, 2f));
	}
	
	public void HitPaddle (bool side) {	
		// Because the paddle is 1 length long, this float is -1.0 < x < 1.0
		// Is paddle != 1 length long, then correct to get in range
		
		//Debug.Log(side.ToString());
		
		float y_offset = (float)GetPaddle(side).transform.InverseTransformPoint(m_ball.transform.position).y;
		y_offset = Mathf.Clamp(y_offset/m_paddle_length, -1f, 1f); // account for non-1 paddle size, clamp for weird angles
		
		float angle = m_middle_angle;
		
		angle += m_range_angle * y_offset * -1f;
		
		if (side) {
			angle *= -1f;
		}
		
		//Debug.Log(y_offset.ToString());
		//Debug.Log(angle_spread.ToString());
		//Debug.Log(angle.ToString());
		
		m_ball.ChangeRotation(angle, m_ball.m_speed);	
	}
	
}
