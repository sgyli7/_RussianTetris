using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class SecneView : MonoBehaviour{

	public GameObject cube;

	protected GameManager _gameManager;
	protected TetrisView _tetrisView;
	protected TetrisPreview _tetrisPreview;


    public void Awake () {
		_gameManager = new GameManager();
	}
		
	public void Start () {
//	gameManager.step();
//		Tetris _currentTetris = gameManager.CurrentTetris;
//		int tSize = _currentTetris.Size;
//		for(int x=0;x<tSize;x++){
//			for(int y=0;y<tSize;y++){
//				if (_currentTetris.Shape[x,y].isNull == false){
//					Instantiate(cube, new Vector3( x + _currentTetris.Postion.x ,  y + _currentTetris.Postion.y, 0), Quaternion.identity);
//				}	
//			}
//		}
    }
    
    public void Update () {
    	_gameManager.step();
    }


    public void onEventCallback (TetrisEvent e) {
    	
    }
	
	public void onTouchCallback(Operator op ) {
		
	}
	
}