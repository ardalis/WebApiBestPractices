using OwnerPermissions.Models;

namespace OwnerPermissions.Data;

public interface IDocumentRepository
{
	Document Find(Guid documentId);
}
