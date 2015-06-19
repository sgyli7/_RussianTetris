/**
 * Created by JasonWu on 6/17/15.
 */

var Map = cc.Class.extend({
    width:0,
    height:0,
    map:null,
    ctor:function(width,height){
        this.width=width;
        this.height=height;

        //此处格式使用[y][x]的形式存储，这样更加便于修改
        this.map=[];
        for(var y=0;y<height;y++){
            var c=[];
            for(var x=0;x<width;x++){
                c.push(new Element());
            }
            this.map.push(c);
        }


    },

    checkElements:function(){

        var compCount=0;

        var map=this.map;
        var width=this.width;
        var height=this.height;
        for(var y= 0;y<height;y++){

            var row=map[y];

            var isComp=true;
            for(var x=0;x<width;x++){
                if(row[x].isNull==true){
                    isComp=false;
                    break;
                }
            }

            if(isComp){
                //消除该行，将其清零排列在最后
                map.splice(y,1);
                map.push(row);
                y--;
                compCount++;

                for(var x=0;x<width;x++){
                    row[x].isNull=true;
                }
            }

        }

        return compCount;
    }


});