'use strict';
angular.module('tippspiel.services')
	.factory('authService', ['$http', '$q', 'localStorageService', function ($http, $q, localStorageService) {
		var clientId = "postman";
		var serviceBase = "http://localhost:50897/";
		var authServiceFactory = {};

		var _authentication = {
			isAuthenticated: false,
			userName: ""
		};

		var _registerNewAccount = function (newAccount) {
			_logout();

			return $http.post(serviceBase + 'api/account/register', newAccount)
				.then(function (response) {
					return response;
				});
		};

		var _login = function (loginData) {
			var data = "grant_type=password&username=" + loginData.userName + "&password=" + loginData.password + "&client_id=" + clientId;

			var deferred = $q.defer();

			$http.post(serviceBase + "api/token", data, {
				headers: {
					"Content-Type": "application/x-www-form-urlencoded"
				}
			}).success(function (response) {
				localStorageService.set("authorizationData", {
					token: response.access_token,
					refresh_token: response.refresh_token,
					expires_in: response.expires_in,
					userName: loginData.userName
				});

				_authentication.isAuthenticated = true;
				_authentication.userName = loginData.userName;

				deferred.resolve(response);
			}).error(function (err, status) {
				_logout();
				deferred.reject(err);
			});

			return deferred.promise;
		};

		var _logout = function () {
			localStorageService.remove("authorizationData");
			_authentication.isAuthenticated = false;
			_authentication.userName = "";
		};

		var _resetPassword = function (emailAddress) {
			_logout();

			return $http.post(serviceBase + 'api/account/resetpassword/', { email: emailAddress })
				.then(function (response) {
					return response;
				});
		};

		var _fillAuthData = function () {
			var authData = localStorageService.get("authorizationData");
			if (authData) {
				_authentication.isAuthenticated = true;
				_authentication.userName = authData.userName;
			}
		};

		authServiceFactory.registerNewAccount = _registerNewAccount;
		authServiceFactory.login = _login;
		authServiceFactory.logout = _logout;
		authServiceFactory.fillAuthData = _fillAuthData;
		authServiceFactory.authentication = _authentication;
		authServiceFactory.resetpassword = _resetPassword;

		return authServiceFactory;
	}]);