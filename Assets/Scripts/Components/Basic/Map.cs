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
				_elementArrayList.Remove(elementArray);
				for (int x = 0; x < _width; x++) {
					elementArray[x].isNull = true;
				}
				_elementArrayList.Add(elementArray);
				removeCount++;
			}
		}
		return removeCount;
	}

}