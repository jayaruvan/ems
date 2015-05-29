function ViewModel() {
    var self = this;
    var urlGerAll = "/Home/GetAllProvinces";
    this.provinces = ko.observableArray([]);

    $.getJSON(urlGerAll, {}, function (data) {
        var datastr = JSON.stringify(ko.toJS(data));

        var provinceNamesArray = ko.utils.arrayMap(data, function (item) {
            var province = new Province(item);
            if (self.selectedProvinceId == 0) { self.selectedProvinceId(province.Id); }
            return province;
        });

        self.provinces(provinceNamesArray);
        return (datastr);
    });


    
    this.selectedProvince = ko.observable();
    this.selectedArea = ko.observable();

    function getById(items, value) {
        if (!value) {
            return [];
        }

        var result = ko.utils.arrayFirst(items, function (item) {
            return item.value === value;
        });

        return result && result.childItems || [];
    }

    this.areas = ko.computed(function () {
        var items = this.provinces();
        var id = this.selectedProvince()
        return getById(items, id);
    }, this);


    this.engines = ko.computed(function () {
        var items = this.areas();
        var id = this.selectedArea()
        return getById(items, id);
    }, this);



}




var itemsx = [
    {
        text: 'Ford', value: 1, childItems:
         [
             {
                 text: 'F-150', value: 1, childItems:
                  [
                      { text: 'Gasoline', value: 1, childItems: [] },
                      { text: 'Diesel', value: 2, childItems: [] }
                  ]
             },
             {
                 text: 'F-250', value: 2, childItems:
                  [
                      { text: 'Gasoline', value: 3, childItems: [] },
                      { text: 'Diesel', value: 4, childItems: [] }
                  ]
             }
         ]
    },
    {
        text: 'Honda', value: 2, childItems:
         [
             {
                 text: 'Civic', value: 5, childItems:
                  [
                      { text: 'Gasoline', value: 5, childItems: [] },
                      { text: 'Electric', value: 6, childItems: [] }
                  ]
             },
             {
                 text: 'Accord', id: 6, childItems:
                  [
                      { text: 'Gasoline', value: 7, childItems: [] },
                      { text: 'Electric', value: 8, childItems: [] }
                  ]
             }
         ]
    }
];


var module = {};

module.viewModel = new ViewModel();

ko.applyBindings(module.viewModel);