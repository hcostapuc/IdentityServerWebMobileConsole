// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace IdentityServer
{
    public static class Config
    {
        public static IEnumerable<IdentityResource> IdentityResources =>
            new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        public static IEnumerable<ApiResource> IdentityApiResources =>
            new ApiResource[]
            {
                new ApiResource(
                name: "wfapi",
                userClaims: new[] { "read"},
                displayName: "API weatherForecast")
                {
                    ApiSecrets = { new Secret("511536EF-F270-4058-80CA-1C89C192F69A".Sha256()) }
                }
            };
        public static IEnumerable<ApiScope> ApiScopes =>
            new ApiScope[]
            {
                new ApiScope(name: "read",   displayName: "Read your data."),
                new ApiScope(name: "write",  displayName: "Write your data."),
                new ApiScope(name: "delete", displayName: "Delete your data.")
            };

        public static IEnumerable<Client> Clients =>
            new Client[]
            {
                // interactive client using code flow + pkce
                new Client
                {
                    ClientId = "MobileClient",
                    ClientSecrets = { new Secret("49C1A7E1-0C79-4A89-A3D6-A37998FB86B0".Sha256()) },
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = { "http://127.0.0.1:56778" },
                    //FrontChannelLogoutUri = "https://localhost:4200/signout-oidc",
                    //PostLogoutRedirectUris = { "https://localhost:4200/signout-callback-oidc" },
                    AllowOfflineAccess = true,
                    AllowedScopes = {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.OfflineAccess
                    },
                    AllowAccessTokensViaBrowser = true
                },
            };
    }
}