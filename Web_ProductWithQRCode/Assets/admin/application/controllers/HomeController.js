var HomeController = function ($rootScope, $scope, $window, MyFactory, md5) {
    $('#birthday').datepicker({
        format: 'dd-mm-yyyy'
    })
    $rootScope.accountSession = {};
    $scope.isDisabled = true;
    MyFactory.GET(MyFactory.HOST_NAME + MyFactory.API.LOGIN).then(function (response) {
        if (response) {
            $rootScope.accountSession = response;
            $scope.gender = $rootScope.accountSession.Gender;
            var ngaysinh = $rootScope.accountSession.Birthday.substr(0, 10);
            $scope.NgaySinh = MyFactory.FORMAT_STRING_DATE(ngaysinh);
        }  
    });
    $scope.showInfo = function () {
        $('#info-staff-form').modal('show');
    }
    $scope.showUpdateFrm = function () {
        $('#update-staff-form').modal('show');
    }
    $scope.showInforDev = function () {
        $('#infor-developer').modal('show');
    }
    $scope.updateStaff = function () {
        $rootScope.accountSession.MatKhau = '';
        $rootScope.accountSession.TenDangNhap = '';
        $rootScope.accountSession.NgaySinh = MyFactory.FORMAT_STRING_DATE($scope.NgaySinh);
        MyFactory.POST(MyFactory.HOST_NAME + MyFactory.API.USER + "post/2", $rootScope.accountSession).then(function () {
            MyFactory.MESSAGE_SUCCESS('Cập nhật thành công!!');
        });
    }
    $scope.reLogin = function (data) {
        var data = {
            username: $scope.username,
            password: md5.createHash($scope.password)
        }
        MyFactory.POST(MyFactory.HOST_NAME + '/api/login/posttologin', data).then(function (response) {
            if (response.ErrorCode === 1) {
                $window.location.reload();
            }
            else {
                $scope.errorAccount = response.ErrorMessage;
            }
        })
    }
}
HomeController.$inject = ['$rootScope', '$scope', '$window', 'MyFactory', 'md5'];