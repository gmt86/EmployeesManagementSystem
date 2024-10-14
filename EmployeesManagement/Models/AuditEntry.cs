using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace EmployeesManagement.Models
{
    public class AuditEntry
    {
        public EntityEntry Entry { get; set; }
        public AuditEntry(EntityEntry entry)
        {
            Entry = entry;
        }

       
        public  string UserId { get; set; }
        public string TableName { get; set; }

        public Dictionary<String, object> KeysValues { get; } = new Dictionary<string, object>();
        public Dictionary<String, object> OldValues { get; } = new Dictionary<string, object>();
        public Dictionary<String, object> NewValues { get; } = new Dictionary<string, object>();

        public AuditType AuditType { get; set; }

        public List<String> ChangedColumns { get; } = new List<string>();

        public Audit ToAudit()
        {
            var audit = new Audit();
            audit.UserId = UserId;
            audit.AuditType = AuditType.ToString();
            audit.TableName = TableName;
            audit.DateTime = DateTime.Now;
            audit.PrimaryKey = JsonConvert.SerializeObject(KeysValues);
            audit.OldValue = OldValues.Count ==0? null: JsonConvert.SerializeObject(OldValues);
            audit.NewValue = NewValues.Count==0? null:  JsonConvert.SerializeObject(NewValues);
            audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
            return audit;
        }
    }
}
