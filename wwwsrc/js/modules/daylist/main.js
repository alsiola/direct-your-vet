define(function (require) {
    const DayListViewModel = require('./dayListViewModel');    

    gmaps.KEY = "AIzaSyDmN8YtEYRrv5rkzljADvn82I2pwCbaWlE";
    gmaps.LIBRARIES = ["places"];

    gmaps.load(google => {
        ko.applyBindings(new DayListViewModel());
    });
});