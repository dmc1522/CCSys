'use strict';
app.factory('weightTicketService', ['$http', 'constants', function ($http, constants) {
    var weightTicketServiceFactory = {};

    var _getWeightTicketsInSettlement = function (settlementId) {
        return $http.get(constants.serviceBase + 'WeightTicket/GetWeightTicketsInSettlementFullDetails?settlementId='+settlementId).then(function (results) {
            return results;
        });
    };

    var _saveWeightTicket = function (weightTicket) {
        if (weightTicket.id == 0)
            return $http.post(constants.serviceBase + 'WeightTicket/Add', weightTicket).then(function (results) {
                return results;
            });
        else
            return $http.post(constants.serviceBase + 'WeightTicket/Update', weightTicket).then(function (results) {
                return results;
            });
    };
    

    var _getWeightTicket = function (id) {
        return $http.get(constants.serviceBase + 'WeightTicket/GetById?id=' + id).then(function (results) {
            return results;
        });
    };

    var _getWeightTicketsAvailablesToSettle = function (cicleId, producerId) {
        return $http.get(constants.serviceBase + 'WeightTicket/GetWeightTicketsAvailablesToSettleFullDetails?cicleId=' + cicleId+ '&producerId='+producerId).then(function (results) {
            return results;
        });
    };
    weightTicketServiceFactory.getWeightTicketsInSettlement = _getWeightTicketsInSettlement;
    weightTicketServiceFactory.getWeightTicketsAvailablesToSettle = _getWeightTicketsAvailablesToSettle;
    weightTicketServiceFactory.getWeightTicket = _getWeightTicket;
    weightTicketServiceFactory.saveWeightTicket = _saveWeightTicket;

    return weightTicketServiceFactory;

}]);