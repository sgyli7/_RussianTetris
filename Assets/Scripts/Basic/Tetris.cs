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
	protected RotateBase _rotate;
	protected Vector2 _position;
	
	public int Size {
		get {
			return _size;
		}
	}
	
	public int Width {
		get {
			return _size;
		}
	}
	
	public int Height {
		get {
			return _size;
		}
	}
	
	public Element[,] Shape{
		get { 
			return _shape;
		}
		set { 
			_shape = value;
		}
	}
	public Vector2 Postion {
		get { 
			return _position;
		}
		set { 
			_position = value;
		}
	}
	

    /**
     *  Constructor
     */
	public Tetris(int size , RotateBase rotate) {
		_size =size;
		_shape = new Element[size,size];
		for (int x = 0; x < size; x++) {
			for (int y = 0; y < size; y++) {
			_shape [x,y] = new Element ();
			}
		}
		_rotate = rotate;
		
    }
	
    /**
     * 
     */
	public void rotateTetris() {
		int [,] shapeIn = new int[_size,_size];
		for (int x = 0; x < _size; x++) {
			for (int y = 0; y < _size; y++) {
				shapeIn [x,y] = _shape[x,y].isNull ? 0 : 1;
			}
		}
		int [,] shapeOut = _rotate.execute(shapeIn);
		
		for (int x = 0; x < _size; x++) {
			for (int y = 0; y < _size; y++) {
				_shape [x,y].isNull = shapeOut [x,y] == 0;
			}
		}
    }
    
    
	public Tetris cloneTetris () {
		Tetris t = new Tetris (_size,_rotate);
		t.Shape = _shape;
		t._position = _position;
		return t;
    }
    


}