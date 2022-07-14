using Microsoft.AspNetCore.Authorization;
using OwnerPermissions.Models;

namespace OwnerPermissions.Services;

public class DocumentAuthorizationHandler :
				AuthorizationHandler<SameAuthorRequirement, Document>
{
	protected override Task HandleRequirementAsync(
		AuthorizationHandlerContext context,
		SameAuthorRequirement requirement,
		Document resource)
	{
		if (context.User.Identity?.Name == resource.Author)
		{
			context.Succeed(requirement);
		}

		return Task.CompletedTask;
	}
}
