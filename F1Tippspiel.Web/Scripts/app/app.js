var services = angular.module('tippspiel.services', []);
var controllers = angular.module('tippspiel.controllers', []);
var config = angular.module('tippspiel.config', []);

var tippspiel = angular.module('tippspiel', 
	[
		'ngRoute',
		'LocalStorageModule',
		'angular-loading-bar',
		'tippspiel.services',
		'tippspiel.controllers',
		'tippspiel.config',
		'ui.bootstrap'
	]);

tippspiel.run(['$rootScope', '$location', 'authService', function ($rootScope, $location, authService) {
	authService.fillAuthData();

	var _logout = function () {
		authService.logout();
		$location.path('/');
	};
	$rootScope.logout = _logout;
}]);