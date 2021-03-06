﻿using CoreApi.Domain;

namespace CoreApi.Security.Token
{
    public interface ITokenHandler
    {
        AccessToken CreateAccessToken(User user);

        void RevokeRefreshToken(User user);
    }
}
