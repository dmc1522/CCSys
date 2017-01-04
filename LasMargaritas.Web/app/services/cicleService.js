'use strict';
app.factory('cicleService', ['$http', 'constants', function ($http, constants) {

    var cicleServiceFactory = {};


    var _getCicles = function () {
        
        return $http.get(constants.serviceBase + 'Cicle/GetAll').then(function (results) {
            return results;
        });
    };

    cicleServiceFactory.getCicles = _getCicles;

    return cicleServiceFactory;

}]);