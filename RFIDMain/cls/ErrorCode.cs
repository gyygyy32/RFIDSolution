using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RFIDMain.cls
{
    public enum ErrorCode
    {
        ReadSuccessful,
        ReadFail,
        TagHasNoData,
        CanNotFindTag,
        OtherException,
    }
}
