define(function () {
    return class SaveDayList {
        constructor(data) {
            this.name = data.name;
            this.dateAdded = data.dateAdded;
            this.detailsUrl = "/SubscriberDashboard/DayLists/" + data.id;
        }
    };
});
