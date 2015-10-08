angular.module('tippspiel.controllers')
	.controller('homeController', ['$scope', '$location', function ($scope, $routeParams, $location) {

	    function redirectIfLoggedIn() {
	        if (typeof $scope.authToken !== 'undefined') {
	            //user is logged in -> redirect to game overview
	            $location.path('/game');
	        } else {

	        }
	        $scope.message = "hello from controller";
            $scope.siteTitle = "Tippspiel 2016";
	    }

	    function init() {
	        redirectIfLoggedIn();
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