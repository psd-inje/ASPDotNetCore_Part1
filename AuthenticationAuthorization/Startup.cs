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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace AuthenticationAuthorization
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();   //  < services.AddControllersWithViews(); < services.AddMvc();


            //services.AddAuthentication("Cookies").AddCookie();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();
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
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();

                endpoints.MapGet("/", async context =>
                {
                    string content = "<h1>ASP.NET Core ������ ���� �ʰ��� �ڵ�</h1>";

                    #region �޴�
                    content += "<a href=\"/Login\">�α���</a><br />";
                    content += "<a href=\"/Login/User\">�α���(user)</a><br />";
                    content += "<a href=\"/Login/Administrator\">�α���(Administrator)</a><br />";
                    content += "<a href=\"/Info\">����</a><br />";
                    content += "<a href=\"/InfoDetails\">����(Details)</a><br />";
                    content += "<a href=\"/InfoJson\">����(Json)</a><br />";
                    content += "<a href=\"/Logout\">�α׾ƿ�</a><br />";
                    content += "<hr />";
                    content += "<a href=\"/Landing/Index\">����������</a><br />";
                    content += "<a href=\"/Landing/Greeting\">ȯ��������</a><br />";
                    content += "<a href=\"/Greeting\">ȯ��������(Route)</a><br />";
                    content += "<hr />";
                    content += "<a href=\"/Dashboard\">����������</a><br />";
                    content += "<a href=\"/api/AuthService\">�α��� ����(Json)</a><br />";
                    #endregion


                    

                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(content);
                }); 

                #region Login
                endpoints.MapGet("/Login", async context =>
                {
                    var claims = new List<Claim>
                    {
                //new Claim(ClaimTypes.Name, "User Name")
                new Claim(ClaimTypes.Name, "���̵�")
                    };

                    //var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    //await context.SignInAsync("Cookies", claimsPrincipal);
                    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

                    string content = "<h3>�α��� �Ϸ�</h3>";
                    context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    await context.Response.WriteAsync(content);

                });
                #endregion

                #region Login/{Username}
                endpoints.MapGet("/Login/{Username}", async context =>
                {
                    var username = context.Request.RouteValues["Username"].ToString();
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, username),
                        new Claim(ClaimTypes.Name, username),
                        new Claim(ClaimTypes.Email, username + "@a.com"),
                        new Claim(ClaimTypes.Role, "Users"),
                        new Claim("���ϴ� �̸�", "���ϴ� ��"),
                    };

                    if (username == "Administrator")
                    {
                        claims.Add
                        (
                            new Claim(ClaimTypes.Role, "Administrators")
                        );
                    }

                    //var claimsIdentity = new ClaimsIdentity(claims, "Cookies");
                    //await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                    //await context.SignInAsync("Cookies", claimsPrincipal);
                    await context.SignInAsync(
                        CookieAuthenticationDefaults.AuthenticationScheme, 
                        claimsPrincipal, 
                        new AuthenticationProperties { IsPersistent = true}  // ��� ����
                        );

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

                #region InfoDetails
                endpoints.MapGet("/InfoDetails", async context =>
                {
                    string result = "";

                    if (context.User.Identity.IsAuthenticated)
                    {
                        result += $"<h3>�α��� �̸�: {context.User.Identity.Name}</h3>";
                        foreach (var claim in context.User.Claims)
                        {
                            result += $"{claim.Type} = {claim.Value} <br />";
                        }
                        if (context.User.IsInRole("Administrators") && context.User.IsInRole("Users") )
                        {
                            result += $"<br /> Administrators + Users ������ �ֽ��ϴ�. <br />";
                        }
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
                    //await context.SignOutAsync("Cookies");
                    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);


                    //context.Response.Headers["Content-Type"] = "text/html; charset=utf-8";
                    context.Response.Headers["Content-Type"] = "application/json; charset=utf-8"; //MIME Ÿ��
                    await context.Response.WriteAsync("<h3>�α׾ƿ� �Ϸ�</h3>");
                });
                #endregion

            });
        }
    }


    #region DTO
    public class ClaimDto
    {
        public string Type { get; set; }
        public string Value { get; set; }
    }
    #endregion

    #region MVC Controller
    [AllowAnonymous]
    #region LandingController
    public class LandingController : Controller
    {
        public IActionResult Index() => Content("������ ���� ����");

        [Authorize]
        [Route("/Greeting")]
        public IActionResult Greeting()
        {
            var roleName = HttpContext.User.IsInRole("Administrators") ? "������" : "�����";
            return Content($"<em>{ roleName}</em> ��, �ݰ����ϴ�.", "text/html", Encoding.Default);
        }
    }
    #endregion


    [Authorize(Roles = "Administrators")]
    public class DashboardController : Controller
    {
        public IActionResult Index() => Content("������ ��, �ݰ����ϴ�. ���� ����");
    }
    #endregion


    #region Web ApiController
    [ApiController]
    [Route("api/[controller]")]
    public class AuthServiceController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IEnumerable<ClaimDto> Get() =>
          HttpContext.User.Claims.Select(c => new ClaimDto { Type = c.Type, Value = c.Value });
    } 
    #endregion


}
