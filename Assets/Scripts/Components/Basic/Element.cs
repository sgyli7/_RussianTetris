using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class Element {

    public Color color;
	public bool isNull;
    
	public Element ( ) {
		this.color = Color.white;
		this.isNull = true;
	}
    
    public Element ( Color color , bool isNull ) {
    this.color = color;
    this.isNull = isNull;
    }
    
}