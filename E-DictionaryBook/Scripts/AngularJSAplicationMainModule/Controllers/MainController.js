(function () {
    'use strict';

    angular
        .module('eDictionaryBookMainModule')
        .controller("MainController", mojaTestowaFuncja);

    mojaTestowaFuncja.$inject = ['$scope', '$q', 'resourceLanguage', '$timeout', '$http'];

    function mojaTestowaFuncja($scope, $q, resourceLanguage, $timeout, $http) {

        var vm = this;
        vm.load = true; //flag which mean that we dont load data yet
        vm.resLang = resourceLanguage; //service which is set to language version 
        vm.resource = '';
        vm.Idik = 1;
        //vm.loadingInit = false; //this flag is set to false, when we start loading data
        //inicjalizacja danymi z serwisu domyslnie jezyk angielski
        resourceLanguage.getLanguage(1).then(
            getItemsSuccess,
            getItemsFailure
        ).finally(function () {
            // called no matter success or failure
            vm.loading = true;
        });

        vm.changeLanguage = changeLanguage;

        function changeLanguage(Id) {
            //alert('change ladn ' + Id);
            vm.Idik = Id;
            vm.load = false;
            resourceLanguage.getLanguage(vm.Idik).then(
                getItemsSuccess,
                getItemsFailure
            ).finally(function () {
                //setTimeout will set this function to end od queqe of execustion
                setTimeout(function () {
                    vm.load = true;//flag is set to true when data is loaded
                    $scope.$apply();//when current cicle of life will be done then apply changes
                }, 0);
            });
        };

        // getAllItems() success handler
        function getItemsSuccess(items) {
            vm.resource = items;
        };

        // getAllItems() error handler
        function getItemsFailure(error) {
            console.log('failure - now me!');
            console.log(error);
        };

        }
})();