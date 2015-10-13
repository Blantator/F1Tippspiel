angular.module('tippspiel.controllers')
	.controller('homeController', ['$rootScope', '$scope', '$location', '$routeParams', 'authService', function ($rootScope, $scope, $location, $routeParams, authService) {

		$rootScope.siteTitle = "Login";
		$rootScope.authentication = authService.authentication;

		function init() {
			if (authService.authentication.isAuthenticated) {
				$location.path('/game');
			}
		}

		var _tryLogin = function (loginModel) {
			var loginData = {
				userName: loginModel.email,
				password: loginModel.password
			};

			authService.login(loginData)
			.then(function (response) {
				$scope.authentication = authService.authentication;
				$location.path('/game');
			}, function (error) {
				alert("could not login!");
			});
		};

		init();
		$scope.tryLogin = _tryLogin;
	}
]);