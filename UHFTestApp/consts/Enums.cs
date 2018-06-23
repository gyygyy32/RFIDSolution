using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace UHFDesk.consts
{
    enum ActionType
    {
        None,
        GetFrequencyRegion,
        WriteTag,
        ReadTIDBankWhenWrite,
        ReadEPC,
        ReadTIDBankWhenRead,
        ReadUserBank,
    }

    enum ActionResault
    {
        GetFrequencyRegionSuccess,
        GetFrequencyRegionFail,
        ReadTIDBankWhenWriteSuccess,
        ReadTIDBankWhenWriteFail,
        WriteTagSuccess,
        WriteTagFail,
        NoTagFound,
        MutiTagFound,

    }

}
