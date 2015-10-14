angular.module('tippspiel.controllers')
	.controller('gameController', ['$rootScope', '$scope', '$location', '$routeParams', 'authService', function ($rootScope, $scope, $location, $routeParams, authService) {

		$rootScope.siteTitle = "Tippspiel 2016";
		$rootScope.authentication = authService.authentication;

		function redirectIfNotLoggedIn() {
			if (!authService.authentication.isAuthenticated) {
				$location.path('/');
			}
		}

		function init() {
			redirectIfNotLoggedIn();
			$rootScope.currentRoute = _getArea($location.path());
		}

		var _logout = function () {
			authService.logout();
			$location.path('/');
		};


		/**
		 * determine the area of the application the user is currently in
		 * to hilight the menu link on the interface accordingly
		 */
		function _getArea(path) {
			if (path.startsWith('/game')) {
				return 'game';
			} else if (path.startsWith('/')) {
				return 'root';
			}
			return '/';
		};

		init();
		$rootScope.logout = _logout;
	}
]);