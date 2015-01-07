angular.module('tippspiel.controllers')
	.controller('homeController', ['$scope', '$location', function ($scope, $routeParams, $location) {

	    init();

	    function init() {
	        redirectIfLoggedIn();
	    }

	    function redirectIfLoggedIn(){
	    	if(typeof $scope.authToken !== 'undefined'){
	    		//user is logged in -> redirect to game overview
	    		$location.path('/game');
	    	} else {
	    	    
	    	}
	    	$scope.message = "hello from controller";
	    }

	    $scope.addCustomer = function () {
	        var customer = {};
	        customer.id = 0;
	        customer.name = $scope.customer.name;
	        customer.city = $scope.customer.city;
	        customerFactory.addCustomer(customer);
	    };

	    $scope.deleteCustomer = function () {
	        customerFactory.deleteCustomer($routeParams.customerId);
	    };
	}
]);