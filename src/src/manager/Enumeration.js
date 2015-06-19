/**
 * Created by JasonWu on 6/17/15.
 */

var Operator={};
Operator.DERECT_FALL="derectFall";
Operator.SPEEDUP_START="speedupStart";
Operator.SPEEDUP_END="speedupEnd";
Operator.LEFT="left";
Operator.RIGHT="right";
Operator.TURN="turn";

var TetrisEvent={};
TetrisEvent.CHANGE_POSITION="changePosition";//改变了砖块的位置
TetrisEvent.ATTACH="attach";//当砖块落地后
TetrisEvent.GAME_OVER="gameOver";//游戏结束