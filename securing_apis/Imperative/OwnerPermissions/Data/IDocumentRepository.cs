using OwnerPermissions.Models;

namespace OwnerPermissions.Data;

public interface IDocumentRepository
{
	Document Find(int documentId);
}
