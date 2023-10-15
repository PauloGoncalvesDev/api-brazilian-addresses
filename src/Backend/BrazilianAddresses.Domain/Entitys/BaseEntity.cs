namespace BrazilianAddresses.Domain.Entitys
{
    public class BaseEntity
    {
        public long? Id { get; set; }

        public DateTime CreationDate { get; set; }

        public long CreationUserId { get; set; }

        public DateTime UpdateDate { get; set; }
        
        public long UpdateUserId { get; set; }
        
        public DateTime? DeletionDate { get; set; }
    }
}
