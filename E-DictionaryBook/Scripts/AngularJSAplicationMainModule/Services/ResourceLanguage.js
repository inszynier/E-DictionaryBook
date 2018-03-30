(function () {
    'use strict';
    angular
        .module('eDictionaryBookMainModule')
        .service("resourceLanguage", changeResourceLanguage);

    changeResourceLanguage.$inject = ['$q'];

    function changeResourceLanguage($q) {

        

        var _username = '';
        var hello = '';
        var resources = { };
        this.loadingLanguage = false;

        var resouceENG =
            {
                test: 'testEng',
                language: 'Language',
                languageENG: 'LanguageENG',
                languagePL: 'LanguagePL',
                account: 'Account',
                gradebook: 'Gradebook',
                accountSettings: 'Account settings',
                personalData: 'Personal data',
                finances: 'Finances',
                dormitory: 'Dormitory',
                schedule: 'Schedule',
                emailsMessages: 'Emails messages',
                sendEmail: 'Send Email',
                receiveEmails: 'Receive emails',
                other: 'Other',
                announcement: 'Announcement',
                notes: 'Notes',
                tasks: 'Tasks',
                createdBy: 'Created by'
            }

        var resoucePL =
            {
                test: 'testPl',
                language: 'Jezyk',
                languageENG: 'Jezyk angielski',
                languagePL: 'Jezyk polski',
                account: 'Konto',
                gradebook: 'Dziennik ocen',
                accountSettings: 'Ustawienia konta',
                personalData: 'PDane osobowe',
                finances: 'Finanse',
                dormitory: 'Akademik',
                schedule: 'Plan lekcji',
                emailsMessages: 'Skrzynka pocztowa',
                sendEmail: 'Wyslij email',
                receiveEmails: 'Odbierz emaile',
                other: 'Inne',
                announcement: 'Ogloszenia',
                notes: 'Notatki',
                tasks: 'Zadania',
                createdBy: 'Stworzone przez'
            }

        this.printHello = function(hello)
        {
            return hello;
        }
        
        this.getLanguage = function (Id) {

            //this.loadingLanguage = false; //when we start loading data we set loadingLanguage to false
            //alert('loading w sersisie ' + this.loadingLanguage);
            var deferred = $q.defer();
           
            if (Id === 0) {
                
                resources = resoucePL;
                //setTimeout(function () { alert("Hello pl"); }, 3000);
                //alert('respl: ' + JSON.stringify(resources));
                deferred.resolve(resources);
            }
            if (Id === 1) {
                resources = resouceENG;
                //setTimeout(function () { alert("Hello eng"); }, 3000);
                deferred.resolve(resources);
            }
            deferred.reject('Error retriving Items');

            return deferred.promise;
        }
    }
})();

