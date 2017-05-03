var app = angular.module('MyApp', ['ngRoute', 'angularUtils.directives.dirPagination', 'ngFlash', 'switcher', 'angular-md5', 'angucomplete', 'checklist-model', 'ckeditor','ja.qr']);
app.controller('HomeController', HomeController);
app.controller('CategoriesController', CategoriesController);
app.controller('ListProController', ListProController);
app.controller('NewProController', NewProController);
app.controller('UpdateProController', UpdateProController);
app.controller('UsersController', UsersController);

app.factory('MyFactory', MyFactory);
app.directive('format', format);

var configFuntion = function ($routeProvider, $locationProvider, ChartJsProvider) {
    $locationProvider.html5Mode(true);
    $routeProvider
    .when('/admin', {
        templateUrl: 'Templates/LoadPartialView/Admin/Home',
        controller:'HomeController'
    })
    .when('/admin/categories-product', {
        templateUrl: 'Templates/LoadPartialView/Admin/categories',
        controller: 'CategoriesController'
    })
    .when('/admin/list-products', {
        templateUrl: 'Templates/LoadPartialViewSubFolder/Admin/products/list',
        controller: 'ListProController'
    })
    .when('/admin/new-product', {
        templateUrl: 'Templates/LoadPartialViewSubFolder/Admin/products/new',
        controller: 'NewProController'
    })
    .when('/admin/update-product/:proID', {
        templateUrl: 'Templates/LoadPartialViewSubFolder/Admin/products/update',
        controller: 'UpdateProController'
    })
    .when('/admin/users', {
        templateUrl: 'Templates/LoadPartialView/Admin/users',
        controller: 'UsersController'
    });
}
configFuntion.$inject = ['$routeProvider', '$locationProvider'];
app.config(configFuntion);