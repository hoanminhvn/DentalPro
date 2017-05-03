var MyFactory = function ($http, $q, Flash) {
    return angular.extend($http, {
        HOST_NAME: window.location.origin + "/",
        API: {
            LOGIN: "api/login/",
            USERS: "api/users/",
            CATEGORIES: "api/categories/",
            PRODUCTS: "api/products/"
        },
        GET: function (url) {
            var result = $q.defer();
            $http({
                method: 'GET',
                url: url
            }).then(function (respone) {
                result.resolve(respone.data);
            }, result.reject.bind(null));

            return result.promise;
        },
        POST: function (url, params) {
            var result = $q.defer();
            $http.defaults.headers.post['Content-Type'] = 'application/json';
            $http({
                method: 'POST',
                url: url,
                data: params
            }).then(function (respone) {
                result.resolve(respone.data);
            }, result.reject.bind(null));

            return result.promise;
        },
        DELETE: function (url) {
            var result = $q.defer();
            $http.defaults.headers.post['Content-Type'] = 'application/json';
            $http({
                method: 'DELETE',
                url: url,
            }).then(function (respone) {
                result.resolve(respone.data);
            }, result.reject.bind(null));

            return result.promise;
        },
        TODAY: function () {
            var today = new Date();
            return (today.getDate()) + "-" + (today.getMonth() + 1) + "-" + today.getFullYear();
        },
        CURRENT_TIME: function () {
            var now = new Date();
            return now.getHours() + ':' + now.getMinutes() + ':' + now.getSeconds();
        },
        FORMAT_STRING_DATE: function (date) {
            var parts = date.split("-");
            return parts[2] + '-' + parts[1] + '-' + parts[0];
        },
        MAX_ID: function (str_type, quantityNo) {
            for (var i = 0; i < quantityNo; i++)
                str_type += "0";
            return str_type;
        },
        MESSAGE_SUCCESS: function (msg) {
            var message = '<strong> Thông báo!</strong> ' + msg;
            var id = Flash.create('success', message, 10000, { class: 'flash-success', id: 'custom-id' }, true);
        },

        MESSAGE_FAIL: function (msg) {
            var message = '<strong> Thông báo!</strong> ' + msg;
            var id = Flash.create('danger', message, 10000, { class: 'flash-danger', id: 'custom-id' }, true);
        },
        FORMATDATE_TO_TIME: function (date) {
            var parts = date.split("-");
            var date = new Date(parts[1] + "/" + parts[0] + "/" + parts[2]);
            return date.getTime();
        },
        FORMART_TITLE_TO_SLUG: function (str) {
            if (str !== undefined) {
                str = str.toLowerCase();
                str = str.replace(/à|á|ạ|ả|ã|â|ầ|ấ|ậ|ẩ|ẫ|ă|ằ|ắ|ặ|ẳ|ẵ/g, "a");
                str = str.replace(/è|é|ẹ|ẻ|ẽ|ê|ề|ế|ệ|ể|ễ/g, "e");
                str = str.replace(/ì|í|ị|ỉ|ĩ/g, "i");
                str = str.replace(/ò|ó|ọ|ỏ|õ|ô|ồ|ố|ộ|ổ|ỗ|ơ|ờ|ớ|ợ|ở|ỡ/g, "o");
                str = str.replace(/ù|ú|ụ|ủ|ũ|ư|ừ|ứ|ự|ử|ữ/g, "u");
                str = str.replace(/ỳ|ý|ỵ|ỷ|ỹ/g, "y");
                str = str.replace(/đ/g, "d");
                str = str.replace(/!|@|\$|%|\^|\*|\(|\)|\+|\=|\<|\>|\?|\/|,|\.|\:|\'| |\"|\”|\“|\&|\#|\[|\]|~/g, "-");
                str = str.replace(/-+-/g, "-"); //thay thế 2- thành 1-
                str = str.replace(/ +-+ /g, "-");
                str = str.replace(/^\-+|\-+$/g, "");//cắt bỏ ký tự - ở đầu và cuối chuỗi
            }
            else {
                str = '';
            }
            return str;
        },
        CKEDITOR_OPTIONS: {
            height: 500
        }
    });
}
MyFactory.$inject = ['$http', '$q', 'Flash'];