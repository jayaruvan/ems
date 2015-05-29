var MetersViewModel = function () {
/**
    MetersArray
        |-Id ( not visible)
        |-Serial
        |-Name
        |-LastReading
        --EditMeter()
        --DeleteMeter()
**/
    var self = this;


    //Create Meter Object to hold above Properties
    function Meters(data) {
        var self = this;
        self.Id = data.Id;
        self.Serial = data.Serial;
        self.Name = data.Name;
        self.LastReading = data.LastReading;

        //Link Functions
        self.EditMeter = function () {
            window.location.href = '/Home/MeterCreateEdit/' + self.Id;
        };

        self.DeleteMeter = function () {
           // window.location.href = '/Home/DeleteMeter/' + obj.Id;
        };


    }

    self.MetersArray = ko.observableArray([]);



   




    /**
    Link Functions
    **/
    // Public operations
    self.CreateNewMeter = function () {
        window.location.href = '/Home/MeterCreateEdit/0';
    };

    var GetAllMeters = function () {
        var urlGetAll = '/home/GetAllMeters';
        //Get All meter data from Controller ...
        $.getJSON(urlGetAll, {}, function (dataArayOut) {
            //data is a array
            // [{ ,,,,},{,,,,} ..... ]
            var datastr = JSON.stringify(ko.toJS(dataArayOut));
            var metersObjects = ko.utils.arrayMap(dataArayOut, function (dataObject) {
                var meter = new Meters(dataObject);
                return meter;
            });

            self.MetersArray(metersObjects);

        });


    }
    //load data from server 
    GetAllMeters();







}



ko.applyBindings(new MetersViewModel())