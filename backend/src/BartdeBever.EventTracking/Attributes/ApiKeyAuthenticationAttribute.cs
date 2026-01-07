using Microsoft.AspNetCore.Mvc;

namespace BartdeBever.EventTracking.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class ApiKeyAuthenticationAttribute() : ServiceFilterAttribute(typeof(Filters.ApiKeyAuthenticationFilter));
