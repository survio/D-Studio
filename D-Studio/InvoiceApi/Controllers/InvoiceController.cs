using System;
using Electronic_Invoice;
using InvoiceApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace InvoiceApi.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class InvoiceController : ControllerBase
    {
        private IInvoiceService service;


        public InvoiceController(IInvoiceService service)
        {
            this.service = service;
        }


        [HttpGet("{Id}")]
        
        public Invoice Get(int id)
        {
            return service.GetById(id);
        }
        [HttpGet]
        public IEnumerable<Invoice> Get([FromQuery]InvoiceFilterParameters filterParameters, [FromQuery]PageParameters pageParameters)
        {
            return service.GetAll(filterParameters,pageParameters);
        }

        [HttpPost]
        public HttpResponseMessage Add([FromBody]Invoice invoice)
        {
            service.Add(invoice);
            return new HttpResponseMessage(HttpStatusCode.Created);
        }

        [HttpPatch]
        public HttpResponseMessage Change([FromQuery]int id, [FromBody]Invoice changeTo)
        {
            return service.Change(id, changeTo) ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.NotModified);
        }

    }
}
