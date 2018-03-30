(function () {
    'use strict';

    angular
        .module('eDictionaryBookMainModule')
        .controller("EmailController", EmailController);

    EmailController.$inject = ['$scope', '$http', '$window'];

    function EmailController($scope, $http, $window) {

        var vm = this;
        vm.load = true;
        vm.ccList = [];
        vm.To = '';
        vm.Body = '';
        vm.Subject = '';
        //this is email model which is send to c# method (send method)
        vm.email =
            {
            To: vm.To,
            Body: vm.Body,
            Subject: vm.Subject,
            ccList: vm.ccList,
            email: vm.ccList,
            }
        
        vm.addCC = addCC;
        vm.sendData = sendData;
        //we add ccusers to message
        function addCC(index) {
            vm.ccList.push({Email: '' }); //we add new input to send form
        }

        function sendData() {
            vm.load = false; //flag which mean that we dont load data yet
            //this is email model which is send to c# method (send method)
            vm.email =
            {
                To: vm.To,
                Body: vm.Body,
                Subject: vm.Subject,
                ccList: vm.ccList,
            }
            $http.post("/Email/Send/SendEmail", vm.email).then(function (msg) {
                
            }).finally(
                function (response) {
                    vm.load = true; //flag is set to true when data is loaded
                    $window.location.href = '/Email/Receive/ReceiveEmail' //redirect to receive emails
                });
        }
    }
})();