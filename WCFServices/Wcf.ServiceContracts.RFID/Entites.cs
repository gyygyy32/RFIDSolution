using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;


namespace Wcf.ServiceContracts.RFID
{
    [DataContract(Namespace = SvcNameSpace.ContractDataNamespace)]
    public class ModuleInfo
    {
        [DataMember]
        public string ProductType { get; set; }

        [DataMember]
        public string ELGrade { get; set; }

        [DataMember]
        public string Status { get; set; }

        [DataMember]
        public string PackedDate { get; set; }

        [DataMember]
        public string Pmax { get; set; }

        [DataMember]
        public string Voc { get; set; }

        [DataMember]
        public string Isc { get; set; }

        [DataMember]
        public string Vpm { get; set; }

        [DataMember]
        public string Ipm { get; set; }

        [DataMember]
        public string Pivf { get; set; }

        [DataMember]
        public string Module_ID { get; set; }

        [DataMember]
        public string PalletNO { get; set; }

        [DataMember]
        public string CellDate { get; set; }

        [DataMember]
        public string Cellsource { get; set; }

        [DataMember]
        public string EqpID { get; set; }

        [DataMember]
        public string IVFilePath { get; set; }
        //[DataMember]
        //public decimal ipm { get; set; }
        //[DataMember]
        //public decimal vpm { get; set; }

        [DataMember]
        public bool b_writenDataBefore { get; set; }
        //add by xue lei on 2018-6-23
        [DataMember]
        public string FF { get; set; }
    }
}
