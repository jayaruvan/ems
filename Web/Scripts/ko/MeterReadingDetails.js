
var objectMap = function (o, handler) {
    var res = {};
    for (var key in o) {
        res[key] = handler(o[key])
    }
    return res;
};


var MeterReadingViewModel = function () {
    var self = this;
    var url = "/Home/GetAllMetersReading";
    var datafromserver = "";


    var getAllMeterReadings = function () {
        $.getJSON(url, {}, function (data) {
           
            /**
             var datastr = JSON.stringify(ko.toJS(data));
            **/
            var objectsArray = ko.utils.arrayMap(data, function (item) {

                var meterReading = new MeterReading(item);
                return meterReading;
            });

            self.MeterReadingArray(objectsArray);

        });
    };


    self.MeterReadingArray = ko.observableArray([]);


    function MeterReading(data) {
        var self = this;
        self.Id = data.Id;

        var date = new Date(parseInt(data.ReadingDate.substr(6)));
        self.ReadingDate = date.toISOString().substring(0, 10);
        self.DayValue = data.DayValue;
        self.PeakValue = data.PeakValue;
        self.OffPeakValue = data.OffPeakValue;
        self.CoincidentPeak = data.CoincidentPeak;
        self.MeterId = data.MeterId;
        self.ReportId = data.ReportId;
    }

    function Meter(data) {
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
    }

    function Report(data) {
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
    }

    // Public operations
  
    getAllMeterReadings();
}

ko.applyBindings(new MeterReadingViewModel())
