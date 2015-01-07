var services = angular.module('tippspiel.services', []);
var controllers = angular.module('tippspiel.controllers', []);
var config = angular.module('tippspiel.config', []);

var tippspiel = angular.module('tippspiel', 
	[
		'ngRoute',
		'tippspiel.services',
		'tippspiel.controllers',
		'tippspiel.config',
		'ui.bootstrap'
	]);