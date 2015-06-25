using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent(typeof(MapView),typeof(TetrisView),typeof(InputLayer))]
public class GameSecne : MonoBehaviour {
	
	public GameObject elementCube;
	public float X_offset, Y_offset;
	public GameObject [,] elementCubes;
	
	protected GameManager _gameManager;
	protected MapView _mapView;
	protected TetrisView _tetrisView;
	protected InputLayer _inputLayer;
	public TetrisPreview _tetrisPreview;
	public Text _scoreText , _levelText , _gameoverText;
	
	protected bool isPlaying = true;

	
	void Awake() {
		_gameManager = new GameManager();
		GameManager._eventCallback += onEventCallback;
		InputLayer._eventCallback += onInputEventCallback;
		
		elementCubes = new GameObject[_gameManager.Map.Width,_gameManager.Map.Height];
		// set up MapView
		_mapView = GetComponent<MapView>();
		if (_mapView != null) {
			_mapView.X_offset = X_offset;
			_mapView.Y_offset = Y_offset;
			_mapView.ElementCube = elementCube;
			_mapView.ElementCubes = elementCubes;
		}
		// set up TetrisView
		_tetrisView = GetComponent<TetrisView>();
		if (_tetrisView !=null) {
			_tetrisView.X_offset = X_offset;
			_tetrisView.Y_offset = Y_offset;
			_tetrisView.ElementCube = elementCube;
			_tetrisView.ElementCubes = elementCubes;
		}
		// set up TouchLayer
		_inputLayer = GetComponent<InputLayer>();
		
		// set up Tetris Preview
		_tetrisPreview = GetComponent<TetrisPreview>();
		
		// set up UI Texts
		_scoreText = GameObject.Find("Score").GetComponent<Text>();
		_levelText = GameObject.Find("Level").GetComponent<Text>();
		_gameoverText = GameObject.Find("GameoverLabel").GetComponent<Text>();
	}
	
	void Start () {
		_mapView.createMap(_gameManager.Map);
		_gameoverText.enabled = false;
	}
	
	void Update () {
		if (isPlaying){
			_gameManager.step();
			_scoreText.text = _gameManager.PlayerCredit.score.ToString();
			_levelText.text = _gameManager.PlayerCredit.level.ToString();
		}
	}
	
	public void onEventCallback (TetrisEvent e) {
		_mapView.updateView(_gameManager.Map);
		_tetrisView.updateView(_gameManager.CurrentTetris);
		_tetrisPreview.updateView(_gameManager.TetrisList);
		
		switch (e) {
		case TetrisEvent.ATTACH:
			break;
		case TetrisEvent.CHANGE_POSITION:
			break;
		case TetrisEvent.GAME_OVER:
			isPlaying = false;
			Debug.Log ("GAME OVER !!!!!!!!!!!!!!!!");
			_gameoverText.enabled = true;
			Invoke("restartGame",1f);
			break;
		}
	}
	
	public void onInputEventCallback (Operator op) {
	
		_gameManager.input(op);
	
		switch (op) {
		case Operator.LEFT:
			break;
		case Operator.RIGHT:
			break;
		case Operator.DIRECT_FALL:
			break;
		case Operator.SPEEDUP_START:
			break;
		case Operator.SPEEDUP_END:
			break;
		case Operator.TURN:
			break;
		}
	}
	
	void restartGame () {
		_gameoverText.enabled = false;
		isPlaying = true;
	}
}
