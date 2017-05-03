var CategoriesController = function ($scope, $location, MyFactory) {
    $('.nav-tabs a').click(function (e) {
        e.preventDefault();
        $(this).tab('show');
    })
    $scope.pageTitle = '';
    $scope.pageSize = 10;
    $scope.currentPage = 1;
    $scope.Type = 0;
    $scope.LIST_CATEGORIES = undefined;
    $scope.list_cate_checked = [];
    $scope.options = MyFactory.CKEDITOR_OPTIONS;
    $scope.main_content = '';

    function control() {
        $scope.Name = '';
        $scope.Meta = '';
        $scope.cateParent = undefined;
    }
    function getData(cateType) {
        MyFactory.GET(MyFactory.HOST_NAME + MyFactory.API.CATEGORIES + 'get/' + cateType).then(function (response) {
            $scope.LIST_CATEGORIES = [];
            if (response.err === 1)
                $scope.LIST_CATEGORIES = response.data;
            else show_message_box(response);
        });
    }

    $scope.detail = function (item) {
        $scope.item_cate = item;
        $scope.image = item.Image;
        $scope.cateParent = $scope.LIST_CATEGORIES.find(function (d) { return d.ID === item.Parent });
    }
    $scope.post = function (type) {
        var data = {};
        if (type === 1) {
            data.ID = getMaxId();
            data.Name = $scope.cateName;
            data.Meta = $scope.cateMeta;
            data.Status = true;
            data.Type = $scope.Type;
            data.Parent = ($scope.cateParent !== undefined) ? $scope.cateParent.ID : 0;
            data.Image = $scope.image;
        }
        else {
            data = $scope.item_cate;
            data.Parent = ($scope.cateParent !== undefined) ? $scope.cateParent.ID : 0;
            data.Image = $scope.image;
        }
        if (data.Parent !== data.ID) {
            MyFactory.POST(MyFactory.HOST_NAME + MyFactory.API.CATEGORIES + 'post/' + type, data).then(function (response) {
                check(response, type);
            })
        }
        else {
            alert('Danh mục không thể là con của chính nó!');
        }
    }
    $scope.delete = function (id) {
        $scope.proId = id;
    }
    $scope.do_del = function () {
        MyFactory.DELETE(MyFactory.HOST_NAME + MyFactory.API.CATEGORIES + 'delete/' + $scope.proId).then(function (response) {
            check(response, 3);
        })
    }

    $scope.getMetaByName = function (name) {
        $scope.cateMeta = MyFactory.FORMART_TITLE_TO_SLUG(name);
    }
    $scope.getItemMetaByItemName = function (itemName) {
        $scope.item_cate.Meta = MyFactory.FORMART_TITLE_TO_SLUG(itemName);
    }

    $scope.BrowseServer = function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (url) {
            $scope.image = url;
            $scope.$apply();
        };
        finder.popup();
    }
    $scope.displayItemsPerPage = function (value) {
        $scope.pageSize = parseInt(value);
        $scope.$apply();
    }
    function getMaxId() {
        if ($scope.LIST_CATEGORIES.length > 0)
            return $scope.LIST_CATEGORIES[0].ID + 1;
        return 1;
    }

    var url = $location.path();
    if (url === '/admin/categories-product') {
        $scope.Type = 1;
        $scope.pageTitle = 'Danh mục sản phẩm';
    }
    else if (url = '/admin/categories-article') {
        $scope.Type = 2;
        $scope.pageTitle = 'Danh mục bài viết';
    }
    else if (url = '/admin/categories-slideshow') {
        $scope.Type = 3;
        $scope.pageTitle = 'Slideshow';
    }
    getData($scope.Type);
    $scope.checkAll = function () {
        var countList = $scope.LIST_CATEGORIES.length;
        for (var i = 0; i < countList; i++) {
            $scope.list_cate_checked[i] = $scope.LIST_CATEGORIES[i].ID;
        }
    }
    $scope.checkItem = function (ID,checked) {
        var cateItem = $scope.list_cate_checked.find(function (d) { return d.ID === ID });
        if (cateItem !== undefined && checked) {
            $scope.list_cate_checked.splice(cateItem, 1);
        }
        else {
            var countList = $scope.list_cate_checked.length;
            $scope.list_cate_checked[countList] = ID;
        }
    }
    $scope.isChecked = function (ID) {
        var cateItem = $scope.list_cate_checked.indexOf(ID);
        if (cateItem > -1) {
            return true;
        }
        else {
            return false;
        }
    }
    $scope.actionChecked = function (value) {
        var data = { list: $scope.list_cate_checked, type: $scope.Type };
        if (value === '1')
            MyFactory.POST(MyFactory.HOST_NAME = MyFactory.API.CATEGORIES + 'UpdateMultiRecords', data).then(function (response) {
                if (response.err === 1) {
                    $scope.LIST_CATEGORIES = response.data;
                }
                else console.log('Action fail!');
            })
        else {

        }
    }
    function check(response, type) {
        if (response.err === 1) {
            $scope.LIST_CATEGORIES = response.data;
            $scope.image = '';
            
            if (type === 1) {
                control();
                $('#insert').modal('hide');
                var message = "Thêm danh mục sản phẩm thành công!";
                MyFactory.MESSAGE_SUCCESS(message);
            }
            else if (type === 2) {
                $('#update').modal('hide');
                var message = "Cập nhật danh mục sản phẩm thành công!";
                MyFactory.MESSAGE_SUCCESS(message);
            }
            else {
                $('#delete').modal('hide');
                var message = "Xóa danh mục sản phẩm thành công!";
                MyFactory.MESSAGE_SUCCESS(message);
            }
        }
        else if (response.err === 0) {
            if (type === 1) {
                $('#insert').modal('hide');
                var message = "Thêm danh mục sản phẩm không thành công!";
                MyFactory.MESSAGE_FAIL(message);
            }
            else if (type === 2) {
                $('#update').modal('hide');
                var message = "Cập nhật danh mục sản phẩm không thành công!";
                MyFactory.MESSAGE_FAIL(message);
            }
            else {
                $('#delete').modal('hide');
                var message = "Xóa danh mục sản phẩm không thành công!";
                MyFactory.MESSAGE_FAIL(message);
            }
        }
        else {
            show_message_box(response);
        }
    }
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
CategoriesController.$inject = ['$scope', '$location', 'MyFactory'];