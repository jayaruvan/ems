function ViewModel(items) {

    this.ProvincesArray = ko.observableArray(items);
    this.selectedProvince = ko.observable();
    this.selectedArea = ko.observable();

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

var ProvinceAreaDetails = "";

function LoadProvincesAndAreas(){
    var urlGetAll = "/Home/GetAllProvincesAreas";
    $.getJSON(urlGetAll, {}, function (data) {
        ProvinceAreaDetails = JSON.stringify(ko.toJS(data));
    });
}

module.refresh = new LoadProvincesAndAreas();

module.viewModel = new ViewModel(items);

ko.applyBindings(module.viewModel);