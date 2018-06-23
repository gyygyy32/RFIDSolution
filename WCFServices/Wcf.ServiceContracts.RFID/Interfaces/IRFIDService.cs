using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;

namespace Wcf.ServiceContracts.RFID
{
    [ServiceContract(Namespace = SvcNameSpace.ContractsNamespace)]
    public interface IRFIDService
    {
        [OperationContract]
        void writeTag();

        [OperationContract]
        void WriteLog(object[] parms);


        [OperationContract]
        void readTag();

        [OperationContract]
        ModuleInfo getModuleInfo(object[] parms);
    }
}
