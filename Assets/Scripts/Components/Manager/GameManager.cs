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
		for (int i = 1;i < lineArray.Length;i++)
		{
			var ruleValue = lineArray[i].Split(","[0]);
			GameRule rule = new GameRule (ruleValue[0],ruleValue[1],ruleValue[2]);
//			Debug.Log (ruleValue[0]+":::::"+ruleValue[1]+":::::"+ruleValue[2]);
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

		float speed = isSpeedup ? 1/(rule.Speed*3) : 1/rule.Speed ;

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
					updateElements();
					_currentTetris = null;
				}
			}
			break;
			#endregion
			
			#region SPEEDUP_START
		case Operator.SPEEDUP_START:
			isSpeedup = true;
			break;
			#endregion
			
			#region SPEEDUP_END
		case Operator.SPEEDUP_END:
			isSpeedup = false;
			break;
			#endregion
		
			#region TURN
		case Operator.TURN:
			Tetris tmpTetris = _currentTetris.cloneTetris();
			_currentTetris = rotateAndShiftTetris (tmpTetris);
			break;
			#endregion
		}
    }

	protected bool collisionDetection(Tetris tetris) {

		for(int x=0;x<tetris.Width;x++){
			for(int y=0;y<tetris.Height;y++){
				if (_currentTetris.Shape[x,y].isNull == false){
					//Check bottom boundary
					if (_currentTetris.Postion.y + y < 0) {
						return true;
					}
					//Check left boundary
					if (_currentTetris.Postion.x + x < 0) {
						return true;
					}
					//Check right boudary
					if (_currentTetris.Postion.x + x >= _map.Width) {
						return true;
					}
					// Check Map Elements
					if (_currentTetris.Postion.y + y < _map.Height) {
						Element e = _map.Elements [(int)_currentTetris.Postion.y + y][(int)_currentTetris.Postion.x + x];
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
		
		//Shift Order: Down > Up > Left > Right
		if (collisionDetection(tmpTetris)){
			//Shift Down
			tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x,tmpTetris.Postion.y - 1);
			if (collisionDetection(tmpTetris)){
				tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x,tmpTetris.Postion.y + 1);
				//Shift Up
				tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x,tmpTetris.Postion.y + 1);
				if (collisionDetection(tmpTetris)){
					tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x,tmpTetris.Postion.y - 1);
					//Shift Left
					tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x - 1,tmpTetris.Postion.y );
					if (collisionDetection(tmpTetris)){
						tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x + 1,tmpTetris.Postion.y );
						//Shift Right
						tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x + 1,tmpTetris.Postion.y );
						if (collisionDetection(tmpTetris)){
							tmpTetris.Postion = new Vector2 (tmpTetris.Postion.x - 1,tmpTetris.Postion.y );
							Debug.Log ("TetrisRoate: Can not Rotate");
						}
					}
				}
			}
		}
		return tmpTetris;
	}
	
    
}