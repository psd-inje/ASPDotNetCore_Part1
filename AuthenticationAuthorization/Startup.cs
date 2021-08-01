using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace AuthenticationAuthorization
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication("Cookies").AddCookie();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();

            app.UseEndpoints(endpoints =>
            {
                #region /
                endpoints.MapGet("/", async context =>
                {
                    string content = "<h1>ASP.NET Core ������ ���� �ʰ��� �ڵ�</h1>";

                    content += "<a href=\"/Login\">�α���</a><br />";
                    content += "<a href=\"/Info\">����</a><br />";
                    content += "<a href=\"/InfoJson\">����(Json)</a><br />";
                    content += "<a href=\"/Logout\">�α׾ƿ�</a><br />";

                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(content);
                }); 
                #endregion

                #region Login
                endpoints.MapGet("/Login", async context =>
                {
                    var claims = new List<Claim>
                    {
                //new Claim(ClaimTypes.Name, "User Name")
                new Claim(ClaimTypes.Name, "���̵�")
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    await context.SignInAsync("Cookies", claimsPrincipal);

                    string content = "<h3>�α��� �Ϸ�</h3>";
                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(content);

                }); 
                #endregion

                #region Info
                endpoints.MapGet("/Info", async context =>
                {
                    string result = "";

                    if (context.User.Identity.IsAuthenticated)
                    {
                        result += $"<h3>�α��� �̸�: {context.User.Identity.Name}</h3>";
                    }
                    else
                    {
                        result += $"<h3>�α������� �ʾҽ��ϴ�.</h3>";
                    }

                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(result, Encoding.Default);
                });
                #endregion

                #region InfoJson
                endpoints.MapGet("/InfoJson", async context =>
                {
                    string json = "";

                    if (context.User.Identity.IsAuthenticated)
                    {
                        //json += "{\"type\": \"Name\", \"value\":\"User Name \"}";
                        var claims = context.User.Claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value });
                        //json = JsonSerializer.Serialize<IEnumerable<ClaimDto>>(claims, new JsonSerializerOptions { });
                        json = JsonSerializer.Serialize<IEnumerable<ClaimDto>>(
                            claims, 
                            new JsonSerializerOptions { Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping }); // �ѱ�
                    }
                    else
                    {
                        json += "{}";
                    }

                    //context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    context.Response.Headers["Content-Type"] = "application/json; charset=utf-8"; //MIME Ÿ��
                    await context.Response.WriteAsync(json);
                });
                #endregion

                #region Logout
                endpoints.MapGet("/Logout", async context =>
                {
                    await context.SignOutAsync("Cookies");


                    //context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    context.Response.Headers["Content-Type"] = "application/json; charset=utf-8"; //MIME Ÿ��
                    await context.Response.WriteAsync("<h3>�α׾ƿ� �Ϸ�</h3>");
                });
                #endregion

            });
        }
    }


    public class ClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
}
