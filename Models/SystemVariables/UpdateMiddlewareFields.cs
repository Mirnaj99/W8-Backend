namespace W8_Backend.Models.SystemVariables
{
    public class UpdateMiddlewareFields
    {
        public int? Retention { get; set; }
        public DateTime? LogCleanRuntime { get; set; }
        public bool MiddlewareStatus { get; set; }

        public DateTime? LastSyncDate { get; set; }

        public bool? SyncEmpDiff { get; set; }

        public string? HRCompanyCode { get; set; }

        public string? HRJobNb { get; set; }

        public string? HRTaskNb { get; set; }

        public bool? DeleteRecords { get; set; }

        public string? DocumentNumberToDelete { get; set; }
    }
}
