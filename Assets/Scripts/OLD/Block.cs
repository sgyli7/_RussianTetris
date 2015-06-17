using UnityEngine;
using System.Collections;

public class Block : MonoBehaviour {
	
	public string[] block;
	
	private bool[,] blockMatrix;
	
	private int size;
	private float halfSize;
	private float halfSizeFloat;
	private float childSize;
	private int xPosition;
	private int yPosition;
	private float fallSpeed;
	private bool drop = false;
	
	// Use this for initialization
	void Start () {
	
		size = block.Length;
		int width = block[0].Length;
		if (size < 2) {
		    print("Blocks must have at least two lines");
		    return;
		}
	    if (width != size) {
		    Debug.LogError ("Block width and height must be the same");
		    return;
	    }
	    if (size > Manager.manager.maxBlockSize) {
		    Debug.LogError ("Blocks must not be larger than " + Manager.manager.maxBlockSize);
		    return;
	    }
	    for (int i = 1; i < size; i++) {
		     if (block[i].Length != block[i-1].Length) {
			     Debug.LogError ("All lines in the block must be the same length");
			     return;
		     }
	    }
		
		halfSize = (size + 1) * .5f;
		childSize = (size - 1) * .5f;
		halfSizeFloat = size * .5f;
		
		blockMatrix = new bool[size, size];
		for(int y=0;y<size;y++){
			for(int x=0;x<size;x++){
				if (block[y][x] == '1'){
				
					blockMatrix[y, x] = true;
			    	var cube = (Transform)Instantiate(Manager.manager.cube, new Vector3(x - childSize, childSize - y, 0), Quaternion.identity);
			    	cube.parent = transform;
					
				}
			}
		}
		
		yPosition = Manager.manager.GetFieldHeight() - 1;
		transform.position = new Vector3( Manager.manager.GetFieldWidth() / 2 + (size % 2 == 0 ? 0.5f : 0), yPosition - childSize, 0);
		xPosition = (int)(transform.position.x - childSize);
		fallSpeed = Manager.manager.blockNormalFallSpeed;
		
		if (Manager.manager.CheckBlock(blockMatrix, xPosition, yPosition)){
			Manager.manager.GameOver();
			return;
		}
		
		StartCoroutine(CheckInput());
		StartCoroutine(Delay((1 / Manager.manager.blockNormalFallSpeed) * 2));
		StartCoroutine(Fall());
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	IEnumerator Delay(float time){
		var t = 0f;
		while (t <= time && !drop){
			t += Time.deltaTime;
			yield return null;
		}
	}
	
	IEnumerator Fall(){
		while(true){
			yPosition--;
			if (Manager.manager.CheckBlock(blockMatrix, xPosition, yPosition)){
				Manager.manager.SetBlock(blockMatrix, xPosition, yPosition + 1);
				Destroy(gameObject);
				break;
			}
			
			for (float i = yPosition + 1;i > yPosition;i -= Time.deltaTime * fallSpeed){
				transform.position = new Vector3(transform.position.x, i - childSize, transform.position.z);
				/*foreach(Transform child in transform){
					print(child.transform.position);
				}*/
				yield return null;
			}
			
		}
	}
	
	IEnumerator MoveHorizontal(int distance){
		
		if (!Manager.manager.CheckBlock(blockMatrix, xPosition + distance, yPosition)){
			transform.position = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
			xPosition += distance;
			yield return new WaitForSeconds(.1f);
		}
		
	}
	
	void RotateBlock(){
		
		var tempMatrix = new bool[size, size];
		/*
		for (int y = 0;y < size;y++){
			for (int x = 0;x < size;x++){
				tempMatrix[y, x] = blockMatrix[x, y];
				print(tempMatrix[y, x] + " ");
			}
		    print("\n");
		}*/
		
	    for (int y = 0; y < size; y++) {
		     for (int x = 0; x < size; x++) {
		          tempMatrix[y, x] = blockMatrix[x, (size-1)-y];
	         }
		}
		
		if (!Manager.manager.CheckBlock(tempMatrix, xPosition, yPosition)){
			System.Array.Copy(tempMatrix, blockMatrix, size * size);
			transform.Rotate(0, 0, 90);
		}
	}
	
	IEnumerator CheckInput(){
		
		while(true){
			var input = Input.GetAxisRaw("Horizontal");
			if (input < 0){
				yield return StartCoroutine(MoveHorizontal(-1));
			}
			
			if (input > 0){
				yield return StartCoroutine(MoveHorizontal(1));
			}
			
			if (Input.GetKeyDown(KeyCode.UpArrow)){
				RotateBlock();
			}
			
			if (Input.GetKeyDown(KeyCode.DownArrow)){
				fallSpeed = Manager.manager.blockDropSpeed;
				drop = true;
				//break;
			}
			
			if (Input.GetKeyUp("space")){
				fallSpeed = Manager.manager.blockNormalFallSpeed;
				drop = false;
				//break;
			}
			
			yield return null;
		}
		
	}
	
}
