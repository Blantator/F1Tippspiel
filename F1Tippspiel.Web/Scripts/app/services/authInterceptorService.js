'use strict';
angular.module('tippspiel.services')
	.factory('authInterceptorService', ['$q', '$location', 'localStorageService', function ($q, $location, localStorageService) {
		var authInterceptorServiceFactory = {};

		//Intercept every request to the server and add the bearer token
		//to the authorization header if available
		var _request = function (config) {
			config.headers = config.headers || {};

			var authData = localStorageService.get("authorizationData");
			if (authData) {
				config.headers.Authorization = "Bearer " + authData.token;
			}
			return config;
		};

		//intercept error responses and redirect user to login view
		var _responseError = function (rejection) {
			if (rejection.status === 401) {
				$location.path("#/");
			}
			return $q.reject(rejection);
		};

		authInterceptorServiceFactory.request = _request;
		authInterceptorServiceFactory.responseError = _responseError;

		return authInterceptorServiceFactory;
	}]);