


var objectMap = function(o, handler){
    var res = {};
    for(var key in o) {
        res[key] = handler(o[key])
    }
    return res;
};


var ProvincesViewModel = function() {
    var self = this;
    var url = "/Home/GetAllProvinces";
    var datafromserver = "";
    var refresh = function () {
        $.getJSON(url, {}, function (data) {

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
    
       

    
    
    refresh();
}

ko.applyBindings(new ProvincesViewModel())
