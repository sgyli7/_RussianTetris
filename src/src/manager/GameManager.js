/**
 * Created by JasonWu on 6/17/15.
 */
var GameManager = cc.Class.extend({

    map:null,//用于保存当前地图的信息

    currentTetris:null,//当前的砖头

    tetrisList:null,//当前剩余的砖头

    levelStrategyHashMap:null,//用于存放游戏策略的Map

    gameRule:null,//当前所有关卡的信息

    playerCredit:null,//玩家当前的信息

    eventCallback:null,//事件回调函数

    _gameRuleInitCompleted:false,//表示关卡信息是否加载玩过

    _time:0,//当前执行的时间 每帧增加一点，用于控制砖头移动

    _isSpeedup:false,//当前是否加速状态

    _score:0,//当前累计所要达到的积分

    ctor:function(){
        this.map=new Map(10,20);
        this.tetrisList=[];
        this.playerCredit=new PlayerCredit();
        this.gameRule=[];
        this.levelStrategyHashMap={};
        this.levelStrategyHashMap["simple"]=new SimpleLevelStrategy();
        this.levelStrategyHashMap["normal"]=new NormalLevelStrategy();
        this.levelStrategyHashMap["advanced"]=new AdvancedLevelStrategy();

        //异步加载资源
        var me=this;
        cc.loader.loadTxt(res.GameRule_csv,function(err,txt){

            var ruleStrList=txt.split("\r\n");

            for(var i=1;i<ruleStrList.length;i++)
            {
                var ruleValue=ruleStrList[i].split(",");
                var rule=new GameRule();
                rule.speed=parseInt(ruleValue[0]);
                rule.score=parseInt(ruleValue[1]);
                rule.strategy=ruleValue[2];
                me.gameRule.push(rule);
            }

            me._gameRuleInitCompleted=true;

        });



    },

    step:function(){
        if(this._gameRuleInitCompleted==false)
            return;


        this._time+=cc.director.getDeltaTime();

        var rule=this.gameRule[this.playerCredit.level];

        //获取砖头
        if(this.currentTetris==null)
        {
            //如何是第一次进入，则创建一定数量的砖头
            if(this.tetrisList.length==0){
                for(var i=0;i<5;i++){
                    var s=this.levelStrategyHashMap[rule.strategy];
                    this.tetrisList.push(s.execute().create());
                }
            }

            //取出砖头
            this.currentTetris=this.tetrisList.shift();
            this.currentTetris.position.x=Math.floor((this.map.width-this.currentTetris.width)/2);
            this.currentTetris.position.y=this.map.height;

            //再存入新的
            var s=this.levelStrategyHashMap[rule.strategy];
            this.tetrisList.push(s.execute().create());

        }

        //向下移动砖头


        var speed=1/rule.speed;//计算出每格所需的时间  如果时间满足则可以移动
        if(this._isSpeedup)speed/=3;//加速


        while(this.currentTetris!=null && this._time>speed)
        {
            this.currentTetris.position.y-=1;

            //检查碰撞
            if(this.collisionDetection(this.currentTetris)) {
                //吸附砖头 抛出事件
                this.currentTetris.position.y += 1;
                this.putTetrisToMap(this.currentTetris);

                this.checkElements();

                this.currentTetris = null;

            }else{
                if (this.eventCallback != null)
                    this.eventCallback(TetrisEvent.CHANGE_POSITION);

            }

            this._time-=speed;
        }





    },

    input:function(operator){
        if(this.currentTetris==null)
            return;

        switch(operator){
            case Operator.LEFT:


                this.currentTetris.position.x--;

                if(this.collisionDetection(this.currentTetris))
                {
                    this.currentTetris.position.x++;
                }else{
                    //抛出事件
                    if (this.eventCallback != null)
                        this.eventCallback(TetrisEvent.CHANGE_POSITION);
                }

                break;
            case Operator.RIGHT:

                this.currentTetris.position.x++;

                if(this.collisionDetection(this.currentTetris))
                {
                    this.currentTetris.position.x--;
                }else{
                    //抛出事件
                    if (this.eventCallback != null)
                        this.eventCallback(TetrisEvent.CHANGE_POSITION);
                }

                break;
            case Operator.DERECT_FALL:
                //直接落地

                while(this.currentTetris!=null)
                {
                    this.currentTetris.position.y-=1;

                    if(this.collisionDetection(this.currentTetris))
                    {
                        this.currentTetris.position.y+=1;

                        this.putTetrisToMap(this.currentTetris);

                        this.checkElements();

                        this.currentTetris = null;
                    }
                }

                break;
            case Operator.SPEEDUP_START:
                //开始加速
                this._isSpeedup=true;
                break;
            case Operator.SPEEDUP_END:
                //结束加速
                this._isSpeedup=false;
                break;
            case Operator.TURN:
                //旋转

                var tmpTetris= this.currentTetris.clone();
                tmpTetris.rotate();

                var me=this;
                var testList=[];
                testList.push(function(t){
                    //检测碰撞
                    return me.collisionDetection(t);
                });
                testList.push(function(t){
                    //向下移动 进行检测
                    t.position.y-=1;
                    var result= me.collisionDetection(t);
                    if(result)
                    {
                        t.position.y+=1;
                    }
                    return result;
                });
                testList.push(function(t){
                    //向上检测
                    t.position.y+=1;
                    var result= me.collisionDetection(t);
                    if(result)
                    {
                        t.position.y-=1;
                    }
                    return result;
                });
                testList.push(function(t){
                    //向左检测
                    t.position.x-=1;
                    var result= me.collisionDetection(t);
                    if(result)
                    {
                        t.position.x+=1;
                    }
                    return result;
                });
                testList.push(function(t){
                    //向右检测
                    t.position.x+=1;
                    var result= me.collisionDetection(t);
                    if(result)
                    {
                        t.position.x-=1;
                    }
                    return result;
                });

                var isCancel=true;
                for(var i=0;i<testList.length;i++)
                {
                    if(testList[i](tmpTetris)==false)
                    {
                        isCancel=false;
                        break;
                    }
                }

                this.currentTetris=tmpTetris;

                break;
        }

    },

    collisionDetection:function(tetris){

        for(var x=0;x<tetris.width;x++)
        {
            for(var y=0;y<tetris.height;y++)
            {
                var e=tetris.shape[x][y];
                if(e.isNull==false)
                {
                    //检测地图 注意：地图数据是先行再列[y][x]

                    if(y+ tetris.position.y<0)//接触到底边
                        return true;

                    if(x+ tetris.position.x<0)//接触到左边
                        return true;

                    if(x+tetris.position.x>=this.map.width)//接触到右边
                        return true;

                    var row=this.map.map[y+ tetris.position.y];
                    if(row==null)
                        continue;
                    var e=row[x+ tetris.position.x];
                    if(e==null)
                        continue;
                    if(e.isNull==false)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    },

    putTetrisToMap:function(tetris){

        for(var x=0;x<tetris.width;x++)
        {
            for(var y=0;y<tetris.height;y++)
            {
                var e=tetris.shape[x][y];
                if(e.isNull==false)
                {
                    if(y+ tetris.position.y>=this.map.height)
                    {
                        //游戏结束
                        if (this.eventCallback != null)
                            this.eventCallback(TetrisEvent.GAME_OVER);

                        return;
                    }

                    //填充数据 注意：地图数据是先行再列[y][x]
                    this.map.map[y+ tetris.position.y][x+ tetris.position.x].isNull=false;
                }
            }
        }

    },

    checkElements:function(){
        //检查消除

        var rule=this.gameRule[this.playerCredit.level];

        var clearCount = this.map.checkElements();
        if (clearCount != 0) {
            //加分
            this.playerCredit.score += 10 * clearCount * clearCount;
            //超过一定的关卡积分则升级
            if (this.playerCredit.score > this._score+rule.score) {
                this._score+=rule.score;
                this.playerCredit.level++;
            }
            //抛出事件
            if (this.eventCallback != null)
                this.eventCallback(TetrisEvent.ATTACH);
        }

    }

});