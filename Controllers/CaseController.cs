using CaseManagmentAPI.DataContext;
using CaseManagmentAPI.Models;
using CaseManagmentAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CaseManagmentAPI.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CaseController: ControllerBase
    {
        private IUserService _userService;
        private CMDataContext ctx = new CMDataContext();

        private readonly IConfiguration Configuration;



        public CaseController(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        //Add new Case
        [Route("AddCase")]
        [HttpPost]
        public IActionResult AddCase([FromBody] CMCase req)
        {

            var db = new CMDataContext();
            var type = db.CMCaseType.Where(c => c.ID == req.cMCaseType.ID ).FirstOrDefault();
            var customerCare = db.CMCustomerCare.Where(c => c.ID == req.customerCare.ID).FirstOrDefault();
            var customer = db.CMCustomer.Where(c => c.ID == req.cMCustomer.ID).FirstOrDefault();
            CMCase s = new CMCase();
            s.description = req.description;
            s.customerCare = customerCare;
            s.customerId = customer.ID;
            s.cMCustomer = customer;
            s.cMCaseType = type;
            s.caseTypeId = type.ID;
            s.status = req.status;
            s.customerCareId = 0;
            s.state = 0;

            db.CMCase.Add(s);

            var r = db.SaveChanges();

            if (r == 1)

            {
                return Ok(r);
            }

            else

            {
                return NotFound();
            }

        }

        // Update Case
        [Route("UpdateCase")]
        [HttpPut]
        public IActionResult UpdateCase([FromBody] CMCase req)
        {

            var db = new CMDataContext();
            var cMCase = db.CMCase.Where(c => c.ID == req.ID).FirstOrDefault();

            if (cMCase == null)
            {
                return NotFound();
            }

            cMCase.description = req.description;
    


            db.CMCase.Attach(cMCase);

            var r = db.SaveChanges();

            if (r == 1)

            {
                return Ok(r);
            }

            else

            {
                return NotFound();
            }

        }

        // Assign Case
        [Route("AssignCase")]
        [HttpPut]
        public IActionResult AssignCase([FromBody] CMCase req)
        {

            var db = new CMDataContext();
            var cMCase = db.CMCase.Where(c => c.ID == req.ID).FirstOrDefault();

            if (cMCase == null)
            {
                return NotFound();
            }

            var customerCare = db.CMCustomerCare.Where(c => c.ID == req.customerCare.ID).FirstOrDefault();
            cMCase.customerCare = customerCare;
            cMCase.customerCare.ID = customerCare.ID;

            cMCase.status = "Assigned";

            db.CMCase.Attach(cMCase);

            var r = db.SaveChanges();

            if (r == 1)

            {
                return Ok(r);
            }

            else

            {
                return NotFound();
            }

        }

        // Delete Case
        [Route("DeleteCase")]
        [HttpDelete]
        public IActionResult DeleteCase(int id)
        {
            var db = new CMDataContext();
            var newCase = db.CMCase.Find(id);

            if (newCase == null)
            {
                return NotFound();
            }
            else
            {
                newCase.state = 1;
                db.CMCase.Attach(newCase);
                db.SaveChanges();
            }

            return Ok(newCase);

        }

        // Get Case With id
        [Route("GetCase")]
        [HttpGet]
        public IActionResult GetCase(int id)
        {
            var db = new CMDataContext();
            CMCase newCase = db.CMCase.Find(id);

            if (newCase == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(newCase);

            }
        }

        // Get Customer Case With id
        [Route("GetCaseByCustomer")]
        [HttpGet]
        public IActionResult GetCaseByCustomer(int id)
        {
            var db = new CMDataContext();
            var caseByCustomers = db.CMCase.Where(c => c.cMCustomer.ID == id).ToList();

            if (caseByCustomers == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(caseByCustomers);

            }
        }


        // Get all cases
        [Route("Cases")]
        [HttpGet]
        public IActionResult Cases()
        {
            var db = new CMDataContext();
            var cases = db.CMCase.ToList();

            if (cases == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cases);

            }
        }

        // Get Case With status
        [Route("CasesByStatus")]
        [HttpGet]
        public IActionResult CasesByStatus(string status)
        {
            var db = new CMDataContext();
            var cases = db.CMCase.Where(c => c.status == status).ToList();

            if (cases == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(cases);

            }
        }

    }

}
