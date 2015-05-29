
var SubStationViewModel = function() {
    var self = this;
    var urlGetAreas = "/Home/GetAllAreaOffices";


    self.SubStationsArray = ko.observableArray([]);

    
    var refresh = function () {
        $.getJSON(urlGetAreas, {}, function (data) {

            var datastr = JSON.stringify(ko.toJS(data));
            /**
            **/
            var objectsArray = ko.utils.arrayMap(data, function (item) {

                var area = new Area(item);

                var subStations = ko.utils.arrayMap(item.SubStationDTO, function (subsObject) {
                    var subs = new SubStation(subsObject);
                    return subs;
                });

                area.SubStationDTO(subStations);
                return area;
            });

            self.SubStationsArray(objectsArray);

        });
    };




    function Province(data) {
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
        self.AreaDTO = ko.observableArray([])
        self.SubStationDTO = ko.observableArray([])

        self.EditProvince = function () {
            window.location.href = '/Home/AreaCreateEdit/' + self.Id;
        };
    }
    function Area(data) {
        var self = this;
        self.Id = ko.observable(data.Id);
        self.Name = ko.observable(data.Name);
        self.ProvinceId = ko.observable(data.ProvinceId);
        self.SubStationDTO = ko.observableArray([]);

        self.EditArea = function () {
            //var test = self.Id._latestValue;
            // (Q) :Is there any propre way to do this ??
            window.location.href = '/Home/AreaCreateEdit/' + self.Id._latestValue;
        };

    }
    function SubStation(data) {
        var self = this;
        self.Id = ko.observable(data.Id);
        self.Name = ko.observable(data.Name);
    }

    
    // Public operations
    self.CreateArea = function () {
        window.location.href = '/Home/SubStationCreateEdit/0';
    };




    self.RemoveArea = function (objProvince) {
        if (confirm("Are you sure you want to delete this Province?")) {
            var id = objProvince.Id;
            //waitingDialog({});
            //Hae to implement this later

            $.ajax({
                type: 'DELETE', url: 'Home/DeleteSubStation/' + id,
                success: function () { self.SubStationsArray.remove(objProvince); },
                error: function (err) {
                    var error = JSON.parse(err.responseText);
                    $("<div></div>").html(error.Message).dialog({ modal: true, title: "Error", buttons: { "Ok": function () { $(this).dialog("close"); } } }).show();
                },
                complete: function () {
                    //closeWaitingDialog();
                    //have to implement that later
                }
            });
        }
    };
    
    refresh();
}

ko.applyBindings(new SubStationViewModel())
