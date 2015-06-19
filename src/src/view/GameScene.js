/**
 * Created by JasonWu on 6/17/15.
 */

var GameLayer = cc.Layer.extend({

    gameManager:null,

    ctor:function () {
        //////////////////////////////
        // 1. super init first
        this._super();
        this.gameManager=new GameManager();


        var me=this;


        this.gameManager.eventCallback=function(operator){

            //cc.log(operator);

            var map=me.gameManager.map;
            var tetris=me.gameManager.currentTetris;

            var strList=[];

            for(var y=map.height-1;y>=0;y--){
                var str="";
                for(var x=0;x<map.width;x++){
                    str+=map.map[y][x].isNull?"   ":"000";
                }
                strList.push(str);
            }


            for(var x=0;x<tetris.width;x++)
            {
                for(var y=0;y<tetris.height;y++)
                {
                    if(tetris.shape[x][y].isNull==false)
                    {
                        var posX=tetris.position.x+x;
                        var posY=tetris.position.y+y;

                        if(posY>=strList.length)
                            continue;

                        posY=(strList.length-1)-posY;//翻转方向

                        var str=strList[posY];
                        var newStr=str.substr(0,(posX)*3)+"000"+str.substr((posX)*3+3);

                        strList[posY]=newStr;


                    }
                }
            }


            cc.log("     -----------Tetris by JasonWu-----------");
            for(var i=0;i<strList.length;i++)
            {
                cc.log(""+(10+i)+"       |"+ strList[i]+"|");
            }
            cc.log("     ---------------------------------------");


        }


        cc.eventManager.addListener({
            event: cc.EventListener.KEYBOARD,
            onKeyPressed:  function(keyCode, event){


                switch(keyCode)
                {
                    case 38://上
                        me.gameManager.input(Operator.DERECT_FALL);
                        break;
                    case 40://下
                        me.gameManager.input(Operator.SPEEDUP_START);
                        break;
                    case 37://左
                        me.gameManager.input(Operator.LEFT);
                        break;
                    case 39://右
                        me.gameManager.input(Operator.RIGHT);
                        break;
                    case 32://空格
                        me.gameManager.input(Operator.TURN);
                        break;
                }


                //cc.log("Key with keycode " + keyCode + " pressed");
            },
            onKeyReleased: function(keyCode, event){

                switch(keyCode) {
                    case 40://下
                        me.gameManager.input(Operator.SPEEDUP_END);
                    break;
                }


                //cc.log("Key with keycode " + keyCode + " released");
            }
        }, this);


        this.scheduleUpdate();

        return true;
    },

    update:function(f){
        this.gameManager.step();
    }
});

var GameScene = cc.Scene.extend({
    onEnter:function () {
        this._super();
        var layer = new GameLayer();
        this.addChild(layer);
    }
});