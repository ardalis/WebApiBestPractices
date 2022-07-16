﻿using JWTAPI.Core.Models;

namespace JWTAPI.Core.Security.Tokens;

public interface ITokenHandler
{
	AccessToken CreateAccessToken(User user);
	RefreshToken TakeRefreshToken(string token, string email);
	void RevokeRefreshToken(string token, string email);
}
