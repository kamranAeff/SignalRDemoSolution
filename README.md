# [SignalR Demo Solution](http://kamranaeff1994-002-site2.ctempurl.com)


Startup faylı üzrə konfigurasiyaların tətbiq edilməsi

###  SignalR dəstyinin Core containerə əlavə edilməsi
<pre>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();
               <b>services.AddSignalR(); </b>
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
