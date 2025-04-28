namespace ResidenceManagement.Core.Interfaces.Common
{
    /// <summary>
    /// Multi-tenant yapı için temel interface
    /// </summary>
    public interface ITenant
    {
        /// <summary>
        /// Firma ID
        /// </summary>
        int CompanyId { get; set; }

        /// <summary>
        /// Şube ID
        /// </summary>
        int BranchId { get; set; }
    }
} 