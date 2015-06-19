/**
 * Created by JasonWu on 6/17/15.
 */


var TetrisFactoryBase = cc.Class.extend({


    toShape:function(shapeStr){

        //将字符串矩阵转换成数组
        var shapeData=[];

        var rows= shapeStr.split("\r");

        for(var i=0;i<rows.length;i++)
        {
            var rowData=[];
            var e=rows[i].split(",");
            for(var j=0;j< e.length;j++){
                rowData.push(e[j]=='0'?0:1);
            }
            shapeData.push(rowData);
        }

        //1、由于游戏所使用的原点是在左下角，因此需要将数据进行对折。
        //2、由于字符串表示中是先行再列的，在游戏中需要转换成先列再行的格式。数组一维代表列。

        var width=shapeData[0].length;
        var height=shapeData.length;
        var shape=[];

        for(var x=0;x<width;x++)
        {
            var c=[];
            for(var y=0;y<height;y++){
                c.push(0);
            }
            shape.push(c);
        }

        for(var x=0;x<width;x++)
        {
            for(var y=0;y<height;y++){
                shape[x][y]=shapeData[(height-1)-y][x];
            }
        }

        return shape;
    },

    putShape:function(tetris,shape){

        var width=shape.length;
        var height=shape[0].length;
        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                tetris.shape[x][y].isNull= shape[x][y]==0;
            }
        }
    },

    create:function(){
    }

});

var TetrisLFactory=TetrisFactoryBase.extend({

    create:function(){

        var tetris=new Tetris(3,3,new ClassicRotate());

        var shape="";
        shape+="0,1,0\r";
        shape+="0,1,0\r";
        shape+="0,1,1";

        this.putShape(tetris,this.toShape(shape));

        return tetris;
    }

});

var TetrisJFactory=TetrisFactoryBase.extend({

    create:function(){
        var tetris=new Tetris(3,3,new ClassicRotate());

        var shape="";
        shape+="0,1,0\r";
        shape+="0,1,0\r";
        shape+="1,1,0";

        this.putShape(tetris,this.toShape(shape));

        return tetris;

    }

});

var TetrisTFactory=TetrisFactoryBase.extend({

    create:function(){
        var tetris=new Tetris(3,3,new ClassicRotate());

        var shape="";
        shape+="0,0,0\r";
        shape+="1,1,1\r";
        shape+="0,1,0";

        this.putShape(tetris,this.toShape(shape));

        return tetris;

    }

});

var TetrisSFactory=TetrisFactoryBase.extend({

    create:function(){
        var tetris=new Tetris(3,3,new ClassicRotate());

        var shape="";
        shape+="0,1,1\r";
        shape+="1,1,0\r";
        shape+="0,0,0";

        this.putShape(tetris,this.toShape(shape));

        return tetris;

    }

});

var TetrisZFactory=TetrisFactoryBase.extend({

    create:function(){
        var tetris=new Tetris(3,3,new ClassicRotate());

        var shape="";
        shape+="1,1,0\r";
        shape+="0,1,1\r";
        shape+="0,0,0";

        this.putShape(tetris,this.toShape(shape));

        return tetris;

    }

});

var TetrisOFactory=TetrisFactoryBase.extend({

    create:function(){
        var tetris=new Tetris(2,2,new ClassicRotate());

        var shape="";
        shape+="1,1\r";
        shape+="1,1";

        this.putShape(tetris,this.toShape(shape));

        return tetris;

    }

});

var TetrisIFactory=TetrisFactoryBase.extend({

    create:function(){
        var tetris=new Tetris(4,4,new ClassicRotate());

        var shape="";
        shape+="0,1,0,0\r";
        shape+="0,1,0,0\r";
        shape+="0,1,0,0\r";
        shape+="0,1,0,0";

        this.putShape(tetris,this.toShape(shape));

        return tetris;

    }

});