using OwnerPermissions.Models;

namespace OwnerPermissions.Data;

public class DocumentRepository : IDocumentRepository
{
	public Document Find(Guid documentId)
	{
		return new Document
		{
			Author = "BALERION\\User", // change this to your windows account name
			Content = "document content",
			ID = documentId,
			Title = "Test Document"
		};
	}
}

