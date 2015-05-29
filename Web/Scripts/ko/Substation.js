var SubViewModel = function () {
    var self = this;
    var urlGetAll = "/Home/GetAllAreaOffices";

    var loadAll = function () {
        $.getJSON(urlGetAll, {}, function (data) {

            var datastr = JSON.stringify(ko.toJS(data));
            /**
            //Array code

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

            self.SubStationArray(objectsArray);

        });
    };


    self.SubStationArray = ko.observableArray([]);

    function SubStation(data) {
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
        self.ProvinceId = data.ProvinceId;
        self.AreaId = data.AreaId;
        self.MeterDTO = ko.observableArray([]);

        
        self.EditSubStation = function () {
            //var test = self.Id._latestValue;
            // (Q) :Is there any propre way to do this ??
            window.location.href = '/Home/SubStationCreateEdit/' + self.Id;
        };
    }

    function Area(data) {
        var self = this;
        self.Id = ko.observable(data.Id);
        self.Name = ko.observable(data.Name);
        self.ProvinceId = ko.observable(data.ProvinceId);
        self.SubStationDTO = ko.observableArray([]);

        self.EditSubStation = function () {
            //var test = self.Id._latestValue;
            // (Q) :Is there any propre way to do this ??
            window.location.href = '/Home/SubStationCreateEdit/' + self.Id._latestValue;
        };
    }

    // Public operations
    self.CreateSubStation = function () {
        window.location.href = '/Home/SubStationCreateEdit/0';
    };
/**
    self.EditArea = function (objArea) {
        window.location.href = '/Home/SubStationCreateEdit/' + objArea.Id;
    };
**/

    self.RemoveSubStation = function (objProvince) {
        if (confirm("Are you sure you want to delete this Province?")) {
            var id = objProvince.Id;
            //waitingDialog({});
            //Hae to implement this later

            $.ajax({
                type: 'DELETE', url: 'Home/DeleteArea/' + id,
                success: function () { self.ProvincesArray.remove(objProvince); },
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
    loadAll();
}

ko.applyBindings(new SubViewModel())