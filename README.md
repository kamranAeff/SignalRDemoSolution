# [SignalR Demo Solution](http://kamranaeff1994-002-site2.ctempurl.com)


Startup faylı üzrə konfigurasiyaların tətbiq edilməsi

###  SignalR dəstyinin Core containerə əlavə edilməsi
<pre>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
               <span style="color:#e74c3c">services.AddSignalR(); </span>
            }
</pre>

###  SignalR dəstyinin aktivləşdirilməsi
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
            
            <span style="color:#e74c3c">
            app.UseSignalR(cfg=> {

                cfg.MapHub<ChatHub>("/chat");
            
            });
            </span>

            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=home}/{action=index}/{id?}");
            });
        }}
</pre>
