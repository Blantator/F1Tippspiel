angular.module('tippspiel.config')
	.config(['$routeProvider', function ($routeProvider) {
	    $routeProvider
	        .when('/',
	        {
	            controller: 'gameController',
	            templateUrl: '/game/start'
	        })
	        .when('/game',
	        {
	            controller: 'gameController',
	            templateUrl: '/game/game'
	        })
	        .when('/delete/:customerId',
	        {
	            controller: 'HomeController',
	            templateUrl: '/game/delete'
	        })
	        .when('/edit/:customerId',
	        {
	            controller: 'HomeController',
	            templateUrl: '/game/edit'
	        })
	        .otherwise({ redirectTo: '/' });
}]);