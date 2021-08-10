using System;
using System.Collections.Generic;
using B.Framework.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace B.Framework.Application.Data
{
    public class AuditEntry
    {
        private EntityEntry _entry;
        private IHttpContextAccessor _httpContextAccessor;

        public AuditEntry(EntityEntry entry,IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _entry = entry;
        }

        public string UserId { get; set; }
        public string TableName { get; set; }
        public Dictionary<string,object> KeyValues { get; set; }
        public Dictionary<string,object> OldValues { get; set; }
        public Dictionary<string,object> NewValues { get; set; }
        public Enum State { get; set; }
        public List<string> ChangedColumns { get; set; }

        public AuditLog ToAuditLog()
        {
            var audit = new AuditLog()
            {
                UserId = _httpContextAccessor.HttpContext.User.Claims.
            }
        }
    }
}