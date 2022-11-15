# .NET Identity Server 4 Quickstart

Full stack .NET 6 PostgreSQL backend with identity server 4 + is4 Skoruba admin.

### Prerequisites

1. Docker
2. Linux OS

### Running

```sh
./manage.sh run dev
./manage.sh build prod
./manage.sh push prod

# Or full path using python venv
./venv/bin/python3 manage.py run dev
./venv/bin/python3 manage.py build prod
./venv/bin/python3 manage.py push prod
```

### Configuration

1. Config everything in `config.yml`.
2. Add your redirect URI for your domain (`/signin-oidc`) in phppgadmin > is4admin > ClientRedirectUris for ClientId skoruba_identity_admin

### Changed from the original version:

https://github.com/skoruba/IdentityServer4.Admin/blob/master/docs/Configure-Ubuntu-PostgreSQL-Tutorial.md

1. Admin setting (`IdentityServer4.Admin-release-2.1.0/src/Skoruba.IdentityServer4.Admin/appsettings.json`):

- Changed database

```json
"DatabaseProviderConfiguration": {
    "ProviderType": "PostgreSQL"
},
```

- Changed db string connection

```
Server=localhost; User Id=postgres; Database=is4admin; Port=5432; Password=postgres; SSL Mode=Prefer; Trust Server Certificate=true

```

2. Added timezone options for postgres in main func
- `IdentityServer4.Admin-release-2.1.0/src/Skoruba.IdentityServer4.Admin/Program.cs`
- `IdentityServer4.Admin-release-2.1.0/src/Skoruba.IdentityServer4.Admin.Api/Program.cs`
- `IdentityServer4.Admin-release-2.1.0/src/Skoruba.IdentityServer4.STS.Identity/Program.cs`


```cs
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
```

- Added `password` grant type
- `IdentityServer4.Admin-release-2.1.0/src/Skoruba.IdentityServer4.Admin/identityserverdata.json`
```json
"Clients": [
    {
        ...
        "AllowedGrantTypes": [
            "authorization_code",
            "password"
        ],
        ...
    }
]

```