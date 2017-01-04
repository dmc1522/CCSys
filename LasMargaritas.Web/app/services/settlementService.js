'use strict';
app.factory('settlementService', ['$http', 'constants', function ($http, constants) {

    var settlementServiceFactory = {};
    var _getSettlements = function (cicleId) {
        return $http.get(constants.serviceBase + 'Settlement/GetAllByCicleId?cicleId='+cicleId).then(function (results) {
            return results;
        });
    };
    var _print = function(id)
    {
        var idModel = new Object();
        idModel.Id = id;

        $http({
            url: constants.serviceBase + 'Settlement/Print',
            method: 'POST',
            data: idModel,
            headers: {
                'Content-type': 'application/json',
            },
            responseType: 'arraybuffer'
        }).success(function (data, status, headers, config) {
            // TODO when WS success
            var file = new Blob([data], {
                type: 'application/pdf'
            });
            //trick to download store a file having its URL
            var fileURL = URL.createObjectURL(file);
            var a = document.createElement('a');
            a.href = fileURL;
            a.target = '_blank';
            a.download = 'liquidacion'+id+'.pdf';
            document.body.appendChild(a);
            a.click();
        }).error(function (data, status, headers, config) {
            //TODO when WS error
            var a = data;
        });
    }

    var _saveSettlement = function(settlement)
    {
        if (settlement.id > 0)
            return $http.post(constants.serviceBase + 'Settlement/Update', settlement).then(function (results) {
                return results;
            });
        else
        return $http.post(constants.serviceBase + 'Settlement/Add', settlement).then(function (results) {
            return results;
        });
    }

   
    var _getSettlementData = function (settlementId) {
        return $http.get(constants.serviceBase + 'Settlement/GetById?id=' + settlementId).then(function (results) {
            return results;
        });
    };

    var _addPayment = function (payment) {
        return $http.post(constants.serviceBase + 'Settlement/AddSettlementPayment', payment).then(function (results) {
            return results;
        });
    };

    var _getPayments = function (settlementId) {
        return $http.get(constants.serviceBase + 'Settlement/GetSettlementPayments?settlementId=' + settlementId).then(function (results) {
            return results;
        });
    };
    var _deletePayment = function (paymentId) {
        return $http.post(constants.serviceBase + 'Settlement/DeleteSettlementPayment', { id: paymentId }).then(function (results) {
            return results;
        });
    };

    settlementServiceFactory.getSettlements = _getSettlements;
    settlementServiceFactory.getSettlementData = _getSettlementData;
    settlementServiceFactory.addPayment = _addPayment;
    settlementServiceFactory.getPayments = _getPayments;
    settlementServiceFactory.saveSettlement = _saveSettlement;
    settlementServiceFactory.deletePayment = _deletePayment;
    settlementServiceFactory.print = _print;
    return settlementServiceFactory;

}]);