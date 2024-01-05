using EPiServer.Cms.Shell;
using EPiServer.Cms.Shell.UI.ObjectEditing.EditorDescriptors;
using EPiServer.Cms.UI.AspNetIdentity;
using EPiServer.ContentApi.Core.Internal;
using EPiServer.ContentApi.Core.Serialization;
using EPiServer.Data;
using EPiServer.Data.Dynamic;
using EPiServer.DependencyInjection;
using EPiServer.Framework.Web.Resources;
using EPiServer.Scheduler;
using EPiServer.ServiceLocation;
using EPiServer.Web;
using EPiServer.Web.Routing;
using Microsoft.AspNetCore.Mvc.Razor;
using Newtonsoft.Json;
using Optimizely.ContentGraph.Cms.Configuration;
using razor_poc.Infrastructure.Constants;
using razor_poc.Infrastructure.ContentDeliveryApi;
using razor_poc.Infrastructure.ContentDeliveryApi.ModelConverters;
using razor_poc.Infrastructure.Rendering;
using razor_poc.Infrastructure.Services;

namespace razor_poc
{
    public class Startup
    {
        private readonly IWebHostEnvironment _webHostingEnvironment;
        private readonly IConfiguration _configuration;
        public Startup(IWebHostEnvironment webHostingEnvironment, IConfiguration configuration)
        {
            _webHostingEnvironment = webHostingEnvironment;
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_webHostingEnvironment.IsDevelopment())
            {
                AppDomain.CurrentDomain.SetData("DataDirectory", Path.Combine(_webHostingEnvironment.ContentRootPath, "App_Data"));

                services.Configure<SchedulerOptions>(options => options.Enabled = false);
            }

            var connectionstring = _configuration.GetConnectionString("EPiServerDB");

            if (_webHostingEnvironment.IsDevelopment())
                services.Configure<ClientResourceOptions>(uiOptions => { uiOptions.Debug = true; });
            else
                services.AddCmsCloudPlatformSupport(_configuration);

            // Required by Wangkanai.Detection
            services.AddDetection();

            services.Configure<DataAccessOptions>(o =>
            {
                o.SetConnectionString(connectionstring);
            });

            services
                .AddCmsAspNetIdentity<ApplicationUser>()
                .AddCms()
                .AddAdminUserRegistration()
                .AddEmbeddedLocalization<Startup>();

            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromSeconds(10);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });

            services.AddMvc().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
            });

            //services.AddWebOptimizer();
            services.AddSwaggerGen(c =>
            {
                //c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIs", Version = "v1" });
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
                c.IgnoreObsoleteActions();
                c.IgnoreObsoleteProperties();
                c.CustomSchemaIds(type => type.FullName);
            });
            services.AddEndpointsApiExplorer();

            services.ConfigureContentApiOptions(o =>
            {
                o.EnablePreviewFeatures = true;
                o.EnablePreviewMode = true;
                o.IncludeEmptyContentProperties = false;
                o.ForceAbsolute = false;
                o.FlattenPropertyModel = true;
                o.IncludeMasterLanguage = false;
                o.IncludeMasterLanguage = false;
                o.IncludeMetadataPropertiesPreview = false;
                o.IncludeInternalContentRoots = true;
                o.IncludeSiteHosts = true;
            });
           
            // Content Delivery API
            services.AddContentDeliveryApi(o =>
            {
                o.SiteDefinitionApiEnabled = true;                
            })
                .WithFriendlyUrl()                
                .WithSiteBasedCors()
                .WithSiteBasedCors();

            services.AddContentGraph(configureOptions: options =>
            {
                options.IncludeInheritanceInContentType = true;
                //options.Include.ContentTypes = new[] { typeof(BasePage).Name, typeof(BaseBlock).Name };
                //options.OnlySyncContentTypesInWhitelistToSchema = true;
                options.ContentVersionSyncMode = Optimizely.ContentGraph.Cms.Configuration.ContentVersionSyncMode.All;
                options.SyncReferencingContents = true;
                options.EnablePreviewTokens = true;
                options.Events.OnGeneratingPreviewUrl = ctx =>
                {
                    // ctx.UpdateUrl(new Uri("/test?id=" + ctx.ContentReference, UriKind.RelativeOrAbsolute));
                    return Task.CompletedTask;
                };

            });
            services.Configure<EventIndexingOptions>(ops =>
            {
                ops.SyncContentTypesOnInit = true;
            });

            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowAnyOrigin",
                    builder =>
                    {
                        builder.WithExposedContentDeliveryApiHeaders();
                        builder.AllowAnyHeader();
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                    });
            });

            services.Configure<DynamicDataStoreOptions>(options =>
            {
                options.AutoRemapStores = true;
                options.AutoResolveTypes = true;
                options.DeleteAllOperationTimeout = TimeSpan.FromSeconds(10);
            });

          
            services.Configure<RazorViewEngineOptions>(options => options.ViewLocationExpanders.Add(new SiteViewEngineLocationExpander()));

            services.Configure<DisplayOptions>(displayOption =>
            {
                displayOption.Add("full", "/displayoptions/full", ContentAreaTags.FullWidth, string.Empty, "epi-icon__layout--full");
                displayOption.Add("wide", "/displayoptions/wide", ContentAreaTags.WideWidth, string.Empty, "epi-icon__layout--wide");
                displayOption.Add("half", "/displayoptions/half", ContentAreaTags.HalfWidth, string.Empty, "epi-icon__layout--half");
                displayOption.Add("narrow", "/displayoptions/narrow", ContentAreaTags.NarrowWidth, string.Empty, "epi-icon__layout--narrow");
            });

           
            services.AddSingleton<IPropertyConverterProvider, CustomPropertyConverterProvider>();
            services.AddSingleton<IPropertyConverter, CustomPropertyReferenceConverter>();
            services.AddSingleton<IPropertyConverter, CustomLinkCollectionConverter>();           
            services.AddSingleton<IPropertyConverter, UrlModelConverter>();
            services.AddSingleton<IContentAreaLoader, CustomContentAreaLoader>();
         

            services.AddSingleton<IContentConverter, CustomContentConverter>();
            services.AddSingleton<IContentConverterProvider, CustomContentConverterProvider>();
            services.AddSingleton<IContentHelper, ContentHelper>();
            services.AddSingleton<ContentLoaderService, CustomContentLoaderService>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();


        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            //app.UseWebOptimizer();

            // Required by Wangkanai.Detection
            app.UseDetection();
            app.UseSession();
            app.UseStaticFiles();
         
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI();
            app.UseCors("AllowAnyOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapContent();
                endpoints.MapControllerRoute(
                  name: "default",
                  pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
