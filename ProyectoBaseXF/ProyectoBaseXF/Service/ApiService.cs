using ProyectoBaseXF.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoBaseXF.Service
{
    public class ApiService : ApiServiceBase
    {
        public ApiService() : base()
        {
            client.BaseAddress = new Uri("https://api.autogo.com.ec/");
        }
    }
}
