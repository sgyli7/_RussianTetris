using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
	
	void standardInput () {
		if (Input.GetKeyDown(KeyCode.LeftArrow)){
			_eventCallback(Operator.LEFT);
		}
		if (Input.GetKeyDown(KeyCode.RightArrow)){
			_eventCallback(Operator.RIGHT);
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
		if(Input.GetKeyUp(KeyCode.Space)){
			_eventCallback(Operator.TURN);
		}
	}
	
	void touchInput () {
	
	}
	
}