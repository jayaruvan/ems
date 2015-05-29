function ViewModel(items) {
    var self = this;
    self.ProvincesArray = ko.observableArray();
    self.selectedProvince = ko.observable();
    self.selectedArea = ko.observable();

    function getById(items, Id) {
        if (!Id) {
            return [];
        }

        var result = ko.utils.arrayFirst(items, function (item) {
            return item.Id === Id;
        });

        return result && result.AreaDTO || [];
    }

    this.AreasArray = ko.computed(function () {
        var items = this.ProvincesArray();
        var id = this.selectedProvince()
        return getById(items, id);
    }, this);


    var ProvinceAreaDetails = "";

    function LoadProvincesAndAreas() {
        var urlGetAll = "/Home/GetAllProvincesAreas";
        $.getJSON(urlGetAll, {}, function (data) {

            var objectsArray = ko.utils.arrayMap(data, function (item) {
                return item;
            });
           self.ProvincesArray(objectsArray);
        });
    }

    LoadProvincesAndAreas();
}



var items = [
    {
        Name: 'ProvinceA', Id: 1, AreaDTO:
         [
             {
                 Name: 'Area A', Id: 1, SubStationDTO:[]
             },
             {
                 Name: 'Area B', Id: 2, SubStationDTO: []
             }
         ]
    },
    {
        Name: 'Province B', Id: 2, AreaDTO:
         [
             {
                 Name: 'Prob A1', Id: 5, SubStationDTO: []
             },
             {
                 Name: 'Prob A2', id: 6, SubStationDTO:
                  [
                      { Name: 'Gasoline', Id: 7, childItems: [] },
                      { Name: 'Electric', Id: 8, childItems: [] }
                  ]
             }
         ]
    }
];

var module = {};

module.viewModel = new ViewModel(item);

ko.applyBindings(module.viewModel);