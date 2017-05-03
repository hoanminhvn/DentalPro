using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web_ProductWithQRCode.Core.Helper
{
    public static class Constants
    {
        public static string USER_SESSION = "USER_SESSION";
        //Permission
        public static int USERS = 1;

        //Bit for action of permission
        public static int ACTION_VIEW = 8;
        public static int ACTION_ADD = 4;
        public static int ACTION_EDIT = 2;
        public static int ACTION_DELETE = 1;

        //ErrorCode, ErrorMessage
        public static int PERMISSION_LOGIN_CODE = -2;
        public static string PERMISSION_LOGIN_MSG = "You are not logged in!!";

        public static int PERMISSION_DENIED_CODE = -1;
        public static string PERMISSION_DENIED_MSG = "Permission denied!!";

        public static int PROCESS_OK = 1;
        public static int PROCESS_FAILED = 0;

        public static string INSERT_SUCCESS = "Thêm thành công!";
        public static string INSERT_FAIL = "Thêm không thành công!";
        public static string UPDATE_SUCCESS = "Cập nhật thành công!";
        public static string UPDATE_FAIL = "Cập nhật không thành công!";
        public static string DELETE_SUCCESS = "Xóa thành công!";
        public static string DELETE_FAIL = "Xóa không thành công!";
    }
}