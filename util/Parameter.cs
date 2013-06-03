using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using haisan.domain;

namespace haisan.util
{
    sealed class Parameter
    {
        public static User user;


        // for 系统参数中的基本设置
        public static readonly string COMPANY_NAME = "company_name";
        public static readonly string COMPANY_REPRESENT = "company_represent";
        public static readonly string COMPANY_BANK = "company_bank";
        public static readonly string COMPANY_BANK_ACCOUNT = "company_bank_account";
        public static readonly string COMPANY_PHONE = "company_phone";
        public static readonly string COMPANY_POSTAL_CODE = "company_postal_code";
        public static readonly string COMPANY_ADDRESS = "company_address";

        //
        public static readonly string SUPER_ADMISTRATOR = "超级管理员";




        //For 权限管理， 通过游标打印tb_module 自动生成代码
        public static readonly string MODULE_DEPARMENT = "module_deparment";//部门资料
        public static readonly string MODULE_PROVIDER = "module_provider";//供应商资料
        public static readonly string MODULE_EMPLOYEE = "module_employee";//员工资料
        public static readonly string MODULE_STOREHOUSE = "module_storehouse";//仓库资料
        public static readonly string MODULE_DOC = "module_doc";//商品资料
        public static readonly string MODULE_STOCK = "module_stock";//期初库存
        public static readonly string MODULE_RELOGIN = "module_relogin";//重新登录
        public static readonly string MODULE_CHGPWD = "module_chgpwd";//修改密码
        public static readonly string MODULE_CONFIGURE = "module_configure";//账套参数
        public static readonly string MODULE_USER_MGR = "module_user_mgr";//用户管理
        public static readonly string MODULE_GROUP_MGR = "module_group_mgr";//用户组管理
        public static readonly string MODULE_DELIVER_GOODS = "module_deliver_goods";//销售出库单
        public static readonly string MODULE_REJECT_GOODS = "module_reject_goods";//销售退货单
        public static readonly string MODULE_ORDER_GOODS = "module_order_goods";//销售订货单
        public static readonly string MODULE_CATE_PROD = "module_cate_prod";//商品类型
        public static readonly string MODULE_CATE_PRD = "module_cate_prd";//往来单位类型
        public static readonly string MODULE_CATE_STONE = "module_cate_stone";//石材类型
        public static readonly string MDOULE_NAME_PROD = "mdoule_name_prod";//产品名称


        public static readonly short UNCHECKED = 0;
        public static readonly short CHECKED = 1;
        public static readonly short INDETERMINATE = 2;
        

        public static readonly string SQUARE_METER = "m^2"; //平方米
        public static readonly string STERE = "m^3"; //立方米
        public static readonly string METER = "m"; //米
        public static readonly string PACKAGE = "p"; //件

        public static readonly int NUMBER_MANTISSA = 3;


        //提货方式
        public static readonly string GET_CASH = "现金提货";
        public static readonly string GET_SIGN = "签字提货";

        //订单编码前缀
        public static readonly string SN_PRE = "HS";


        //datagridview中缩略图的长宽
        public static readonly int THUMBNAIL_LENGTH = 70;
        public static readonly int THUMBNAIL_WIDTH = 30;

    }
}
