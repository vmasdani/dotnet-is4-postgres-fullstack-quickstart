using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Google.Protobuf;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationContext>(o =>
{
    o.UseNpgsql(builder.Configuration.GetConnectionString("PostgresUrl"));
});

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

Console.WriteLine("get postgres url");
Console.WriteLine(builder.Configuration.GetConnectionString("PostgresUrl"));

var c = app.Services.GetRequiredService<ApplicationContext>();
app.Services.GetRequiredService<ApplicationContext>().Database.Migrate();

Console.WriteLine("Migrating....");
c.Database.Migrate();

// Test startup
Console.WriteLine("Adding record....");
var r1 = new Record();
var r2 = new Record();

c.Update(r1);
c.Update(r2);

c.SaveChanges();

Console.WriteLine(JsonSerializer.Serialize(r1));
Console.WriteLine(JsonSerializer.Serialize(r2));

Thread.Sleep(1000);

r1.Deleted = DateTime.UtcNow;
c.Update(r1);
c.SaveChanges();

Console.WriteLine(JsonSerializer.Serialize(r1));

app.MapPost("/userinfo", async (HttpRequest r) =>
{
    var reqBody = new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true)).Parse<ISUserInfoRequest>(
        await new StreamReader(r.Body).ReadToEndAsync()
    );

    var res = await new HttpClient().Setup(c =>
    {
        c.DefaultRequestHeaders.Add("Authorization", $"Bearer {reqBody.Token}");
    }).GetAsync(
        $"http://{builder.Configuration.GetConnectionString("HostUrl")}:{builder.Configuration.GetConnectionString("IdentityServerPort")}/connect/userinfo"

    );

    Console.WriteLine(res.StatusCode);
    Console.WriteLine(await res.Content.ReadAsStringAsync());

    return Results.Text(await res.Content.ReadAsStringAsync(), contentType: "application/json");

});

app.MapGet("/users", async (HttpRequest r) =>
{
    try
    {
        Console.WriteLine($"Getting {$"http://{builder.Configuration.GetConnectionString("HostUrl")}:{builder.Configuration.GetConnectionString("IdentityApiPort")}/api/Users"}, {(string?)r.Headers["Authorization"]}");
        var res = await new HttpClient().Setup(c =>
            {
                c.DefaultRequestHeaders.Add("Authorization", (string?)r.Headers["Authorization"]);
            }).GetAsync(
                $"http://{builder.Configuration.GetConnectionString("HostUrl")}:{builder.Configuration.GetConnectionString("IdentityApiPort")}/api/Users?pageSize={int.MaxValue}"
            );

        if (res.StatusCode != HttpStatusCode.OK)
        {
            Console.WriteLine((await res.Content.ReadAsStringAsync()));
            throw new Exception(await res.Content.ReadAsStringAsync());
        }

        return Results.Text(
            await res.Content.ReadAsStringAsync(),
            contentType: "application/json"
         );
    }
    catch (Exception e)
    {
        return Results.Problem($"{e}");
    }
});

app.MapPost("/login", async (LoginBody body) =>
{
    Console.WriteLine(
        $"uname: {body.Username}, pwd: {body.Password} === http://{builder.Configuration.GetConnectionString("HostUrl")}:{builder.Configuration.GetConnectionString("IdentityServerPort")}/connect/token"
    );

    try
    {
        var res = await new HttpClient().PostAsync(
            $"http://{builder.Configuration.GetConnectionString("HostUrl")}:{builder.Configuration.GetConnectionString("IdentityServerPort")}/connect/token",
            new FormUrlEncodedContent(new Dictionary<string, string>().Setup(d =>
            {
                d.Add("client_id", builder.Configuration.GetConnectionString("SkorubaClientId") ?? "");
                d.Add("client_secret", builder.Configuration.GetConnectionString("SkorubaClientSecret") ?? "");
                d.Add("grant_type", "password");
                d.Add("username", body.Username ?? "");
                d.Add("password", body.Password ?? "");
            }))
        );

        Console.WriteLine(res.StatusCode);

        if (res.StatusCode != HttpStatusCode.OK)
        {
            return Results.BadRequest(JsonFormatter.Default.Format(
                new JsonParser(JsonParser.Settings.Default.WithIgnoreUnknownFields(true)).Parse<ISLoginError>(await res.Content.ReadAsStringAsync())
            ));
        }

        var endpointReturn = new JsonParser(
            JsonParser.Settings.Default.WithIgnoreUnknownFields(true)
        ).Parse<ISTokenEndpointReturn>(await res.Content.ReadAsStringAsync());

        Console.WriteLine(
            JsonFormatter.Default.Format(endpointReturn)
        );

        return Results.Text(
            JsonFormatter.Default.Format(endpointReturn),
            contentType: "application/json"
        );
    }
    catch (Exception e)
    {
        return Results.Problem(e.ToString());
    }

});

app.Run();

