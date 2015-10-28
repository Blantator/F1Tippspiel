angular.module('tippspiel.controllers')
	.controller('gameController', ['$rootScope', '$scope', '$location', '$routeParams', 'authService', 'seasonService', 
						  function ($rootScope,   $scope,   $location,   $routeParams,   authService,   seasonService) {

		$rootScope.siteTitle = "Tippspiel 2016";
		$rootScope.authentication = authService.authentication;

		$scope.playerStandings = [];
		$scope.isLoadingPlayerStandings = false;

		function _redirectIfNotLoggedIn() {
			if (!authService.authentication.isAuthenticated) {
				$location.path('/');
			}
		}

		function init() {
			_redirectIfNotLoggedIn();
			$rootScope.currentRoute = _getArea($location.path());
			_loadPlayerStandings();
		}

		function _loadPlayerStandings() {
			$scope.isLoadingPlayerStandings = true;

			seasonService.getCurrentPlayerStandings()
			.then(function (response) {
				$scope.isLoadingPlayerStandings = false;
				$scope.playerStandings = response.data;
			}, function (err, status) {
				$scope.isLoadingPlayerStandings = false;
				console.log("fehler: " + err.data.message);
			});
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
		$scope.refreshPlayerStandings = _loadPlayerStandings;
	}
]);