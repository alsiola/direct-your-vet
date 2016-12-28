define(function () {
        const Place = require('./place.js'),
              DayList = require('./daylist.js');

        return class DashboardViewModel {
            constructor() {
                this.places = ko.observableArray([]);

                this.searchClient = ko.observable("").extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 400 } });
                this.searchPlace = ko.observable("").extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 400 } });
                this.searchAddress = ko.observable("").extend({ rateLimit: { method: "notifyWhenChangesStop", timeout: 400 } });

                this.hasResults = ko.observable(false);

                this.dayList = ko.observable(new DayList());

                this.page = ko.observable(1);
                this.numPerPage = ko.observable(5);
                this.numResults = ko.observable(0);

                this.numPages = ko.computed(() => Math.ceil(this.numResults() / this.numPerPage()));

                this.pageArray = ko.computed(() => {
                    const pageArr = [];
                    for (var i = 0; i < this.numPages(); i++) {
                        pageArr.push(i);
                    }
                    return pageArr;
                });

                this.startedDayList = ko.computed(() => {
                    return this.dayList().dayListPlaces().length > 0;
                });

                this.resultSizes = ko.observableArray([3, 5, 10, 20, 50, 100]);

                ko.computed(() => {
                    this.hasResults(false);
                    const apiUrl = "/SearchPlaces/?page=" + this.page() + "&take=" + this.numPerPage() + "&placeTerm=" + this.searchPlace() + "&clientTerm=" + this.searchClient() + "&addressTerm=" + this.searchAddress();
                    return $.ajax({
                        type: "GET",
                        dataType: "json",
                        url: window.location.href + apiUrl,
                        success: data => {
                            const temp = [];

                            data.places.forEach(place => {
                                temp.push(new Place(place));
                            });

                            this.places(temp);
                            this.numResults(data.totalResults);

                            if ((this.page() - 1) * this.numPerPage() > data.totalResults) {
                                this.page(1);
                            }

                            this.hasResults(true);
                        }
                    });
                }).extend({ deferred: true });

                this.dayList().loadDayLists();
            }

            clearSearch() {
                this.searchClient("");
                this.searchAddress("");
                this.searchPlace("");
            }

            setPage(pageNum) {
                if (pageNum > 0 && pageNum <= this.numPages()) {
                    this.page(pageNum);
                }
            }

            log(item) {
                console.log(item);
            }
    };
});
