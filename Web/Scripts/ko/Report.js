
var objectMap = function (o, handler) {
    var res = {};
    for (var key in o) {
        res[key] = handler(o[key])
    }
    return res;
};


var ReportsViewModel = function () {
    var self = this;
    var url = "/Home/GetAllReports";
    var datafromserver = "";
    //
    /*var getSubStationName = function (Id) {
        var urlSubName = "/Home/GetSubStationById/" + Id;
        $.getJSON(urlSubName, {}, function (data) {
            var sub = new SubStation(data);
            return meter;
        });
    };*/


    var getAllReports = function () {
        $.getJSON(url, {}, function (data) {


            /**
                        var datastr = JSON.stringify(ko.toJS(data));
            **/
            var objectsArray = ko.utils.arrayMap(data, function (item) {

                var reports = new Reports(item);
                return reports;
            });

            self.ReportsArray(objectsArray);

        });
    };


    self.ReportsArray = ko.observableArray([]);
    
 

    function Reports(data) {
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;


        var date = new Date(parseInt(data.ReportDate.substr(6)));
        self.ReportDate = date.toISOString().substring(0, 10);

        self.CreatedDate = data.CreatedDate;
        //self.ReportDate = data.ReportDate;        
        self.SupplierId = data.SupplierId;
    }

   

    function Supplier(data) {
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
    }


    // Public operations

    getAllReports();
}

ko.applyBindings(new ReportsViewModel())
