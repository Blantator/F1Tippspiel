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
		}

		init();

		//$scope.addCustomer = function () {
		//    var customer = {};
		//    customer.id = 0;
		//    customer.name = $scope.customer.name;
		//    customer.city = $scope.customer.city;
		//    customerFactory.addCustomer(customer);
		//};

		//$scope.deleteCustomer = function () {
		//    customerFactory.deleteCustomer($routeParams.customerId);
		//};
	}
]);