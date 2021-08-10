using B.Framework.Domain.Shared.Attribute;

namespace B.Framework.Domain.Shared.Enums.Claims
{
    public enum BEClaimTypes
    {
        [BAttributeBase("username")]
        UserName,
        [BAttributeBase("firstname")]
        FirstName,
        [BAttributeBase("lastname")]
        LastName,
        [BAttributeBase("tenantId")]
        TenantId,
        [BAttributeBase("role")]
        Role
        
    }
}