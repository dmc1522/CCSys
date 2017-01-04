'use strict';
app.factory('constants', [function () {

    var constantsServiceFactory = {};
    var _serviceBase = 'http://localhost:50115/';
    //var _serviceBase = 'http://lasmargaritasdev.azurewebsites.net/';
    //var _serviceBase = 'http://lasmargaritas.azurewebsites.net/';

    constantsServiceFactory.serviceBase = _serviceBase;

    return constantsServiceFactory;

}]);