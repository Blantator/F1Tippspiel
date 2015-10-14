angular.module('tippspiel.config')
	.config(['$routeProvider', function ($routeProvider) {
	    $routeProvider
	        .when('/',
	        {
	            controller: 'homeController',
	            templateUrl: '/home/start'
	        })
	        .when('/game',
	        {
	            controller: 'gameController',
	            templateUrl: '/game/start'
	        })
			
	        .when('/rules',
	        {
	            controller: 'rulesController',
	            templateUrl: '/home/rules'
	        })
	        .when('/edit/:customerId',
	        {
	            controller: 'HomeController',
	            templateUrl: '/game/edit'
	        })
	        .otherwise({ redirectTo: '/' });
}]);