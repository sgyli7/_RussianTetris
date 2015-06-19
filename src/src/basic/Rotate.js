/**
 * Created by JasonWu on 6/17/15.
 */

var RotateBase = cc.Class.extend({

    execute:function(shape) {

    }

});

var ClassicRotate=RotateBase.extend({

    execute:function(shape) {

        if(shape==null||shape.length==0){
            throw new Error("shape format error");
            return;
        }

        var width=shape.length;
        var height=shape[0].length;

        var tmpShape=shape.concat();
        for(var i = 0; i < width; i++){
            tmpShape[i]=shape[i].concat();
        }

        for (var x = 0; x < width; x++) {
            for (var y = 0; y < height; y++) {
                shape[y][x] = tmpShape[x][(width-1)-y];
            }
        }
    }

});