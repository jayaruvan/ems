


var objectMap = function(o, handler){
    var res = {};
    for(var key in o) {
        res[key] = handler(o[key])
    }
    return res;
};


var ProvincesViewModel = function() {
    var self = this;
    var urlGetAllProvince = "/Home/GetAllProvinces";
    var datafromserver = "";
    var refresh = function () {
        $.getJSON(urlGetAllProvince, {}, function (data) {

            //var datastr = JSON.stringify(ko.toJS(data));
            /**
            [{"Id":12,"Name":"Province B","AreaDTO":[],"SubStationDTO":[]},
             {"Id":1,"Name":"Province A","AreaDTO":[
                                            {"Id":1,"Name":"Provicne A AreaA","ProvinceId":1,"SubStationDTO":null},
                                            {"Id":5,"Name":"Province A Area B","ProvinceId":1,"SubStationDTO":null}],
                                                        "SubStationDTO":[]}]
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
    self.CreateProvince = function () {
        window.location.href = '/Home/ProvinceCreateEdit/0';
    };

    self.EditProvince = function (objProvince) {
        window.location.href = '/Home/ProvinceCreateEdit/' + objProvince.Id;
    };

    self.RemoveProvince = function (obj) {
        if (confirm("Are you sure you want to delete this Province?")) {
            var id = obj.Id;
            //waitingDialog({});
            //Hae to implement this later
            var urlDel='/Home/RemoveProvince/' + obj.Id;
            $.ajax({
                type: 'DELETE', url: urlDel,
                success: function () { self.ProvincesArray.remove(obj); },
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

ko.applyBindings(new ProvincesViewModel())
