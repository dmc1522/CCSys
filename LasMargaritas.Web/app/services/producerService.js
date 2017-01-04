'use strict';
app.factory('producerService', ['$http', 'constants', function ($http, constants) {
    var producersServiceFactory = {};

    var serviceBase = 'http://lasmargaritasdev.azurewebsites.net/';
    var _getProducers = function () {

        return $http.get(constants.serviceBase + 'Producer/GetSelectableModels').then(function (results) {
            return results;
        });
    };

    producersServiceFactory.getProducers = _getProducers;

    return producersServiceFactory;

}]);