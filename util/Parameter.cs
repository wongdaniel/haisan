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
    }
}
