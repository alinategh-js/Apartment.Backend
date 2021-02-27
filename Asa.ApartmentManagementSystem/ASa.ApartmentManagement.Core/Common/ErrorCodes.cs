using System;
using System.Collections.Generic;
using System.Text;

namespace ASa.ApartmentManagement.Core.Common
{
    public class ErrorCodes
    {
        ErrorCodes() { }
        public const int Invalid_Building_Name = 1000;
        public const int Invalid_Number_Of_Units = 1001;
        public const int Invalid_Person_Name = 1002;
        public const int Invalid_Person_Phone_Number = 1003;
        public const int Building_Not_Found = 1004;
    }
}
