using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Map {

	protected int _width;
	protected int _height;
	protected List<Element[]> _elementArrayList;
	
	public int Width {
		get { 
			return _width;
		}
	}
	
	public int Height {
		get { 
			return _height;
		}
	}
		
	public List<Element[]> Elements {
		get { 
			return _elementArrayList;
		}
		set { 
			_elementArrayList = value;
		}
	}
	
    /**
     * Constructor
     */
	public Map(int width, int height) {
		_width = width;
		_height = height;
		
		_elementArrayList = new List<Element[]> ();
		for (int y = 0; y < _height; y++) {
			Element[] elementArray = new Element[_width];
			for (int x = 0; x < _width; x++) {
				elementArray[x] = new Element ();
			}
			_elementArrayList.Add (elementArray);
		}
    }
    
	public int checkElements() {
		int removeCount = 0;
		for (int y = 0; y < _height; y++) {
			Element[] elementArray = new Element[_width];
			elementArray = _elementArrayList[y];
			bool filled = true;
			for (int x = 0; x < _width; x++) {
				if ( elementArray[x].isNull){
					filled = false;
				}
			}
		
			if (filled){
				_elementArrayList.Remove(elementArray);		//Remove filled Elements
				y--; 															// ReCheck the last y postion
				for (int x = 0; x < _width; x++) {
					elementArray[x].isNull = true;
				}
				_elementArrayList.Add(elementArray);			//Put them to the top
				removeCount++;
			}
		}
		Debug.Log ("removeCount:   "+removeCount);
		return removeCount;
	}
	
	public void clearElements () {
		for (int y = 0; y < _height; y++) {
			for (int x = 0; x < _width; x++) {
				_elementArrayList[y][x].isNull = true;
			}
		}
	}

}