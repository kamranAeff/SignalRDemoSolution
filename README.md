# [SignalR Demo Solution](http://kamranaeff1994-002-site2.ctempurl.com)


Startup faylı üzrə konfigurasiyaların tətbiq edilməsi

###  SignalR dəstyinin Core containerə əlavə edilməsi
<code>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
           <ins>services.AddSignalR();</ins>
        }
</code>

###  SignalR dəstyinin aktivləşdirilməsi
<code>
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
            
            <ins>
            app.UseSignalR(cfg=> {

                cfg.MapHub<ChatHub>("/chat");
            
            });
            </ins>
            
            app.UseMvc(routes =>
            {
                routes.MapRoute("default", "{controller=home}/{action=index}/{id?}");
            });
        }}
</code>
