using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class TetrisFactoryBase {


    public virtual Tetris create() {
        return null;
    }
    
	protected int[,] str2Shape (string _shapeStr) {
		string [] shapeStrings = _shapeStr.Split ( new char[] {','});
		int size = shapeStrings.Length;
		int width =  shapeStrings[0].Length;
		if (size < 2) {
			Debug.LogError ("Blocks must have at least two lines");
		}
		if (width != size) {
			Debug.LogError ("Block width and height must be the same");
		}
		for (int i = 1; i < size; i++) {
			if (shapeStrings[i].Length != shapeStrings[i-1].Length) {
				Debug.LogError ("All lines in the block must be the same length");
			}
		}

		int[,] shapeData = new int[size,size];

		for(int x=0;x<size;x++){
			for(int y=0;y<size;y++){
				shapeData [x,y] = shapeStrings[x][y]=='1' ? 1:0;
			}
		}

		return shapeData;
		
	}

	protected void putShape (ref Tetris tetris, int[,]shape) {
		int width = shape.GetLength (0);
		int height = shape.GetLength (1);
		for (int x = 0; x < width; x++) {
			for (int y = 0; y < height; y++) {
				tetris.Shape [x,y].isNull = shape [x,y] != 0;
			}
		}

	}

}

public class TetrisFactory_L : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris = new Tetris (3,new RotateClassic()) ;
		string shape = "010,010,011";
		this.putShape (ref tetris, this.str2Shape (shape));
		return tetris;
	}
}

public class TetrisFactory_J : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris = new Tetris (3,new RotateClassic()) ;
		return tetris;
	}
}

public class TetrisFactory_T : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris = new Tetris (3,new RotateClassic()) ;
		return tetris;
	}
}

public class TetrisFactory_S : TetrisFactoryBase{

	public override Tetris create ()
	{
		Tetris tetris = new Tetris (3,new RotateClassic()) ;
		return tetris;
	}
}

public class TetrisFactory_Z : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris = new Tetris (3,new RotateClassic()) ;
		return tetris;
	}
}

public class TetrisFactory_O : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris = new Tetris (2,new RotateClassic()) ;
		return tetris;
	}
}

public class TetrisFactory_I : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris = new Tetris (4,new RotateClassic()) ;
		return tetris;
	}
}
