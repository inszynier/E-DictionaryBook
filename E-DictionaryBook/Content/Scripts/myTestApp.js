

       var app = angular.module('myTestApp', []);
        app.controller('myTestController', myFunction);

        function myFunction($scope) {
            $scope.firstName = "John";
            $scope.lastName = "Doe";
        }    
    

