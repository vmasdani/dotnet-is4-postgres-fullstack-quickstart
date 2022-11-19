using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;
using Google.Protobuf;

var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

Console.WriteLine("get postgres url");
Console.WriteLine(builder.Configuration.GetConnectionString("PostgresUrl"));

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
