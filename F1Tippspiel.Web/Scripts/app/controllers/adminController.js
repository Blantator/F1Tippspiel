angular.module('tippspiel.controllers')
	.controller('adminController', ['$rootScope', '$scope', '$location', '$routeParams', 'authService', 'adminService', 
						   function ($rootScope,   $scope,   $location,   $routeParams,   authService,   adminService) {

		$rootScope.siteTitle = "Administration";
		$rootScope.authentication = authService.authentication;

		$scope.isLoadingConfiguredDrivers = false;
		$scope.isLoadingConfiguredTeams = false;
		$scope.isLoadingConfiguredTracks = false;

		function _redirectIfNotLoggedIn() {
			if (!authService.authentication.isAuthenticated) {
				$location.path('/');
			}
		}

		function init() {
			_redirectIfNotLoggedIn();
			$rootScope.currentRoute = _getArea($location.path());

			_loadConfiguredDrivers();
			_loadConfiguredTeams();
			_loadConfiguredTracks();
		}

		function _loadConfiguredDrivers() {
			$scope.isLoadingConfiguredDrivers = true;

			adminService.getConfiguredDrivers()
			.then(function (response) {
				$scope.isLoadingConfiguredDrivers = false;
				$scope.configuredDrivers = response.data;
			}, function (err, status) {
				$scope.isLoadingConfiguredDrivers = false;
				console.log("fehler: " + err.data.message);
			});
		};

		function _loadConfiguredTeams() {
			$scope.isLoadingConfiguredTeams = true;

			adminService.getConfiguredTeams()
			.then(function (response) {
				$scope.isLoadingConfiguredTeams = false;
				$scope.configuredTeams = response.data;
			}, function (err, status) {
				$scope.isLoadingConfiguredTeams = false;
				console.log("fehler: " + err.data.message);
			});
		};

		function _loadConfiguredTracks() {
			$scope.isLoadingConfiguredTracks = true;

			adminService.getConfiguredTracks()
			.then(function (response) {
				$scope.isLoadingConfiguredTracks = false;
				$scope.configuredTracks = response.data;
			}, function (err, status) {
				$scope.isLoadingConfiguredTracks = false;
				console.log("fehler: " + err.data.message);
			});
		};

		var _showArea = function (area) {
			$rootScope.currentRoute = _getArea('/admin/' + area);
			$scope.openArea = area;
		};

		/**
		 * determine the area of the application the user is currently in
		 * to hilight the menu link on the interface accordingly
		 */
		function _getArea(path) {
			
			if (path.startsWith('/admin/reminders')) {
				$scope.currentAdminArea = "Erinnerungen";
				return 'admin';
			} else if (path.startsWith('/admin/players')) {
				$scope.currentAdminArea = "Spieler";
				return 'admin/players';
			} else if (path.startsWith('/admin/results')) {
				$scope.currentAdminArea = "Rennergebnisse";
				return 'admin/results';
			} else if (path.startsWith('/admin/qualifying')) {
				$scope.currentAdminArea = "Qualifyingergebnisse";
				return 'admin/qualifying';
			} else if (path.startsWith('/admin/tracks')) {
				$scope.currentAdminArea = "Strecken";
				return 'admin/tracks';
			} else if (path.startsWith('/admin/drivers')) {
				$scope.currentAdminArea = "Fahrer";
				return 'admin/drivers';
			} else if (path.startsWith('/admin/teams')) {
				$scope.currentAdminArea = "Rennställe";
				return 'admin/teams';
			} else if (path.startsWith('/admin/dates')) {
				$scope.currentAdminArea = "Termine";
				return 'admin/dates';
			} else if (path.startsWith('/admin')) {
				$scope.currentAdminArea = "Erinnerungen";
				return 'admin';
			}
			return '/';
		};

		init();
		$scope.showArea = _showArea;
	}
]);