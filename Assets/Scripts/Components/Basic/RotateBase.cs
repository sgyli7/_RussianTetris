using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class RotateBase {

	public virtual int [,]  execute(int [,] shapeIn) {
		return null;
    }

}


public class RotateClassic : RotateBase {

	public override int [,] execute(int [,] shapeIn) {
		int size = shapeIn.GetLength(0);
		int [,] shapeOut = new int[size,size];
		for(int x=0;x<size;x++){
			for(int y=0;y<size;y++){
				shapeOut[x,y]=shapeIn[(size-1)-y,x];
			}
		}

		return shapeOut;
	}

}