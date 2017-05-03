var UpdateProController = function ($scope, $routeParams, MyFactory) {
    $scope.LIST_CATEGORIES = undefined;
    $scope.options = MyFactory.CKEDITOR_OPTIONS;
    $scope.today = MyFactory.FORMAT_STRING_DATE(MyFactory.TODAY());
    $scope.langID = 1;
    $scope.keyword = [];
    $scope.list_cate = [];
    $scope.Meta = '';
    MyFactory.GET(MyFactory.HOST_NAME + MyFactory.API.PRODUCTS + 'getbyid/' + $routeParams.proID).then(function (response) {
        if (response.err === 1) {
            $scope.PRODUCT = response.data;
            $scope.Meta = $scope.PRODUCT.Meta;
            $scope.image = $scope.PRODUCT.Image;
            getData($scope.PRODUCT.Lang);
            getKeyword($scope.PRODUCT.Tag);
            $('#selectLang').val($scope.PRODUCT.Lang);
            if($scope.PRODUCT.Status)
                $('#status').val(1);
            else $('#status').val(2);
            if ($scope.PRODUCT.ListImage !== null && $scope.PRODUCT.ListImage !== '')
                $scope.imageList = $scope.PRODUCT.ListImage.split(':');
            else
                $scope.imageList = [];
        }
        else show_message_box(response);
    });
    $scope.update = function () {
        var image = concatImageFromArr($scope.imageList);
        var data = {
            ID: $scope.PRODUCT.ID,
            Name: $scope.PRODUCT.Name,
            Meta: $scope.Meta,
            Intro: $scope.PRODUCT.Intro,
            CateID: '',
            Digital: $scope.PRODUCT.Digital,
            Content: $scope.PRODUCT.Content,
            Status: parseInt($('#status').val()),
            CreateDate: $scope.PRODUCT.CreateDate,
            CreateBy: $scope.PRODUCT.CreateBy,
            Image: $scope.image,
            Lang: $scope.langID,
            Views: 0,
            Price: $scope.PRODUCT.Price,
            Discount: $scope.PRODUCT.Discount,
            ListImage: image,
            QRCode: '',
            Tag: ''
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
            MyFactory.POST(MyFactory.HOST_NAME + MyFactory.API.PRODUCTS + 'post/2', data).then(function (response) {
                if (response.err === 1) {
                    var msg = 'Cập nhật sản phẩm thành công!';
                    MyFactory.MESSAGE_SUCCESS(msg);
                }
                else {
                    var msg = 'Cập nhật sản phẩm không thành công!';
                    MyFactory.MESSAGE_FAIL(msg);
                }
            });
        }
        else if(data.Discount !== undefined) alert('Vui lòng nhập giá khuyến mãi nhỏ hơn giá bán!');
    }
    function getData(lang) {
        MyFactory.GET(MyFactory.HOST_NAME + MyFactory.API.CATEGORIES + 'get/' + 1).then(function (response) {
            $scope.LIST_CATEGORIES = [];
            if (response.err === 1) {
                $scope.LIST_CATEGORIES = response.data;
                getCateForPro();
            }
            else show_message_box(response);
        });
    }
    function getCateForPro() {
        var data = $scope.PRODUCT.CateID.split(':');
        var countListCateByStr = data.length;
        var countListCateByArr = $scope.LIST_CATEGORIES.length;
        for (var i = 1; i < countListCateByStr; i++)
            for (var j = 0; j < countListCateByArr; j++)
                if (parseInt(data[i]) === $scope.LIST_CATEGORIES[j].ID)
                    $scope.list_cate.push($scope.LIST_CATEGORIES[j]);
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
    $scope.check = function (ID) {
        var cateItem = $scope.list_cate.find(function (d) { return d.ID === ID });
        if (cateItem !== undefined) {
            return true;
        }
        else {
            return false;
        }
    };
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
    function getKeyword(list) {
        var list_item = list.split(':');
        for (var i = 1; i < list_item.length - 1; i++) {
            var tag = list_item[i].split('|');
            var data = {
                name: tag[0],
                meta: tag[1]
            }
            $scope.keyword.push(data);
        }
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
    $scope.comparePrice = function () {
        if ($scope.PRODUCT.Price <= $scope.PRODUCT.Discount)
            $scope.price_error = 'Giá khuyến mãi không được lớn hơn hoặc bằng giá bán!';
        else $scope.price_error = undefined;
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
UpdateProController.$inject = ['$scope', '$routeParams', 'MyFactory'];