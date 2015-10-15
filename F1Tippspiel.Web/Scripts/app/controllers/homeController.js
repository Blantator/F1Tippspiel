angular.module('tippspiel.controllers')
	.controller('homeController', ['$rootScope', '$scope', '$location', 'authService', function ($rootScope, $scope, $location, authService) {

		$scope.alert = {
			available: false,
			message: '',
			type: ''
		};
		$scope.registerAlert = {
			available: false,
			message: '',
			type: ''
		};
		$scope.resetPasswordAlert = {
			available: false,
			message: '',
			type: ''
		};

		$rootScope.siteTitle = "Willkommen";
		$rootScope.authentication = authService.authentication;

		function init() {
			if (authService.authentication.isAuthenticated) {
				$location.path('/game');
			}
			$rootScope.currentRoute = _getArea($location.path());
		}

		var _tryLogin = function (loginModel) {
			$scope.alert = {
				available: false,
				message: '',
				type: 'alert-danger'
			};
			var loginData = {
				userName: loginModel.email,
				password: loginModel.password
			};

			authService.login(loginData)
			.then(function (response) {
				$rootScope.authentication = authService.authentication;
				$location.path('/game');
			}, function (error) {
				$scope.alert = {
					available: true,
					message: error.error_description,
					type: 'alert-danger'
				};
			});
		};

		var _tryRegister = function (registerModel) {
			$scope.registerAlert = {
				available: false,
				message: '',
				type: ''
			};

			authService.registerNewAccount(registerModel)
			.then(function (response) {
				$scope.registerAlert = {
					available: true,
					message: 'Die registrierung war erfolgreich, Sie können sich nun einloggen.',
					type: 'alert-success'
				};
				$scope.registerModel = {
					displayname: '',
					email: '',
					password: '',
					password2: ''
				};
			}, function (error) {
				var errors = [];
				for (var key in error.data.modelState) {
					for (var i = 0; i < error.data.modelState[key].length; i++) {
						errors.push(error.data.modelState[key][i]);
					}
				}
				$scope.registerAlert = {
					available: true,
					message: 'Die registrierung konnte nicht abgeschlossen werden: ' + errors.join(' '),
					type: 'alert-danger'
				};
			});
		};

		var _tryResetPassword = function (resetPassModel) {
			$scope.resetPasswordAlert = {
				available: false,
				message: '',
				type: ''
			};
			authService.resetpassword(resetPassModel.resetmail)
			.then(function (response) {
				$scope.resetPasswordAlert = {
					available: true,
					message: 'Ihr Passwort wurde erfolgreich zurückgesetzt, Sie bekommen in den nächsten Minuten eine E-Mail mit Ihrem neuen Passwort!',
					type: 'alert-success'
				};
				$scope.resetPassModel = { resetmail: '' };
			}, function (error) {
				$scope.resetPasswordAlert = {
					available: true,
					message: 'Passwort konnte nicht zurückgesetzt werden, entweder wurde die E-Mail Adresse nicht gefunden oder es gab ein anderes Problem!',
					type: 'alert-danger'
				};
			});
		};

		/**
		 * determine the area of the application the user is currently in
		 * to hilight the menu link on the interface accordingly
		 */
		function _getArea(path) {
			 if (path.startsWith('/')) {
				return 'root';
			}
			return '/';
		};

		init();
		$scope.tryLogin = _tryLogin;
		$scope.tryRegister = _tryRegister;
		$scope.tryResetPassword = _tryResetPassword;
	}
]);