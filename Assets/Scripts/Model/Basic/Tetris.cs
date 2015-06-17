using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Tetris {
	
	protected Element[,] _shape;
	protected Vector2 _position;
	
	protected string _shapeStr;
	protected RotateBase _rotateStyle;
	protected int _size; 
	
	public Element[,] shape{
		get;set;
	}
    /**
     * 
     */
	public Tetris(string shapeStr , RotateBase rotateStyle) {
		_shapeStr = shapeStr;
		_rotateStyle = rotateStyle;
		
    }
	
    /**
     * 
     */
    public void rotate() {
        
    }
    
	private void str2Shape (string _shapeStr) {
		string [] shapeStrings = _shapeStr.Split ( new char[] {','});
		_size = shapeStrings.Length;
		int width =  shapeStrings[0].Length;
		if (_size < 2) {
			Debug.LogError ("Blocks must have at least two lines");
			return;
		}
		if (width != _size) {
			Debug.LogError ("Block width and height must be the same");
			return;
		}
		for (int i = 1; i < _size; i++) {
			if (shapeStrings[i].Length != shapeStrings[i-1].Length) {
				Debug.LogError ("All lines in the block must be the same length");
				return;
			}
		}
		
		shape = new Element[_size , _size];
		for(int y=0;y<_size;y++){
			for(int x=0;x<_size;x++){
				
			}
		}
		
				
    }

}