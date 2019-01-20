
using SEContactManager.ApplicationCore.Interfaces.Repositories;
using SEContactManager.ApplicationCore.Interfaces.Services;
using SEContactManager.Tests.Utilities;
using SEContactManager.UI.Web;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SEContactManager.Tests.Controllers
{   
    public class CustomerControllerTest : TestFixture<Startup>
    {

        private ICustomerRepository Repository { get; set; }

        private ICustomerService Service { get; }

        [Fact]
        public void something()
        {
            
        }
    }
}
