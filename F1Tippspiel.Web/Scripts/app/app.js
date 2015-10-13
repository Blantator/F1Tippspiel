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

tippspiel.run(['authService', function (authService) {
	authService.fillAuthData();
}]);