var ListProController = function ($scope, MyFactory) {
    $('.nav-tabs a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    })
    $scope.pageSize = 10;
    $scope.currentPage = 1;
    $scope.LIST_PRODUCTS = undefined;
    function getData() {
        MyFactory.GET(MyFactory.HOST_NAME + MyFactory.API.PRODUCTS).then(function (response) {
            $scope.LIST_CATEGORIES = [];
            if (response.err === 1)
                $scope.LIST_PRODUCTS = response.data;
            else show_message_box(response);
        })
    }
    getData();
    $scope.delete = function (id) {
        $scope.proId = id;
    }
    $scope.do_del = function () {
        MyFactory.DELETE(MyFactory.HOST_NAME + MyFactory.API.PRODUCTS + 'delete/' + $scope.proId).then(function (response) {
            if (response.err === 1) {
                var msg = 'Xóa sản phẩm thành công';
                MyFactory.MESSAGE_SUCCESS(msg);
                $('.modal').modal('hide');
                $scope.LIST_PRODUCTS = response.data;
            }
            else show_message_box(response);
        })
    }
    function show_message_box(data) {
        if (data.err === 0) {
            var msg = 'Xóa sản phẩm không thành công';
            MyFactory.MESSAGE_FAIL(msg);
            $('.modal').modal('hide');
        }
        else if (data.err === -2) {
            $('#login-form').modal({
                show: true,
                backdrop: 'static'
            })
        }
        else {
            $rootScope.Error = data;
            $('#message-box').modal({
                show: true,
                backdrop: 'static'
            })
        }
    }
}
ListProController.$inject = ['$scope', 'MyFactory'];