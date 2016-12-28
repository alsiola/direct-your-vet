define(function () {

    const SavedDayList = require('./saveDayList.js');

    return class DayList {

        constructor() {

            this.name = ko.observable("New Day List");

            this.dayListPlaces = ko.observableArray([]);

            this.savedDayLists = ko.observableArray([]);

            ko.bindingHandlers.slideOutUp = {
                init: function (element, valueAccessor) {
                    // Initially set the element to be instantly visible/hidden depending on the value
                    const value = valueAccessor();
                    $(element).toggle(ko.unwrap(value)); // Use "unwrapObservable" so we can handle values that may or may not be observable
                },
                update: function (element, valueAccessor) {
                    // Whenever the value subsequently changes, slowly fade the element in or out
                    const value = valueAccessor();
                    ko.unwrap(value) ? $(element).slideDown() : $(element).slideUp();
                }
            };
        }

        canAddToList(item)  {
            return !this.dayListPlaces().some(current => {
                return current.id() === item.id();
            });
        }

        canRemoveFromList(item) {
            return this.dayListPlaces().some(current => {
                return current.id() === item.id();
            });
        }

        tryDiscard() {
            nm.notyConfirm("Discard this daylist?").then(status => {
                if (status) {
                    this.discard();
                }
            });
        }

        discard() {
            this.name("New Day List");
            this.dayListPlaces.removeAll();
        }

        addToList(place) {
            if (this.dayListPlaces.indexOf(place) < 0) {
                this.dayListPlaces.push(place);
            }
        }

        removeFromList(place) {
            const temp = [];
            this.dayListPlaces().forEach(current => {
                if (current.id() !== place.id()) {
                    temp.push(current);
                }
            });
            this.dayListPlaces(temp);
        }

        save() {
            $.ajax({
                url: window.location.href + "/SaveDayList/",
                type: "POST",
                data: ko.toJSON(this),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: data => {
                    if (data.success) {
                        nm.notyMessage("Daylist saved.");
                        this.loadDayLists();
                        this.discard();
                    } else {
                        data.errors.forEach(error => {
                            nm.notyMessage(error);
                        });
                    }
                },
                error: () => {
                    nm.notyMessage("Server unavailable, daylist has not been saved.");
                }
            });
        }

        loadDayLists() {
            $.ajax({
                type: "GET",
                dataType: "json",
                url: window.location.href + "/DayLists/",
                success: data => {
                    var temp = [];
                    data.dayLists.forEach(dayList => {
                        temp.push(new SavedDayList(dayList));
                    });
                    this.savedDayLists(temp);
                }
            });
        }

        fadeOut(elem) {
            $(elem).slideUp(() => { $(elem).remove(); });
        }

        fadeIn(elem) {
            $(elem).hide().fadeIn();
        }              
    };
});
