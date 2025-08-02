# Models and servicies
This folder contains some data models and their respeted data services that
used to be part of the framework in version priors to version 10.0.0.

Their can be used to implement it functionality in other sytems.


# Code fragments removed
These a code fragments that whas removed but can be useful to implement
funtionality base on these models and services.

## Tenant provider registration
This code was used to register a tenant provider.

```csharp
/// <summary>
/// Adds a scoped <see cref="ITenantProvider"/> implemented by <see cref="TenantProvider"/>.
/// </summary>
public static IKpokPatagonBuilder AddTenantProvider(
    this IKpokPatagonBuilder builder,
    Action<TenantProviderOptions> options = null)
{
    if (options == null)
    {
        options = (o) => { };
    }

    builder.Services.Configure(options);
    builder.Services.AddScoped<ITenantProvider, TenantProvider>();
    return builder;
}
```
## Tenant connection string getter/setter extensions
Used to allow getting and setting a tenant connection string,

```csharp
/// <summary>
/// Gets the <paramref name="tenant"/> connection string as plain text
/// using symmetric algorithm configured with <paramref name="options"/>
/// to decrypt the connection string or <see langword="null"/> if 
/// <paramref name="tenant"/> does not has a connection string.
/// </summary>
/// <param name="tenant">A <see cref="Tenant"/>.</param>
/// <param name="options">Options to configure the symmetric algorithm.</param>
/// <exception cref="ArgumentNullException">
///     <para>Thrown when <paramref name="tenant"/> is <see langword="null"/>.</para>
///     <para>-or-</para>
///     <para>When <paramref name="options"/> is <see langword="null"/>.</para>
/// </exception>
public static string GetConnectionString(this Tenant tenant, CryptographyOptions options)
{
    if (tenant is null)
    {
        throw new ArgumentNullException(nameof(tenant));
    }

    if (options is null)
    {
        throw new ArgumentNullException(nameof(options));
    }

    if (tenant.ConnectionString.IsPresent())
    {
        using (var provider = SymmetricProviderFactory.Create(options))
        {
            var ciphered = Convert.FromBase64String(tenant.ConnectionString);
            var decrypted = provider.Decrypt(ciphered);
            var plaintext = Encoding.UTF8.GetString(decrypted);
            CryptographyUtility.ZeroOutBytes(ciphered);
            CryptographyUtility.ZeroOutBytes(decrypted);

            return plaintext;
        }
    }

    return null;
}

/// <summary>
/// Encrypts and sets the <paramref name="tenant"/> connection string using
/// a symmetric algorithm configured with <paramref name="options"/>.
/// </summary>
/// <param name="tenant">A <see cref="Tenant"/>.</param>
/// <param name="options">Options to configure the symmetric algorithm.</param>
/// <param name="connectionString">The connection string to set.</param>
/// <exception cref="ArgumentNullException">
///     <para>Thrown when <paramref name="tenant"/> is <see langword="null"/>.</para>
///     <para>-or-</para>
///     <para>When <paramref name="options"/> is <see langword="null"/>.</para>
/// </exception>
/// <exception cref="ArgumentException">
///     Thrown when <paramref name="connectionString"/> is <see langword="null"/> or empty.
/// </exception>
public static void SetConnectionString(this Tenant tenant, CryptographyOptions options, string connectionString)
{
    if (tenant is null)
    {
        throw new ArgumentNullException(nameof(tenant));
    }

    if (options is null)
    {
        throw new ArgumentNullException(nameof(options));
    }

    if (string.IsNullOrWhiteSpace(connectionString))
    {
        throw new ArgumentException($"'{nameof(connectionString)}' cannot be null or whitespace.", nameof(connectionString));
    }

    using (var provider = SymmetricProviderFactory.Create(options))
    {
        var plaintext = Encoding.UTF8.GetBytes(connectionString);
        tenant.ConnectionString = Convert.ToBase64String(provider.Encrypt(plaintext));
        CryptographyUtility.ZeroOutBytes(plaintext);
    }
}
```
