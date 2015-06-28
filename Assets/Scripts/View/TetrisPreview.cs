using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class TetrisPreview : MonoBehaviour {
	[HideInInspector]
	public Transform TetrisListBack_1,TetrisListBack_2,TetrisListBack_3;
	public GameObject ElementCube;
	public GameObject[,] tetris_1, tetris_2 , tetris_3;
	
	protected int maxSize = 4;
	
	void Awake () {
		TetrisListBack_1 = GameObject.Find("TetrisListBack_1").transform;
		TetrisListBack_2 = GameObject.Find("TetrisListBack_2").transform;
		TetrisListBack_3 = GameObject.Find("TetrisListBack_3").transform;
		tetris_1 = new GameObject[maxSize,maxSize];
		tetris_2 = new GameObject[maxSize,maxSize];
		tetris_3 = new GameObject[maxSize,maxSize];
	}
	// Use this for initialization
	void Start () {
		GameObject tetrisPreviewContainer = GameObject.Find ("Tetris Preview");
		for (int x = 0; x < maxSize; x++) {
			for (int y = 0; y < maxSize; y++) {
				// Instantiate T1 which is the cubes on the top preview
				var t1 =  (GameObject)Instantiate(ElementCube, new Vector3( x + TetrisListBack_1.position.x ,  y + TetrisListBack_1.position.y, 0), Quaternion.identity);
				t1.transform.parent = tetrisPreviewContainer.transform;
				tetris_1[x,y] = t1;
				tetris_1[x,y].GetComponent<MeshRenderer>().enabled = false;
				// Instantiate T2 which is the cubes on the top preview
				var t2 =  (GameObject)Instantiate(ElementCube, new Vector3( x + TetrisListBack_2.position.x ,  y + TetrisListBack_2.position.y, 0), Quaternion.identity);
				t2.transform.parent = tetrisPreviewContainer.transform;
				tetris_2[x,y] = t2;
				tetris_2[x,y].GetComponent<MeshRenderer>().enabled = false;
				// Instantiate T3 which is the cubes on the top preview
				var t3 =  (GameObject)Instantiate(ElementCube, new Vector3( x + TetrisListBack_3.position.x ,  y + TetrisListBack_3.position.y, 0), Quaternion.identity);
				t3.transform.parent = tetrisPreviewContainer.transform;
				tetris_3[x,y] = t3;
				tetris_3[x,y].GetComponent<MeshRenderer>().enabled = false;
			}
		}
	}
	
	public void updateView ( List<Tetris> TetrisList) {
		if (TetrisList.Count > 3) {
				Tetris t1 = TetrisList[0];
				Tetris t2 = TetrisList[1];
				Tetris t3 = TetrisList[2];
				
			for (int x = 0; x < maxSize; x++) {
				for (int y = 0; y <maxSize; y++) {
				
					tetris_1 [x,y].GetComponent<MeshRenderer>().enabled = false;
					if(x<t1.Size && y<t1.Size && t1.Shape [x,y].isNull == false){
						tetris_1 [x,y].GetComponent<MeshRenderer>().enabled = true;
					}
					
					tetris_2 [x,y].GetComponent<MeshRenderer>().enabled = false;
					if(x<t2.Size && y<t2.Size && t2.Shape [x,y].isNull == false){
						tetris_2 [x,y].GetComponent<MeshRenderer>().enabled = true;
					}
					
					tetris_3 [x,y].GetComponent<MeshRenderer>().enabled = false;
					if(x<t3.Size && y<t3.Size && t3.Shape [x,y].isNull == false){
						tetris_3 [x,y].GetComponent<MeshRenderer>().enabled = true;
					}
					
				}
			}
		}
		else{
			for (int x = 0; x < maxSize; x++) {
				for (int y = 0; y <maxSize; y++) {
					tetris_1 [x,y].GetComponent<MeshRenderer>().enabled = false;
					tetris_2 [x,y].GetComponent<MeshRenderer>().enabled = false;
					tetris_3 [x,y].GetComponent<MeshRenderer>().enabled = false;
				}
			}
		}
	}
}
