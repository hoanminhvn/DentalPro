var NewProController = function ($rootScope, $scope, MyFactory) {
    $scope.LIST_CATEGORIES = undefined;
    $scope.options = MyFactory.CKEDITOR_OPTIONS;
    $scope.today = MyFactory.FORMAT_STRING_DATE(MyFactory.TODAY());
    $scope.langID = 1;
    function control() {
        $scope.Name = '';
        $scope.Intro = '';
        $scope.Content = '';
        $scope.Digital = '';
        $scope.Price = 0;
        $scope.Discount = 0;
        $scope.list_cate = [];
        $scope.imageList = [];
        $scope.Meta = '';
        $scope.keyword = [];
        $scope.image = "Assets/admin/images/No_image.png";
    }
    control();
    function getData(lang) {
        MyFactory.GET(MyFactory.HOST_NAME + MyFactory.API.CATEGORIES + 'get/' + 1).then(function (response) {
            $scope.LIST_CATEGORIES = [];
            if (response.err === 1) {
                $scope.LIST_CATEGORIES = response.data;
            }
            else show_message_box(response);
        });
    }
    function concatImageFromArr(arrImg) {
        var result = '';
        for (var i = 0; i < arrImg.length; i++) {
            if (i === arrImg.length - 1)
                result += arrImg[i];
            else
                result += arrImg[i] + ':';
        }
        return result;
    };

    $scope.insert = function () {
        var image = concatImageFromArr($scope.imageList);
        var data = {
            Name: $scope.Name,
            Meta: $scope.Meta,
            Intro: $scope.Intro,
            CateID: '',
            Digital: $scope.Digital,
            Content: $scope.Content,
            Status: parseInt($('#status').val()),
            Image: $scope.image,
            Lang: $scope.langID,
            CreateDate: $scope.today,
            CreateBy: $rootScope.accountSession.ID,
            Views: 0,
            Price: $scope.Price,
            Discount: $scope.Discount,
            ListImage: image,
            QRCode: '',
            Tag:''
        }
        var countList = $scope.list_cate.length;
        for (var i = 0; i < countList; i++) {
            if (i === 0)
                data.CateID += ':' + $scope.list_cate[i].ID + ':';
            else data.CateID += $scope.list_cate[i].ID + ':';
        }
        for (var i = 0; i < $scope.keyword.length; i++) {
            if (i === 0)
                data.Tag += ':' + $scope.keyword[i].name + '|' + $scope.keyword[i].meta + ':';
            else data.Tag += $scope.keyword[i].name + '|' + $scope.keyword[i].meta + ':';
        }
        var qrcode_img = document.getElementById('qrcode-img').src;
        data.QRCode = qrcode_img;
        if (data.Price > data.Discount && data.Discount !== undefined) {
            MyFactory.POST(MyFactory.HOST_NAME + MyFactory.API.PRODUCTS + 'post/1', data).then(function (response) {
                if (response.err === 1) {
                    var msg = 'Thêm sản phẩm thành công!';
                    MyFactory.MESSAGE_SUCCESS(msg);
                    control();
                    $(":checkbox").attr("checked", false);
                }
                else {
                    var msg = 'Thêm sản phẩm không thành công!';
                    MyFactory.MESSAGE_FAIL(msg);
                }
            });
        }
        else if (data.Discount !== undefined) alert('Vui lòng nhập giá khuyến mãi nhỏ hơn giá bán!');
    }
    $scope.comparePrice = function () {
        if ($scope.PRODUCT.Price <= $scope.PRODUCT.Discount)
            $scope.price_error = 'Giá khuyến mãi không được lớn hơn hoặc bằng giá bán!';
        else $scope.price_error = undefined;
    }
    $scope.check = function (ID) {
        var cateItem = $scope.list_cate.find(function (d) { return d.ID === ID });
        if (cateItem !== undefined) {
            return true;
        }
        else {
            return false;
        }
    };
    getData(1);
    $scope.selectLang = function (langId) {
        $scope.langID = parseInt(langId);
        getData($scope.langID);
    }
    $scope.BrowseImage = function () {
        var finder = new CKFinder();
        finder.selectActionFunction = function (url) {
            $scope.image = url;
            $scope.$apply();
        };
        finder.popup();
    };
    $scope.getMeta = function (name) {
        if ($scope.langID === 1)
            $scope.Meta = MyFactory.HOST_NAME + 'san-pham/' + MyFactory.FORMART_TITLE_TO_SLUG(name);
        else $scope.Meta = MyFactory.HOST_NAME + 'product/' + MyFactory.FORMART_TITLE_TO_SLUG(name);
    }
    $scope.BrowseServer = function () {
        var finder = new CKFinder();
        finder.selectActionFunction = showFileInfo;
        finder.popup();
    }
    $scope.event = {
        editImg: function (index) {
            var finder = new CKFinder();
            finder.selectActionFunction = function (url) {
                $scope.imageList[index] = url;
            };
            finder.popup();
        },
        delImg: function (index) {
            $scope.imageList.splice(index, 1);
        }
    };
    function showFileInfo(fileUrl, file, files, data) {
        for (var i = 0; i < files.length; i++) {
            $scope.imageList.push(files[i].url);
        }
        $scope.$apply();
    }
    $scope.addKeyword = function () {
        var data = {
            name: $scope.tag,
            meta: 'tags/' + MyFactory.FORMART_TITLE_TO_SLUG($scope.tag)
        }
        $scope.keyword.push(data);
        $scope.tag = '';
    }
    $scope.del_keyword = function (item) {
        var idx = $scope.keyword.indexOf(item);
        $scope.keyword.splice(idx, 1);
    }
}
NewProController.$inject = ['$rootScope', '$scope', 'MyFactory'];