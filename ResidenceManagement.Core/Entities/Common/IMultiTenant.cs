namespace ResidenceManagement.Core.Entities.Common
{
    /// <summary>
    /// Multi-tenant yapısı için FirmaId ve SubeId alanlarını içeren varlıkların 
    /// uygulaması gereken arayüz
    /// </summary>
    public interface IMultiTenant
    {
        /// <summary>
        /// Varlığın ait olduğu firma ID
        /// </summary>
        int FirmaId { get; set; }

        /// <summary>
        /// Varlığın ait olduğu şube ID (0 ise tüm şubelerde geçerli)
        /// </summary>
        int SubeId { get; set; }
    }
} 