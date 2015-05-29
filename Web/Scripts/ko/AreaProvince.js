

var AreasViewModel = function() {
    var self = this;
    var url = "/Home/GetAllProvinces";


    var refresh = function () {
        $.getJSON(url, {}, function (data) {

            var datastr = JSON.stringify(ko.toJS(data));
            /**
            **/
            var objectsArray = ko.utils.arrayMap(data, function (item) {

                var province = new Province(item);

                var areas = ko.utils.arrayMap(item.AreaDTO, function (areaObject) {
                    var area = new Area(areaObject);
                    return area;
                });
                var subStations = ko.utils.arrayMap(item.SubStationDTO, function (subsObject) {
                    var subs = new SubStation(subsObject);
                    return subs;
                });

                province.AreaDTO(areas);
                return province;
            });

            self.ProvincesArray(objectsArray);

        });
    };
    

    self.ProvincesArray = ko.observableArray([]);


    function Province(data){
        var self = this;
        self.Id = data.Id;
        self.Name = data.Name;
        self.AreaDTO = ko.observableArray([])
        self.SubStationDTO = ko.observableArray([])
    }    
    function Area(data){
        var self = this;
        self.Id = ko.observable(data.Id);
        self.Name = ko.observable(data.Name);
        self.ProvinceId=ko.observable(data.ProvinceId);
        self.SubStationDTO = ko.observableArray([]);
    }       
    function SubStation(data){
        var self = this;
        self.Id = ko.observable(data.Id);
        self.Name = ko.observable(data.Name);
    }       
    
    // Public operations
    self.CreateArea = function () {
        window.location.href = '/Home/AreaCreateEdit/0';
    };

    self.EditArea = function (objArea) {
        window.location.href = '/Home/AreaCreateEdit/' + objArea.Id;
    };


    self.RemoveArea = function (objProvince) {
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
    
    refresh();
}

ko.applyBindings(new AreasViewModel())
