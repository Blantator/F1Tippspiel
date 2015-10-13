angular.module('tippspiel.config')
	.config(['$httpProvider', function ($httpProvider) {
		$httpProvider.interceptors.push('authInterceptorService');
	}]);