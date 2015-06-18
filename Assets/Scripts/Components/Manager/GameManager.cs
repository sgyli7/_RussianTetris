using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class GameManager : MonoBehaviour{

	protected TetrisFactoryBase  _tetrisFactory;
	protected Map _map;
	protected Tetris _currentTetris;

	public GameObject cube;
//    public delegate eventCallback;


	void Start () {
//		_tetrisFactory = new TetrisFactory_I ();
//		_currentTetris = _tetrisFactory.create ();
//		_currentTetris.rotateTetris();
//		int tSize = _currentTetris.Size;
//		for(int x=0;x<tSize;x++){
//			for(int y=0;y<tSize;y++){
//				if (_currentTetris.Shape[x,y].isNull == false){
//					Instantiate(cube, new Vector3( x,  y, 0), Quaternion.identity);
//				}	
//			}
//		}
		_map = new Map ( 10, 20);
//		for(int y=0;y<20;y++){
//			for(int x=0;x<10;x++){
//					if (!_map.Elements[y][x].isNull ){
//						Instantiate(cube, new Vector3( x, y, 0), Quaternion.identity);
//					}	
//				}
//			}
	
	}
	
//	void Update () {
//		if (Input.GetKeyDown(KeyCode.A)){
//			_map.checkElements();
//			for(int y=0;y<20;y++){
//				for(int x=0;x<10;x++){
//					if (_map.Elements[y][x].isNull ){
//						Instantiate(cube, new Vector3( x + 20, y, 0), Quaternion.identity);
//					}	
//				}
//			}
//		}
//	}

    
    public void step() {
    }


    public void input(Operator op ) {

    }

    private bool collisionDetection(Tetris tetrs) {

        return false;
    }

}