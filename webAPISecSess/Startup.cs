using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Data.Entity;
using webAPISecSess.Models;

[assembly: OwinStartup(typeof(webAPISecSess.Startup))]

namespace webAPISecSess
{
    public partial class Startup
    {

        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
