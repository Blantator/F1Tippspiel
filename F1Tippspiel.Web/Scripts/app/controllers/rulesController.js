angular.module('tippspiel.controllers')
	.controller('rulesController', ['$rootScope', '$scope', '$location', '$routeParams', 'authService', function ($rootScope, $scope, $location, $routeParams, authService) {

		$rootScope.siteTitle = "Spielregeln";
		$rootScope.authentication = authService.authentication;

		function init() {
			$rootScope.currentRoute = _getArea($location.path());
		}
		
		/**
		 * determine the area of the application the user is currently in
		 * to hilight the menu link on the interface accordingly
		 */
		function _getArea(path) {
			if (path.startsWith('/rules')) {
				return 'rules';
			} else if (path.startsWith('/')) {
				return 'root';
			}
			return '/';
		};

		init();
	}
]);