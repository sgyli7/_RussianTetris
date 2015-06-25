using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

public class InputLayer : MonoBehaviour  {

	public delegate void InputEventHanlder (Operator op);
	public static event InputEventHanlder _eventCallback;
	
	void Update () {
		#if UNITY_STANDALONE
		standardInput();
		#endif
		
		#if UNITY_ANDROID
		touchInput();
		#endif
		
		#if UNITY_IOS
		touchInput();
		#endif
	}
	
	protected void standardInput () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			StartCoroutine("moveLeft");
		}
		if (Input.GetKeyUp(KeyCode.LeftArrow)){
			StopCoroutine("moveLeft");
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)){
			StartCoroutine("moveRight");
		}
		if (Input.GetKeyUp(KeyCode.RightArrow)){
			StopCoroutine("moveRight");
		}
		if (Input.GetKeyDown(KeyCode.UpArrow)){
			_eventCallback(Operator.DIRECT_FALL);
		}
		if (Input.GetKeyDown(KeyCode.DownArrow)){
			_eventCallback(Operator.SPEEDUP_START);
		}
		if (Input.GetKeyUp(KeyCode.DownArrow)){
			_eventCallback(Operator.SPEEDUP_END);
		}
		if(Input.GetKeyDown(KeyCode.Space)){
			_eventCallback(Operator.TURN);
		}
	}
	
	protected void touchInput () {
		
	}
	
	IEnumerator moveLeft () {
		while (true){
			_eventCallback(Operator.LEFT);
			yield return new WaitForSeconds (0.2f);
		}
	}
	
	IEnumerator moveRight () {
		while (true){
			_eventCallback(Operator.RIGHT);
			yield return new WaitForSeconds (0.2f);
		}
	}
	
	#region UI controls 
	
	public void onLeftButtonDown () {
		StartCoroutine("moveLeft");
	}
	public void onLeftButtonUp () {
		StopCoroutine("moveLeft");
	}
	public void onRightButtonDown () {
		StartCoroutine("moveRight");
	}
	public void onRightButtonUp () {
		StopCoroutine("moveRight");
	}
	public void onTurnButtonClick () {
		_eventCallback(Operator.TURN);
	}
	public void onDownButtonDown () {
		_eventCallback(Operator.SPEEDUP_START);
		StartCoroutine("directDown");
	}
	public void onDownButtonUp () {
		_eventCallback(Operator.SPEEDUP_END);
		StopCoroutine("directDown");
	}
	
	IEnumerator directDown () {
		yield return new WaitForSeconds (1f);
		_eventCallback(Operator.DIRECT_FALL);
	}
	
	#endregion
	
}