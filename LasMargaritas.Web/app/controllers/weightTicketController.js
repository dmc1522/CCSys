'use strict';
app.controller('weightTicketController', ['$scope', '$routeParams', 'weightTicketService', 'weightTicketId', 'ngDialog',
    function ($scope, $routeParams, weightTicketService, weightTicketId, ngDialog) {
        $scope.loadWeightTicket = function()
        {
            weightTicketService.getWeightTicket($scope.id).then(function (results) {
                if (results.data.success && results.data.weightTickets.length == 1) {
                    $scope.weightTicket = results.data.weightTickets[0];                   
                }
                else {
                    alert(results.data.errorMessage);
                }
            }, function (error) {
                alert(error.data.message);
            });
        }
        $scope.saveWeightTicket = function () {
            weightTicketService.saveWeightTicket($scope.weightTicket).then(function (results) {
                if (results.data.success) {
                    $scope.$parent.loadWeightTicketsAvailablesToSettle();
                    $scope.$parent.loadWeightTicketsInSettlement();
                    ngDialog.close();
                }
                else {
                    alert(results.data.errorMessage);
                }
            }, function (error) {
                alert(error.data.message);
            });
        }
        $scope.close = function()
        {
            ngDialog.close();
        }
        $scope.updateTotals = function()
        {
            //get discounts
            if ($scope.weightTicket.applyHumidity)
                $scope.weightTicket.humidityDiscount = $scope.getDiscount($scope.humidityDiscountType, $scope.weightTicket.humidity, $scope.weightTicket.netWeight);
            else
                $scope.weightTicket.humidityDiscount = 0;
            if ($scope.weightTicket.applyImpurities)
                $scope.weightTicket.impuritiesDiscount = $scope.getDiscount($scope.impuritiesDiscountType, $scope.weightTicket.impurities, $scope.weightTicket.netWeight);
            else
                $scope.weightTicket.impuritiesDiscount = 0;
            if ($scope.weightTicket.applyDrying)
                $scope.weightTicket.dryingDiscount = $scope.getDiscount($scope.dryingDiscountType, $scope.weightTicket.humidity, $scope.weightTicket.netWeight);
            else
                $scope.weightTicket.dryingDiscount = 0;

            //calculate totals
            var isEntranceWeightTicket = ($scope.weightTicket.entranceWeightKg > $scope.weightTicket.exitWeightKg);
            if (isEntranceWeightTicket)
            {
                $scope.weightTicket.entranceNetWeight = $scope.weightTicket.entranceWeightKg - $scope.weightTicket.exitWeightKg;
                $scope.weightTicket.exitNetWeight = 0;
                $scope.weightTicket.netWeight = $scope.weightTicket.entranceNetWeight;
            }
            else
            {
                $scope.weightTicket.exitNetWeight = $scope.weightTicket.exitWeightKg - $scope.weightTicket.entranceWeightKg;
                $scope.weightTicket.entranceNetWeight = 0;
                $scope.weightTicket.netWeight = $scope.weightTicket.exitNetWeight;
            }
            $scope.weightTicket.totalWeightToPay = $scope.weightTicket.netWeight - $scope.weightTicket.humidityDiscount - $scope.weightTicket.impuritiesDiscount;
            $scope.weightTicket.subTotal = $scope.weightTicket.price * $scope.weightTicket.totalWeightToPay;
            $scope.weightTicket.totalToPay = $scope.weightTicket.subTotal - $scope.weightTicket.dryingDiscount
                                                  - $scope.weightTicket.brokenGrainDiscount - $scope.weightTicket.crashedGrainDiscount
                                                  - $scope.weightTicket.damagedGrainDiscount - $scope.weightTicket.smallGrainDiscount;

        }
        $scope.humidityDiscountType = 0;
        $scope.impuritiesDiscountType = 1;
        $scope.dryingDiscountType = 2;
        $scope.getDiscount = function(discountType, value, totalKg)
        {
            var isEntranceWeightTicket = ($scope.weightTicket.entranceWeightKg > $scope.weightTicket.exitWeightKg);
            switch (discountType)
            {
                case $scope.humidityDiscountType:
                    if (isEntranceWeightTicket)
                        return (value > 14.0 ? ((value - 14.0) * 0.0116 * totalKg) : 0.00);
                else
                        return (value - 14.0) * 0.0116 * totalKg;
                case $scope.impuritiesDiscountType:
                    if (isEntranceWeightTicket)
                        return (value > 2 ? ((value - 2) * 0.01 * totalKg) : 0); 
                else
                        return (value - 2) * 0.01 * totalKg;
                case $scope.dryingDiscountType:
                    if (isEntranceWeightTicket)
                        return (value >= 16 ? ((value - 16.0) * 10.0 + 130.0) * totalKg / 1000.0 : 0.0);
                else
                        return 0;
            }
            return 0;
        }
        $scope.id = weightTicketId;
        $scope.weightTicket = {};
        $scope.weightTicket.id = weightTicketId;
        if ($scope.id != undefined && $scope.id > 0)
            $scope.loadWeightTicket();
      

}]);