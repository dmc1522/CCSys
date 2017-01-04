'use strict';
app.controller('homeController', ['$scope', 'authService', '$location', function ($scope, authService, $location) {
    if(authService.authentication.userName)
    {
        $location.path('/settlement');
    }
}]);