var app = angular.module('LasMargaritasApp', ['ngRoute', 'LocalStorageModule', 'angular-loading-bar', 'ngDialog', 'angularMoment' ]);

app.directive('currencyFormat', ['$filter', '$parse', function ($filter, $parse) {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelController) {

            var decimals = $parse(attrs.decimals)(scope);

            ngModelController.$parsers.push(function (data) {
                // Attempt to convert user input into a numeric type to store
                // as the model value (otherwise it will be stored as a string)
                // NOTE: Return undefined to indicate that a parse error has occurred
                //       (i.e. bad user input)
                var parsed = parseFloat(data);
                return !isNaN(parsed) ? parsed : undefined;
            });

            ngModelController.$formatters.push(function (data) {
                //convert data from model format to view format
                return $filter('currency')(data, decimals); //converted
            });

            element.bind('focus', function () {
                element.val(ngModelController.$modelValue);
            });

            element.bind('blur', function () {
                // Apply formatting on the stored model value for display
                var formatted = $filter('currency')(ngModelController.$modelValue, decimals);
                element.val(formatted);
            });
        }
    }
}]);

app.directive('numberFormat', ['$filter', '$parse', function ($filter, $parse) {
    return {
        require: 'ngModel',
        link: function (scope, element, attrs, ngModelController) {

            var decimals = $parse(attrs.decimals)(scope);

            ngModelController.$parsers.push(function (data) {
                // Attempt to convert user input into a numeric type to store
                // as the model value (otherwise it will be stored as a string)
                // NOTE: Return undefined to indicate that a parse error has occurred
                //       (i.e. bad user input)
                var parsed = parseFloat(data);
                return !isNaN(parsed) ? parsed : undefined;
            });

            ngModelController.$formatters.push(function (data) {
                //convert data from model format to view format
                return $filter('number')(data, decimals); //converted
            });

            element.bind('focus', function () {
                element.val(ngModelController.$modelValue);
            });

            element.bind('blur', function () {
                // Apply formatting on the stored model value for display
                var formatted = $filter('number')(ngModelController.$modelValue, decimals);
                element.val(formatted);
            });
        }
    }
}]);
app.directive('appDatetime', function ($window) {
    return {
        restrict: 'A',
        require: 'ngModel',
        link: function (scope, element, attrs, ngModel) {
            var moment = $window.moment;

            ngModel.$formatters.push(formatter);
            ngModel.$parsers.push(parser);

            element.on('change', function (e) {
                var element = e.target;
                element.value = formatter(ngModel.$modelValue);
                console.log('element.value is ' + element.value);
            });

            function parser(value) {
                console.log("PARSER value is " + value);
                var m = moment(value, 'YYYY/MM/DD');
                var valid = m.isValid();
                ngModel.$setValidity('PARSER datetime', valid);
                if (valid)
                {
                    console.log("PARSER Is Valid " + new Date(m.valueOf()));
                    return new Date(m.valueOf());
                }
                else
                {
                    console.log("PARSER Is not valid " + value);
                    return value;
                }
              
            }

            function formatter(value) {
                console.log("FORMATTER value is " + value);
                var m = moment(value);
                var valid = m.isValid();
                if (valid) {
                    console.log("FORMATTER Is Valid " +m.format("YYYY/MM/DD"));
                    return m.format("YYYY/MM/DD");
                }
                else {
                    console.log("FORMATTER Is not valid " + value);
                    return value;
                }

            }

        } //link
    };

}); //appDatetime


app.config(function ($routeProvider) {

    $routeProvider.when("/home", {
        controller: "homeController",
        templateUrl: "/app/views/home.html"
    });

    $routeProvider.when("/login", {
        controller: "loginController",
        templateUrl: "/app/views/login.html"
    });

    $routeProvider.when("/signup", {
        controller: "signupController",
        templateUrl: "/app/views/signup.html"
    });

    $routeProvider.when("/settlement", {
        controller: "settlementController",
        templateUrl: "/app/views/settlement.html"
    });
    $routeProvider.when("/settlement-details/:id", {
        controller: "settlementDetailsController",
        templateUrl: "/app/views/settlement-details.html"
    });
    $routeProvider.when("/settlement-details", {
        controller: "settlementDetailsController",
        templateUrl: "/app/views/settlement-details.html"
    });
    
    $routeProvider.otherwise({ redirectTo: "/home" });
});
app.config(function ($httpProvider) {
    $httpProvider.interceptors.push('authInterceptorService');
});

app.run(['authService', function (authService) {
    authService.fillAuthData();
}]);