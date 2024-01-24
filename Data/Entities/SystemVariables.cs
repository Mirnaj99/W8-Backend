using System.ComponentModel.DataAnnotations.Schema;

namespace W8_Backend.Data.Entities
{
    public class SystemVariables
    {
        public int VariableID { get; set; }
        public bool SyncStatus { get; set; }
        public bool IsSyncing { get; set; }
        public bool SyncMonthlyCostSheet { get; set; }

        public bool SyncEmpDiff { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LogCleanDate { get; set; }
        public int Retention { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? LogCleanRuntime { get; set; }
        public string MonthlyCostSheetPath { get; set; }

        public string TargetPathForMonthlyCostSheet { get; set; }

        public string Api1Url { get; set; }

        public string Api2Url { get; set; }

        public string HRJobNb { get; set; }

        public string HRTaskNb { get; set; }

        public string HRCompanyCode { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime? LastSyncDate { get; set; }

        public string DocumentNumberToDelete { get; set; }

        public bool DeleteRecords { get; set; }
      



    }
}
