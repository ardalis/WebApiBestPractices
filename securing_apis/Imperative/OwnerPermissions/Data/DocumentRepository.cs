using OwnerPermissions.Models;

namespace OwnerPermissions.Data;

public class DocumentRepository : IDocumentRepository
{
	public Document Find(int documentId)
	{
		return new Document
		{
			Author = "COMPUTERNAME\\UserName", // change this to your windows account name
			Content = "document content",
			Id = documentId,
			Title = "Test Document"
		};
	}
}

