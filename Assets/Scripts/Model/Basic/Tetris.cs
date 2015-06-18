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
    


}