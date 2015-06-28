using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public abstract class LevelStrategyBase {

	public virtual TetrisFactoryBase execute() {
		return null;
	}

}

public class LevelStrategy_Simple :  LevelStrategyBase{

	public override TetrisFactoryBase execute() {
		float rand = UnityEngine.Random.Range ( 0.0f, 1.0f);
		if (rand < 0.2) {
			return new TetrisFactory_L();
		} else if (rand < 0.2) {
			return new TetrisFactory_J();
		} else if (rand < 0.3) {
			return new TetrisFactory_T();
		} else if (rand < 0.4) {
			return new TetrisFactory_S();
		} else if (rand < 0.5) {
			return new TetrisFactory_Z();
		} else if (rand < 0.7) {
			return new TetrisFactory_O();
//		}else if (rand <0.8) {
//			return new TetrisFactory_X();		
		} else {
			return new TetrisFactory_I();
		}
	}
	
}

public class LevelStrategy_Normal :  LevelStrategyBase{
	
	public override TetrisFactoryBase execute() {
		float rand = UnityEngine.Random.Range ( 0.0f, 0.8f);
		if (rand < 0.1) {
			return new TetrisFactory_L();
		} else if (rand < 0.2) {
			return new TetrisFactory_J();
		} else if (rand < 0.3) {
			return new TetrisFactory_T();
		} else if (rand < 0.4) {
			return new TetrisFactory_S();
		} else if (rand < 0.5) {
			return new TetrisFactory_Z();
		} else if (rand < 0.7) {
			return new TetrisFactory_O();
		} else {
			return new TetrisFactory_I();
		}
	}
	
}

public class LevelStrategy_Advanced :  LevelStrategyBase{
	
	public override TetrisFactoryBase execute() {
		float rand = UnityEngine.Random.Range ( 0.0f, 1.0f);
		if (rand < 0.2) {
			return new TetrisFactory_L();
		} else if (rand < 0.4) {
			return new TetrisFactory_J();
		} else if (rand < 0.6) {
			return new TetrisFactory_T();
		} else if (rand < 0.75) {
			return new TetrisFactory_S();
		} else if (rand < 0.9) {
			return new TetrisFactory_Z();
		} else if (rand < 0.95) {
			return new TetrisFactory_O();
		} else {
			return new TetrisFactory_I();
		}
	}
	
}

