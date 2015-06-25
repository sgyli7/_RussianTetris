using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class GameManager {
	
	public int TetrisAmount = 5;												// Amount of Tetris in List
			
	protected Map _map;
	protected Tetris _currentTetris;
	protected List<Tetris> _tetrisList;
	protected Dictionary <String, LevelStrategyBase> _levelStrategyDictionary;
	protected List<GameRule> _gameRules;
	protected PlayerCredit _playerCredit;

	public delegate void TetrisEventHanlder (TetrisEvent e);
	public static event TetrisEventHanlder _eventCallback;
	
	public Map Map {
		get { 
			return _map;
		}
	}
	public Tetris CurrentTetris {
		get { 
			return _currentTetris;
		}
	}
	public List<Tetris> TetrisList {
		get { 
			return _tetrisList;
		}
	}
	public GameRule CurrentRule{
		get { 
			return _gameRules [_playerCredit.level];
		}
	}
	public PlayerCredit PlayerCredit {
		get { 
			return _playerCredit;
		}
	}
	
	
	float time = 0.0f;																//Current Time, used to controll Tetris falling
	int score = 0;																	//Player Score, used to Level up
	bool isSpeedup = false;													//Wether in "Speed Up" mode
	bool gameRuleInitCompleted = false;								//Wether Levels Infos were loaded

	
	/**
     *  Constructor
     */
	public GameManager () {
		_map = new Map ( 10, 20);
		_tetrisList = new List<Tetris> ();	
		_gameRules = new List<GameRule> ();
		_playerCredit = new PlayerCredit ();
		_levelStrategyDictionary = new Dictionary<String , LevelStrategyBase> ();
		_levelStrategyDictionary["simple"] = new LevelStrategy_Simple();
		_levelStrategyDictionary["normal"] = new LevelStrategy_Normal();
		_levelStrategyDictionary["advanced"] = new LevelStrategy_Advanced();
		
		TextAsset txt = Resources.Load ("GameRule",typeof(TextAsset)) as TextAsset;
		string [] lineArray = txt.text.Split ("\r\n"[0]);
		for (int i = 1;i < lineArray.Length-1;i++)
		{
			var ruleValue = lineArray[i].Split(","[0]);
//			Debug.Log (ruleValue[0]+":::::"+ruleValue[1]+":::::"+ruleValue[2]);
			GameRule rule = new GameRule (ruleValue[0],ruleValue[1],ruleValue[2]);
			_gameRules.Add (rule);
		}
		gameRuleInitCompleted = true;
	}
   
    public void step() {
		if(gameRuleInitCompleted==false)
			return;
		
		GameRule rule = _gameRules [_playerCredit.level];
		if (_currentTetris == null){
		
			// if 1st time, Generate Tetris List
			if (_tetrisList.Count == 0){
				for(int i=0;i<5;i++){
					LevelStrategyBase ls =(LevelStrategyBase)_levelStrategyDictionary[rule.LevelStrategy];
					_tetrisList.Add(ls.execute().create());
				}
			}
			
			// get CurrentTetris from List
			_currentTetris = _tetrisList[0];
			_tetrisList.RemoveAt(0);
			
			// Set up CurrentTetris
			float xPos = Mathf.Floor (((float) _map.Width - (float) _currentTetris.Width)/2);
			float yPos = (float) _map.Height;
			_currentTetris.Postion = new Vector2 (xPos,yPos);
			
			//add Another Tetris to List
			LevelStrategyBase s =(LevelStrategyBase)_levelStrategyDictionary[rule.LevelStrategy];
			_tetrisList.Add(s.execute().create());
		};
			
			
		time += Time.deltaTime;

		float speed = isSpeedup ? 1/(rule.Speed*5) : 1/rule.Speed ;

		// check the coliision of _currentTetris
		while (_currentTetris != null  && time > speed){
			_currentTetris.Postion = new Vector2 (_currentTetris.Postion.x,_currentTetris.Postion.y - 1);
			if (collisionDetection(_currentTetris)) {
				_currentTetris.Postion = new Vector2 (_currentTetris.Postion.x,_currentTetris.Postion.y + 1);
				putTetrisToMap (_currentTetris);
				updateElements();
				_currentTetris = null;
			}
			else{
				if ( _eventCallback != null){
					_eventCallback(TetrisEvent.CHANGE_POSITION);
				}
			}

			time -= speed;
		}

    }

    public void input(Operator op ) {
		if (_currentTetris == null )
			return;
		
		switch (op) {
		
			#region LEFT
		case Operator.LEFT:
			_currentTetris.Postion = new Vector2 (_currentTetris.Postion.x  - 1,_currentTetris.Postion.y);
			if ( collisionDetection(_currentTetris)){
				_currentTetris.Postion = new Vector2 (_currentTetris.Postion.x  + 1,_currentTetris.Postion.y);
			}
			else{
				if (_eventCallback != null){
					_eventCallback( TetrisEvent.CHANGE_POSITION);
				}
			}
			break;
			#endregion
		
			#region RIGHT
		case Operator.RIGHT:
			_currentTetris.Postion = new Vector2 (_currentTetris.Postion.x  + 1,_currentTetris.Postion.y);
			if ( collisionDetection(_currentTetris)){
				_currentTetris.Postion = new Vector2 (_currentTetris.Postion.x  - 1,_currentTetris.Postion.y);
			}
			else{
				if (_eventCallback != null){
					_eventCallback( TetrisEvent.CHANGE_POSITION);
				}
			}
			break;
			#endregion
			
			#region DIRECT_FALL
		case Operator.DIRECT_FALL:
			while (_currentTetris != null) {
				_currentTetris.Postion = new Vector2 (_currentTetris.Postion.x,_currentTetris.Postion.y - 1);
				if ( collisionDetection (_currentTetris)){
					_currentTetris.Postion = new Vector2 (_currentTetris.Postion.x,_currentTetris.Postion.y + 1);
					putTetrisToMap (_currentTetris);
					_currentTetris = null;
					if (_eventCallback != null){
						_eventCallback( TetrisEvent.CHANGE_POSITION);
					}
					updateElements();
				}
			}
			break;
			#endregion
			
			#region SPEEDUP_START
		case Operator.SPEEDUP_START:
			isSpeedup = true;
			if (_eventCallback != null){
				_eventCallback( TetrisEvent.CHANGE_POSITION);
			}
			break;
			#endregion
			
			#region SPEEDUP_END
		case Operator.SPEEDUP_END:
			isSpeedup = false;
			if (_eventCallback != null){
				_eventCallback( TetrisEvent.CHANGE_POSITION);
			}
			break;
			#endregion
		
			#region TURN
		case Operator.TURN:
			Tetris tmpTetris = _currentTetris.cloneTetris();
			_currentTetris = rotateAndShiftTetris (tmpTetris);
			if (_eventCallback != null){
				_eventCallback( TetrisEvent.CHANGE_POSITION);
			}
			break;
			#endregion
		}
    }

	protected bool collisionDetection(Tetris tetris) {

		for(int x=0;x<tetris.Width;x++){
			for(int y=0;y<tetris.Height;y++){
				if (tetris.Shape[x,y].isNull == false){
					//Check bottom boundary
					if (tetris.Postion.y + y < 0) {
						return true;
					}
					//Check left boundary
					if (tetris.Postion.x + x < 0) {
						return true;
					}
					//Check right boudary
					if (tetris.Postion.x + x >= _map.Width) {
						return true;
					}
					// Check Map Elements
					if (tetris.Postion.y + y < _map.Height) {
						Element e = _map.Elements [(int)tetris.Postion.y + y][(int)tetris.Postion.x + x];
						if (e != null && e.isNull == false) {
							return true;
						}
					}
				}	
			}
		}
        return false;
    }
        
	protected void putTetrisToMap(Tetris tetris) {
		for(int x=0;x<tetris.Width;x++){
			for(int y=0;y<tetris.Height;y++){
				if (_currentTetris.Shape[x,y].isNull == false){
					//check GameOver
					if (_currentTetris.Postion.y + y >= _map.Height){
						if (_eventCallback != null ) {
							_eventCallback (TetrisEvent.GAME_OVER);
						}
						// Restart game
						_map.clearElements();
						_playerCredit.level = 0;
						_playerCredit.score = 0;
						_tetrisList = null ;
						_tetrisList = new List<Tetris> ();	
					}
					//put Tetris to Map
					else {
						_map.Elements [(int)_currentTetris.Postion.y + y][(int)_currentTetris.Postion.x + x].isNull = false;
					}			
					
				}
			}
		}
	}
	
	protected void updateElements () {
		//checkElements
		int clearCount = _map.checkElements();
		if (clearCount != 0 )
		{
			if (_eventCallback != null ) {
				_eventCallback (TetrisEvent.ATTACH);
			}
		}
		
		//update Player Score
		_playerCredit.score += clearCount * clearCount * 10;
		
		//update Player Level
		GameRule rule = _gameRules [_playerCredit.level];
		if (_playerCredit.score - score > rule.Score) {
			score +=  rule.Score;
			_playerCredit.level ++ ;
		}
		
	}
	
	protected Tetris rotateAndShiftTetris (Tetris tmp) {
		Tetris tmpTetris = tmp;
		//Rotate tmpTetris
		tmpTetris.rotateTetris();
		//Shift tmpTetris in 1 space
		tmpTetris = shiftTetris (tmpTetris, 1);
		return tmpTetris;
	}
	
    
	protected Tetris shiftTetris (Tetris tmpTetris, int shiftCount)
	{
		//Shift Order: Down > Up > Left > Right
		if (collisionDetection (tmpTetris)) {
			//Shift Down
			Debug.Log ("Shift Down" + shiftCount );
			tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x, tmpTetris.Postion.y - shiftCount);
			if (collisionDetection (tmpTetris)) {
				tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x, tmpTetris.Postion.y + shiftCount);
				//Shift Up
				Debug.Log ("Shift Up"+ shiftCount );
				tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x, tmpTetris.Postion.y + shiftCount);
				if (collisionDetection (tmpTetris)) {
					tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x, tmpTetris.Postion.y - shiftCount);
					//Shift Left
					Debug.Log ("Shift Left"+ shiftCount );
					tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x - shiftCount, tmpTetris.Postion.y);
					if (collisionDetection (tmpTetris)) {
						tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x + shiftCount, tmpTetris.Postion.y);
						//Shift Right
						Debug.Log ("Shift Right"+ shiftCount );
						tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x + shiftCount, tmpTetris.Postion.y);
						if (collisionDetection (tmpTetris)) {
							tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x - shiftCount, tmpTetris.Postion.y);
							Debug.Log ("TetrisRoate: Can not Shift in " + shiftCount +" space");
							return shiftTetris (tmpTetris,shiftCount+1); //Shift tmpTetris in +1 space
						}
					}
				}
			}
		}
		return tmpTetris;
	}
}