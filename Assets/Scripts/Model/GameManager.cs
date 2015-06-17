using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/**
 * 
 */
public class GameManager : MonoBehaviour{
	TetrisFactoryBase  TetrisFactory;

	public GameObject cube;
//    public delegate eventCallback;
	void Start () {
		TetrisFactory = new TetrisFactory_L ();
		Tetris t = TetrisFactory.create ();
	}

    /**
     * 
     */
    public void step() {
    }

    /**
     * @param operator
     */
    public void input(Operator op ) {

    }

    /**
     * @param tetrs 
     * @return
     */
    private bool collisionDetection(Tetris tetrs) {

        return false;
    }

}