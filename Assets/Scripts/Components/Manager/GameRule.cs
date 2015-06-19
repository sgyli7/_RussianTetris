using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class GameRule {

    public float Speed;
    public int Score;
    public string LevelStrategy;

	public GameRule() {
		
		Speed = 0;
		Score = 0;
		LevelStrategy = "";
		
	}
	
	public GameRule(float speed, int score, string levelStragegy) {
	
		Speed = speed;
		Score = score;
		LevelStrategy = levelStragegy;
	
	}
	
	public GameRule(string speed, string score, string levelStragegy) {
		
		Speed = (float)Convert.ToDouble (speed);
		Score = Convert.ToInt32 (score);
		LevelStrategy = levelStragegy;
		
	}
	
}