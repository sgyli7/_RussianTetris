using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Tetris {

	protected int _size; 
	protected Element[,] _shape;
	protected RotateBase _rotateStyle;
	protected Vector2 _position;
	
	public Element[,] Shape{
		get { 
			return _shape;
		}
		set { 
			_shape = value;
		}
	}
	public int Size {
		get {
			return _size;
		}
	}

    /**
     *  Constructor
     */
	public Tetris(int size , RotateBase rotateStyle) {
		_shape = new Element[size,size];
		_rotateStyle = rotateStyle;
		
    }
	
    /**
     * 
     */
    public void rotate() {
        
    }
    


}