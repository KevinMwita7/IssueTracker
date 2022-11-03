using System.ComponentModel.DataAnnotations;

namespace IssueTracker.Data {
    public abstract class BaseEntity {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        [ConcurrencyCheck]
        public int Version { get; set; }
    }
}