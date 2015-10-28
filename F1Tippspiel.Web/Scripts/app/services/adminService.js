'use strict';
angular.module('tippspiel.services')
	.factory('adminService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {
		var clientId = "postman";
		var serviceBase = "http://localhost:50897/";
		var adminServiceFactory = {};

		var _getConfiguredDrivers = function (loginData) {
			
			return $http.get(serviceBase + "api/admin/drivers");
			
		};

		var _getConfiguredTeams = function (loginData) {

			return $http.get(serviceBase + "api/admin/teams");

		};

		var _getConfiguredTracks = function (loginData) {

			return $http.get(serviceBase + "api/admin/tracks");

		};

		adminServiceFactory.getConfiguredDrivers = _getConfiguredDrivers;
		adminServiceFactory.getConfiguredTeams = _getConfiguredTeams;
		adminServiceFactory.getConfiguredTracks = _getConfiguredTracks;

		return adminServiceFactory;
	}]);