using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MapView : MonoBehaviour {
	
	protected GameObject _elementCube;
	protected float _xOffset, _yOffset;
	protected GameObject [,] _elementCubes; //Map Element Objects
	
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
		
	public void createMap ( Map map) {
		
		GameObject mapContainer = GameObject.Find ("Map");
		for (int y = 0; y < map.Height; y++) {
			for (int x = 0; x < map.Width; x++) {
				var cube = (GameObject)Instantiate(_elementCube, new Vector3( x + _xOffset ,  y + _yOffset, 0), Quaternion.identity);
				cube.transform.parent = mapContainer.transform;
				_elementCubes [x,y] = cube;
				_elementCubes [x,y].GetComponent<MeshRenderer>().enabled = false;
			}
		}
	}
	
	public void updateView ( Map map) {
		// Update Map View
		for (int y = 0; y < map.Height; y++) {
			for (int x = 0; x < map.Width; x++) {
				_elementCubes [x,y].GetComponent<MeshRenderer>().enabled = false;
				if(map.Elements [y][x].isNull == false){
					_elementCubes [x,y].GetComponent<MeshRenderer>().enabled = true;
				}
			}
		}
	}
	
	
}
