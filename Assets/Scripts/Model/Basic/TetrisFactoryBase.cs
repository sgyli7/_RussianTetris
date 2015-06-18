using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class TetrisFactoryBase {

	protected Element[,] shape;

    public virtual Tetris create() {
        return null;
    }
    
	protected void str2Shape (string _shapeStr) {
		string [] shapeStrings = _shapeStr.Split ( new char[] {','});
		int size = shapeStrings.Length;
		int width =  shapeStrings[0].Length;
		if (size < 2) {
			Debug.LogError ("Blocks must have at least two lines");
			return;
		}
		if (width != size) {
			Debug.LogError ("Block width and height must be the same");
			return;
		}
		for (int i = 1; i < size; i++) {
			if (shapeStrings[i].Length != shapeStrings[i-1].Length) {
				Debug.LogError ("All lines in the block must be the same length");
				return;
			}
		}
		
		int [,] shapedata = new int[size,size];
		for(int x=0;x<size;x++){
			for(int y=0;y<size;y++){
//				shapedata[x,y] = shapeStrings[x,y] == '1' ? 0:1;
				if (shapeStrings[x][y] == '1'){
					shapedata [x,y] = 1;
				}
				else{
					shapedata[x,y] = 0;
				}
			}
		}
		
		
		
	}

}

public class TetrisFactory_L : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris;
		return tetris;
	}
}

public class TetrisFactory_J : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris;
		return tetris;
	}
}

public class TetrisFactory_T : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris;
		return tetris;
	}
}

public class TetrisFactory_S : TetrisFactoryBase{
	private Tetris tetris;
	public override Tetris create ()
	{
		return tetris;
	}
}

public class TetrisFactory_Z : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris;
		return tetris;
	}
}

public class TetrisFactory_O : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris;
		return tetris;
	}
}

public class TetrisFactory_I : TetrisFactoryBase{
	
	public override Tetris create ()
	{
		Tetris tetris;
		return tetris;
	}
}
