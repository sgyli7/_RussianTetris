/**
 * Created by JasonWu on 6/17/15.
 */

var Tetris = cc.Class.extend({
    width:0,
    height:0,
    shape:null,
    position:null,
    _rotate:null,
    ctor:function(width,height,rotate){
        this.width=width;
        this.height=height;


        this.shape=[];
        for(var x=0;x<width;x++)
        {
            var c=[];
            for(var y=0;y<height;y++){
                c.push(new Element());
            }
            this.shape.push(c);
        }

        this.position=cc.p();

        this._rotate=rotate;

    },

    rotate:function() {
        this._rotate.execute(this.shape);
    },

    clone:function(){
        var t=new Tetris(this.width,this.height,this._rotate);
        t.position=cc.p( this.position.x,this.position.y);

        for(var x= 0;x<this.width;x++)
        {
            for(var y= 0;y<this.height;y++)
            {
                t.shape[x][y]=this.shape[x][y];
            }
        }

        return t;
    }

});