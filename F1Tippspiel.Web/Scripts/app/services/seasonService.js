'use strict';
angular.module('tippspiel.services')
	.factory('seasonService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {
		var clientId = "postman";
		var serviceBase = "http://localhost:50897/";
		var seasonServiceFactory = {};

		var _getCurrentPlayerStandings = function (loginData) {
			
			return $http.get(serviceBase + "api/season/currentPlayerStandings");
			
		};

		seasonServiceFactory.getCurrentPlayerStandings = _getCurrentPlayerStandings;

		return seasonServiceFactory;
	}]);