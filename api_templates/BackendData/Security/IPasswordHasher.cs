﻿namespace BackendData.Security;

public interface IPasswordHasher
{
	string HashPassword(string password);
	bool PasswordMatches(string providedPassword, string passwordHash);
}
