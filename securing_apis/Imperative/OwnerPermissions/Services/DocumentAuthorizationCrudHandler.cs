using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using OwnerPermissions.Models;

namespace OwnerPermissions.Services;

public class DocumentAuthorizationCrudHandler :
				AuthorizationHandler<OperationAuthorizationRequirement, Document>
{
	protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
																								 OperationAuthorizationRequirement requirement,
																								 Document resource)
	{
		if (context.User.Identity?.Name == resource.Author &&
				requirement.Name == Operations.Read.Name)
		{
			context.Succeed(requirement);
		}

		return Task.CompletedTask;
	}
}
