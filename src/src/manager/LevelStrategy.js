/**
 * Created by JasonWu on 6/17/15.
 */
var LevelStrategyBase = cc.Class.extend({
    
    execute: function () {

    }

});


var SimpleLevelStrategy = cc.Class.extend({

    execute: function () {
        var s = Math.random();
        if (s < 0.1) {
            return new TetrisLFactory();
        } else if (s < 0.2) {
            return new TetrisJFactory();
        } else if (s < 0.3) {
            return new TetrisTFactory();
        } else if (s < 0.4) {
            return new TetrisSFactory();
        } else if (s < 0.5) {
            return new TetrisZFactory();
        } else if (s < 0.7) {
            return new TetrisOFactory();
        } else {
            return new TetrisIFactory();
        }
    }
});



var NormalLevelStrategy = cc.Class.extend({

    execute: function () {

        var map=[];
        map.push(new TetrisLFactory());
        map.push(new TetrisJFactory());
        map.push(new TetrisTFactory());
        map.push(new TetrisSFactory());
        map.push(new TetrisZFactory());
        map.push(new TetrisOFactory());
        map.push(new TetrisIFactory());

        return map[Math.floor(Math.random()*7)];
    }
});


var AdvancedLevelStrategy = cc.Class.extend({

    execute: function () {
        var s = Math.random();
        if (s < 0.2) {
            return new TetrisLFactory();
        } else if (s < 0.4) {
            return new TetrisJFactory();
        } else if (s < 0.6) {
            return new TetrisTFactory();
        } else if (s < 0.75) {
            return new TetrisSFactory();
        } else if (s < 0.9) {
            return new TetrisZFactory();
        } else if (s < 0.95) {
            return new TetrisOFactory();
        } else {
            return new TetrisIFactory();
        }
    }

});
