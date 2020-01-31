# [SignalR Demo Solution](http://kamranaeff1994-002-site2.ctempurl.com)


Startup faylı üzrə konfigurasiyaların tətbiq edilməsi

###  SignalR dəstəyinin Core containerə əlavə edilməsi
<pre>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
               <b>services.AddSignalR(); </b>
        }
</pre>

###  SignalR dəstəyinin aktivləşdirilməsi
<pre>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = "/npm",
                FileProvider = new PhysicalFileProvider(
                    Path.Combine(Directory.GetCurrentDirectory(), "node_modules"))
            });
            
            <b>
            app.UseSignalR(cfg=> {

                cfg.MapHub&lt;ChatHub&gt;("/chat");
            
            });
            </b>

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=home}/{action=index}/{id?}");
            });
        }}
</pre>


###  SignalR ilə realtime idarəedicinin-Hub yaradılması

<pre>
    public interface IChatHub
    {
        Task Received(string sender,string message);
    }

    public class ChatHub : Hub&lt;IChatHub&gt;
    {
        static ConcurrentDictionary&lt;string,string&gt; clients = new ConcurrentDictionary&lt;string, string&gt;();

        async public Task SendMessage(string to, string message)
        {
            var httpContext = Context.GetHttpContext();
            var email = httpContext.Request.Query["email"];

            if (clients.TryGetValue(to, out string connectionId))
            {
                await Clients.Client(connectionId).Received(email, message);
                return;
            }
            await Clients.Caller.Received("From Server", $"{to} is disconnected!");
        }


        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var email = httpContext.Request.Query["email"];

            if (!string.IsNullOrWhiteSpace(email))
            {
                clients.AddOrUpdate(email, Context.ConnectionId, (k, v) => Context.ConnectionId);

                Clients.Caller.Received("FromServer","Welcome");
            }
            

            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
</pre>
