'use strict';
app.controller('settlementDetailsController', ['$scope', '$location', 'settlementService', 'cicleService', 'producerService','$routeParams', 'weightTicketService', 'ngDialog',
    function ($scope, $location, settlementService, cicleService, producerService, $routeParams, weightTicketService, ngDialog) {
        //SECTION: General initialization
      //  $(".form_datetime").datetimepicker({ format: 'yyyy-mm-dd' });
        var today = new Date().toISOString();
      
        //var today = new Date(new Date().getUTCFullYear(), new Date().getUTCMonth(), new Date().getUTCDate(), new Date().getUTCHours(), new Date().getUTCMinutes(), new Date().getUTCSeconds());
        //SECTION: Payments
        $scope.paymentTypes = [{ id: 0, name: 'Efectivo' }, { id: 1, name: 'Cheque' }, { id: 2, name: 'Transferencia' }];
        $scope.payment = { date: today, paymentType:0};
        $scope.payments = [];
        $scope.addPaymentSectionCollapsed = false;      
        $scope.deletePayment = function (index)
        {
            var idToRemove = $scope.payments[index].id;
           
            settlementService.deletePayment(idToRemove).then(function (results) {
                if (results.data.success) {
                    $scope.getPayments();
                    $scope.payment = { date: today, paymentType:0 };                  
                }
                else {
                    alert(results.data.errorMessage);
                }
            }, function (error) {
                alert(error.data.message);
            });
        }
        $scope.addPayment = function()
        {
            //TODO validate
            $scope.payment.settlementId = $scope.id;
            settlementService.addPayment($scope.payment).then(function (results) {
                if (results.data.success) {                  
                    $scope.getPayments();
                    $scope.payment = { date: today, paymentType:0};
                    $scope.updateTotals();
                }
                else {
                    alert(results.data.errorMessage);
                }
            }, function (error) {
                alert(error.data.message);
            });
            
        }
        $scope.getPayments = function()
        {
            settlementService.getPayments($scope.id).then(function (results) {
                if (results.data.success) {
                    $scope.payments = results.data.payments;
                    $scope.updateTotals();  
                }
                else {
                    alert(results.data.errorMessage);
                }
            }, function (error) {
                alert(error.data.message);
            });
        }
        //SECTION: Settelement
        $scope.finalBalance = 0;
        $scope.paymentsTotal = 0;
        $scope.id = $routeParams.id;       

        $scope.producerChanged = function ()
        {
            if ($scope.id != undefined && $scope.id > 0) {
                $scope.loadWeightTicketsAvailablesToSettle();
                $scope.loadWeightTicketsInSettlement();
            }
        }
        $scope.loadWeightTicketsAvailablesToSettle = function () {
            weightTicketService.getWeightTicketsAvailablesToSettle($scope.settlement.cicleId, $scope.settlement.producerId).then(function (results) {
                if (results.data.success) {                  
                    $scope.weightTicketsAvailablesToSettle = results.data.weightTickets;
                }
                else {
                    alert(results.data.errorMessage);
                }
            }, function (error) {
                alert(error.data.message);
            });
        }

        $scope.loadWeightTicketsInSettlement = function () {
            weightTicketService.getWeightTicketsInSettlement($scope.id).then(function (results) {
                if (results.data.success) {
                    $scope.weightTicketsInSettlement = results.data.weightTickets;
                    $scope.updateTotals();
                }
                else {
                    alert(results.data.errorMessage);
                }
            }, function (error) {
                alert(error.data.message);
            });
        }
        $scope.backToList = function()
        {
            $location.path('/settlement');
        }
        $scope.addWeightTicketToSettlement = function (availableIndex)
        {
            var weightTicket = $scope.weightTicketsAvailablesToSettle.splice(availableIndex, 1);
            $scope.weightTicketsInSettlement.push(weightTicket[0]);
            $scope.saveSettlement();
            $scope.updateTotals();
        }

        $scope.removeWeightTicketFromSettlement = function (weightTicketIndex) {
            var weightTicket = $scope.weightTicketsInSettlement.splice(weightTicketIndex, 1);
            $scope.weightTicketsAvailablesToSettle.push(weightTicket[0]);
            $scope.saveSettlement();
            $scope.updateTotals();
        }
        $scope.editWeightTicket = function(index)
        {
            var ticketId = $scope.weightTicketsInSettlement[index].id;
            ngDialog.open(
                {
                    template: 'app/views/weight-ticket-details.html',
                    controller: 'weightTicketController',
                    scope: $scope,
                    resolve: {
                        weightTicketId: function () {
                            return ticketId;
                        }
                    }
                }
            );
        }
        $scope.saveSettlement = function ()
        {
            $scope.settlement.weightTicketsIds = [];
            if ($scope.weightTicketsInSettlement != undefined) {
                for (var i = 0; i < $scope.weightTicketsInSettlement.length; i++) {
                    $scope.settlement.weightTicketsIds.push($scope.weightTicketsInSettlement[i].id);
                }
            }
            settlementService.saveSettlement($scope.settlement).then(function (results) {
                if (results.data.success) {                   
                    $scope.id = results.data.settlement.id;
                    $scope.settlement.id = $scope.id;
                    $scope.loadWeightTicketsAvailablesToSettle();
                } else {
                    alert(results.data.errorMessage);
                }
            }, function (error) {
                alert(error.data.message);
            });            
        }
        
        $scope.loadSettlementData = function () {
            settlementService.getSettlementData($scope.id).then(function (results) {
                if (results.data.success && results.data.settlements.length == 1) {
                    $scope.settlement = results.data.settlements[0];
                    $scope.loadWeightTicketsInSettlement();
                    $scope.loadWeightTicketsAvailablesToSettle();
                    $scope.getPayments();
                    $scope.updateTotals();
                }
                else {
                    alert(results.data.errorMessage);
                }
            }, function (error) {
                alert(error.data.message);
            });
        }       

        $scope.cicles = [];
        $scope.producers = [];
       
        cicleService.getCicles().then(function (results) {
            if (results.data.success) {
                $scope.cicles = results.data.cicles;
                if ($scope.cicles.length > 0) {
                    $scope.selectedCicle = $scope.cicles[0];               
                }
            }
            else {
                alert(results.data.errorMessage);
            }
        }, function (error) {
            alert(error.data.message);
        });
       
        producerService.getProducers().then(function (results) {
            if (results.data.success) {
                $scope.producers = results.data.selectableModels;
            }
            else {
                alert(results.data.errorMessage);
            }

        }, function (error) {
            alert(error.data.message);
        });


        if ($scope.id != undefined && $scope.id > 0) {
            $scope.loadSettlementData();
        }
        else {
            $scope.settlement = { id: 0, date: today, producerId: 0 };
        }
        $scope.sum = function(property)
        {
            var sum = 0;
            if ($scope.weightTicketsInSettlement != undefined)
            {
                for(var i = 0; i < $scope.weightTicketsInSettlement.length; i++)
                {
                    sum += $scope.weightTicketsInSettlement[i][property];
                }
            }
            return sum;
        }
        $scope.updateTotals = function () {
            $scope.settlement.weightTicketsTotal = 0;
            $scope.finalBalance = 0;
            $scope.paymentsTotal = 0;
            if ($scope.weightTicketsInSettlement != undefined) {
                for (var i = 0; i < $scope.weightTicketsInSettlement.length; i++) {
                    $scope.settlement.weightTicketsTotal += $scope.weightTicketsInSettlement[i].totalToPay;
                }
            }
        
            if ($scope.payments != undefined) {
                for (var i = 0; i < $scope.payments.length; i++) {
                    $scope.paymentsTotal += $scope.payments[i].total;
                }
            }
            $scope.settlement.total = parseFloat($scope.settlement.weightTicketsTotal) - parseFloat($scope.settlement.cashAdvancesTotal) - parseFloat($scope.settlement.creditsTotal);
            $scope.finalBalance = $scope.settlement.total - $scope.paymentsTotal;
        }
        $scope.print = function () {
            settlementService.print($scope.id);
        }
}]);