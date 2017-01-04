'use strict';
app.controller('settlementController', ['$scope', 'settlementService', 'cicleService', '$location', function ($scope, settlementService, cicleService, $location) {

    $scope.settlements = [];
    $scope.cicles = [];
    $scope.selectedCicle = {};
    cicleService.getCicles().then(function (results) {
        if (results.data.success) {
            $scope.cicles = results.data.cicles;
            if ($scope.cicles.length > 0) {
                $scope.selectedCicle = $scope.cicles[0];
                $scope.loadSettlements();
            }
        }
        else {
            alert(results.data.errorMessage);
        }

    }, function (error) {
        alert(error.data.message);
    });
    $scope.loadSettlements = function () {
        settlementService.getSettlements($scope.selectedCicle.id).then(function (results) {
            if (results.data.success) {
                $scope.settlements = results.data.settlements;
            }
            else {
                alert(results.data.errorMessage);
            }

        }, function (error) {
            alert(error.data.message);
        });
    };
    $scope.newSettlement = function () {
        $location.path('/settlement-details');
    };

}]);