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
	protected Hashtable _levelStrategyHashMap;
	protected List<GameRule> _gameRules;
	protected PlayerCredit _playerCredit;
	
	float time = 0.0f;																//Current Time, used to controll Tetris falling
	int score = 0;																	//Player Score, used to Level up
	bool isSpeedup = false;													//Wether in "Speed Up" mode
	bool gameRuleInitCompleted = false;								//Wether Levels Infos were loaded

//    public delegate eventCallback;
	
	/**
     *  Constructor
     */
	public GameManager () {
		_map = new Map ( 10, 20);
		_tetrisList = new List<Tetris> ();	
		_gameRules = new List<GameRule> ();
		_playerCredit = new PlayerCredit ();
		_levelStrategyHashMap = new Hashtable ();
		_levelStrategyHashMap["simple"] = new LevelStrategy_Simple();
		_levelStrategyHashMap["normal"] = new LevelStrategy_Normal();
		_levelStrategyHashMap["advanced"] = new LevelStrategy_Advanced();
		
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
					LevelStrategyBase ls =(LevelStrategyBase)_levelStrategyHashMap[rule.LevelStrategy];
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
			LevelStrategyBase s =(LevelStrategyBase)_levelStrategyHashMap[rule.LevelStrategy];
			_tetrisList.Add(s.execute().create());
		};
			
			
			
		time += Time.deltaTime;
    }


    public void input(Operator op ) {

    }

	protected bool collisionDetection(Tetris tetrs) {

        return false;
    }
    
    
	protected void putTetrisToMap(Tetris tetris) {
		
	}
	
	protected void checkElements () {
	
	}
	
    
}