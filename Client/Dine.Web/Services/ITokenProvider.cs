﻿namespace Dine.Web.Services
{
    public interface ITokenProvider
    {
        void SetToken(string token);
        string? GetToken();
        void ClearToen();
    }
}
