var UsersController = function ($rootScope, $scope, MyFactory, md5) {
    $('#birthday, #birthdayUP').datepicker({
        format: 'dd-mm-yyyy'
    })
    function control() {
        $scope.pageSize = 5;
        $scope.status = true;
        $scope.name = '';
        $scope.gender = true;
        $scope.birthday = MyFactory.TODAY();
        $scope.phone = '';
        $scope.username = null;
        $scope.password = null;
        $scope.currentPage = 1;
        $scope.usertype = undefined;
    }
    function list() {
        $scope.LIST_USER = undefined;
        $scope.item_user = {};
        $scope.item_user.birthday = '';
        $scope.LIST_GROUPS_USER = [];
    }
    $scope.getData = function () {
        MyFactory.GET(MyFactory.HOST_NAME + MyFactory.API.USERS + 'get').then(function (response) {
            $scope.LIST_USER = [];
            if (response.err === 1) {
                $scope.LIST_USER = response.data;
            }
            else show_message_box(response)
        });
        MyFactory.GET(MyFactory.HOST_NAME + MyFactory.API.USERS + "GetListGroupsUser/").then(function (response) {
            if (response.err === 1)
                $scope.LIST_GROUPS_USER = response.data;
            else console.log(response);
        });
    }
    control();
    list();
    $scope.getData();

    $scope.detail = function (item) {
        $scope.status = item.status;
        $scope.gender = item.gender;
        $scope.item_user = item;
        $scope.group = $scope.LIST_GROUPS_USER.find(function (d) { return d.ID === item.groupId });
        $scope.gender = item.gender;
        $('#pass').val(item.password);
        $scope.item_user.birthday = $scope.item_user.birthday.split('T')[0];
        $scope.item_user.birthday = MyFactory.FORMAT_STRING_DATE($scope.item_user.birthday);
    }
    $scope.post = function (type) {
        var data = {};
        if (type === 1) {
            data.Name = $scope.fullname;
            data.Gender = $scope.gender;
            data.Phone = $scope.phone;
            data.Birthday = MyFactory.FORMAT_STRING_DATE($scope.birthday);
            data.Status = $scope.status;
            data.Username = $scope.username;
            data.Password = md5.createHash($scope.password);
            data.GroupID = $scope.group.ID;
        }
        else {
            data = $scope.item_user;
            data.Status = $scope.item_user.status;
            data.Gender = $scope.gender;
            data.GroupID = $scope.group.ID;
            data.Birthday = MyFactory.FORMAT_STRING_DATE(data.birthday);
        }
        MyFactory.POST(MyFactory.HOST_NAME + MyFactory.API.USERS + "post/" + type, data).then(function (response) {
            check(response, type);
        })
    }
    $scope.delete = function (id) {
        $scope.MaNhanVien = id;
    }
    $scope.do_del = function () {
        MyFactory.DELETE(MyFactory.HOST_NAME + MyFactory.API.USER + "delete/" + $scope.MaNhanVien).then(function (response) {
            check(response, 3);
        })
    }
    function check(response, type) {
        if (response.err === 1) {
            $scope.LIST_USER = response.data;

            if (type === 1) {
                control();
                $('#insert').modal('hide');
                var message = "Thêm nhân viên thành công!";
                MyFactory.MESSAGE_SUCCESS(message);
            }
            else if (type === 2) {
                $('#update').modal('hide');
                var message = "Cập nhật nhân viên thành công!";
                MyFactory.MESSAGE_SUCCESS(message);
            }
            else {
                $('#delete').modal('hide');
                var message = "Xóa nhân viên thành công!";
                MyFactory.MESSAGE_SUCCESS(message);
            }
        }
        else if (response.err === 0) {
            if (type === 1) {
                $('#insert').modal('hide');
                var message = response.msg;
                MyFactory.MESSAGE_FAIL(message);
            }
            else if (type === 2) {
                $('#update').modal('hide');
                var message = response.msg;
                MyFactory.MESSAGE_FAIL(message);
            }
            else {
                $('#delete').modal('hide');
                var message = response.msg;
                MyFactory.MESSAGE_FAIL(message);
            }
        }
        else {
            show_message_box(response);
        }
    }
    $scope.displayItemsPerPage = function (value) {
        $scope.pageSize = parseInt(value);
        $scope.$apply();
    }

    $('#datepicker').datepicker({
        format: 'dd-mm-yyyy',
        defaultDate: MyFactory.TODAY()
    });
    function show_message_box(data) {
        if (data.err === -2) {
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
UsersController.$inject = ['$rootScope', '$scope', 'MyFactory', 'md5'];