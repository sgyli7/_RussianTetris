using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TetrisView : MonoBehaviour{

	protected GameObject _elementCube;
	protected float _xOffset, _yOffset;
	protected GameObject [,] _elementCubes;//Map Element Objects
	
	public GameObject ElementCube {
		get { 
			return _elementCube;
		}
		set { 
			_elementCube = value;
		}
	}
	public float X_offset {
		get { 
			return _xOffset;
		}
		set { 
			_xOffset = value;
		}
	}
	public float Y_offset {
		get { 
			return _yOffset;
		}
		set { 
			_yOffset = value;
		}
	}
	public GameObject[,] ElementCubes {
		get { 
			return _elementCubes;
		}
		set { 
			_elementCubes = value;
		}
	}
	
	
	public void updateView (Tetris tetris ) {
		if (tetris != null){
			for (int x = 0; x < tetris.Width; x++) {
				for (int y = 0; y < tetris.Height; y++) {
					int xPosition = x + (int)tetris.Postion.x;
					int yPositon = y+ (int)tetris.Postion.y;
					if(tetris.Shape [x,y].isNull == false){
						if (xPosition < _elementCubes.GetLength(0) && yPositon < _elementCubes.GetLength(1) ){
							_elementCubes [xPosition,yPositon].GetComponent<MeshRenderer>().enabled = true;
						}
					}
					
				}
			}
		}
	}
	
	
}
