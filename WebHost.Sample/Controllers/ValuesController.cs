namespace Sample.Controllers
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using NServiceBus;

    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IMessageSession session;

        public ValuesController(IMessageSession session)
        {
            this.session = session;
        }

        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            await session.SendLocal(new MyOtherMessage());

            return new[] {"value1", "value2"};
        }
    }
}