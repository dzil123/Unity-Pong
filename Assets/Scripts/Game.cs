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
	
	public Countdown m_countdown;
	
	public float m_middle_angle	= 90f; // 90f is right, 270f is left
	public float m_range_angle	= 75f;
	
	public int m_points_to_win = 5;
	
	private int m_left_score = 0;
	private int m_right_score = 0;
	
	private PaddleController m_paddles;
	
	private bool m_side = true;
	
	private int m_time = 3;
	
	void Awake() {
		m_paddles = new PaddleController(m_left_paddle, m_right_paddle);
	}
	
	void Start () {
		//m_countdown.Start_Countdown(5);
		StartRound(5);
	}
	
	void RestartScene () {
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}
	
	void StartRound () {
		StartRound(m_time);
	}
	
	void StartRound (int time) {
		m_ball.ChangeColor(Color.white);
		Vector3 position = GenerateStartingPosition(m_side);
		//Debug.Log(position.ToString());
		m_ball.transform.position = new Vector2(position.x, position.y);
		m_ball.ChangeAngle(position.z);
		
		m_paddles.Reset();
		m_paddles.SetControl(true);
		
		m_countdown.Start_Countdown(time, Begin);
	}
	
	void Begin () {
		m_ball.ChangeSpeed();
		
		m_paddles.SetControl(true);
	}
	
	void Update () {
		if ( Input.GetKeyDown(KeyCode.Escape) ) {
			Application.Quit();
		}
	}
	
	void FixedUpdate () {
		/*
		Vector3 position = GenerateStartingPosition(true);
		m_ball.transform.position = new Vector2(position.x, position.y);
		m_ball.ChangeRotation(position.z, 0f);
		Debug.Log(position.x.ToString());
		Debug.Log(position.y.ToString());
		Debug.Log(position.z.ToString());
		*/
	}
	
	public Vector3 GenerateStartingPosition (bool side) {
		float x = UnityEngine.Random.Range(-4f, -7f);
		float y = UnityEngine.Random.Range(-4f, 4f);
		float angle = UnityEngine.Random.Range(30f, 45f);
		
		//Debug.Log(x.ToString());
		//Debug.Log(y.ToString());
		//Debug.Log(angle.ToString());
		
		if (UnityEngine.Random.Range(0f, 0.999999f) > 0.5f) {
			angle += 90f;
		}
		
		if (side) {
			//Debug.Log("flipping");
			x *= -1f;
			angle *= -1f;
		}
		
		return new Vector3(x, y, angle);
	} 
	
	public void Win (bool side) {
		
	}
	
	public void TestWin () {
		if ( m_left_score >= m_points_to_win ) {
			Win(false);
		} else if ( m_right_score >= m_points_to_win ){
			Win(true);
		}
	}
	
	public void AddPoint ( bool side ) {
		if ( side ) {
			m_right_score += 1;
		} else { 
			m_left_score += 1;
		}
		m_left_score_text.Set( m_left_score );
		m_right_score_text.Set( m_right_score );
		
		TestWin();
	}
	
	public IEnumerator FunctionTimer(Action func, float delay) {
		yield return new WaitForSeconds(delay);
		func();
	}
	
	public void HitEdge (bool side) { // false = left, right = true
		AddPoint(!side);
		
		m_paddles.SetControl(false);
		
		m_ball.ChangeColor(Color.red);
		m_ball.ChangeSpeed(0);
		
		m_side = side;
		m_time = 3;
		
		//m_countdown.Start_Countdown(3, StartRound);
		
		StartCoroutine(FunctionTimer(StartRound, 1f));
	}
	
	public void HitPaddle (bool side) {	
		// Because the paddle is 1 length long, this float is -1.0 < x < 1.0
		// Is paddle != 1 length long, then correct to get in range
		
		//Debug.Log(side.ToString());
		
		float y_offset = m_paddles.GetYOffset(side, m_ball.transform.position);
		
		float angle = m_middle_angle;
		
		angle += m_range_angle * y_offset * -1f;
		
		angle = Mathf.Clamp(angle, 1f, 179f); // stops wildly random angles
		
		if (side) {
			angle *= -1f;
		}
		
		//Debug.Log(y_offset.ToString());
		//Debug.Log(angle_spread.ToString());
		//Debug.Log(angle.ToString());
		
		m_ball.ChangeRotation(angle, m_ball.m_speed);	
	}
}

public class PaddleController {
	
	public float m_paddle_length = 1f;
	
	private PaddleControl m_left_paddle;
	private PaddleControl m_right_paddle;
	
	public PaddleController(PaddleControl left_paddle, PaddleControl right_paddle) {
		m_left_paddle = left_paddle;
		m_right_paddle = right_paddle;
		Reset();
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
		SetControl(enabled, true);
		SetControl(enabled, false);
	}
	
	public void SetControl (bool enabled, bool side) 
	{
		GetPaddle(side).m_enabled = enabled;
	}
	
	public float GetYOffset(bool side, Vector3 pos) {
		PaddleControl paddle = GetPaddle(side);
		Vector3 offset = paddle.transform.InverseTransformPoint(pos);
		float y_offset = offset.y;
		
		y_offset = Mathf.Clamp(y_offset/m_paddle_length, -1f, 1f); // account for non-1 paddle size, clamp for weird angles
		
		return y_offset;
	}
	
	public float GetYOffset(bool side, float y_pos) {
		return GetYOffset(side, new Vector3(0f, y_pos, 0f));
	}
	
	public void Reset() {
		Reset(true);
		Reset(false);
	}
	
	public void Reset(bool side) {
		GetPaddle(side).Reset();
	}
	
}
