using EduPress.Core.Common;

namespace EduPress.Core.Entities
{
    public class ContactLocations : BaseEntity, IAuditedEntity
    {
        public string Address { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
