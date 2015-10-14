angular.module('tippspiel.controllers')
	.controller('homeController', ['$rootScope', '$scope', '$location', 'authService', function ($rootScope, $scope, $location, authService) {

		$scope.alert = {
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
	}
]);