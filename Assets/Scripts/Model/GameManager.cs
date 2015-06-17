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
//    public delegate eventCallback;
	void Start () {
		
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