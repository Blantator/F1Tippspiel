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
	            templateUrl: '/home/game'
	        })
	        .when('/delete/:customerId',
	        {
	            controller: 'HomeController',
	            templateUrl: '/home/delete'
	        })
	        .when('/edit/:customerId',
	        {
	            controller: 'HomeController',
	            templateUrl: '/home/edit'
	        })
	        .otherwise({ redirectTo: '/' });
}]);