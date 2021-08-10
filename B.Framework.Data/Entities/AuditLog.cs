using System;
using System.Collections.Generic;

#nullable disable

namespace B.Framework.Data.Entities
{
    public partial class AuditLog
    {
        public Guid Id { get; set; }
        public string AuditType { get; set; }
        public string TableName { get; set; }
        public string Pk { get; set; }
        public string ColumnName { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
        public DateTime Date { get; set; }
        public Guid UserId { get; set; }
    }
}
