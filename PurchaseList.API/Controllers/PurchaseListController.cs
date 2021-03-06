﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PurchaseList.API.Requests;
using System.Threading.Tasks;

namespace PurchaseList.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseListController : ApiBaseController
    {
        public PurchaseListController(IMediator mediator) : base(mediator) { }

        /// <summary>
        /// Method responsible for calculate the user's bills
        /// </summary>
        /// <remarks>
        /// Sample Request:
        /// 
        /// {
        ///     "items": [
        ///         {
        ///             "name": "coffee",
        ///             "quantity": 1,
        ///             "price": 5
        ///         },
        ///         {
        ///             "name": "tea",
        ///             "quantity": 2,
        ///             "price": 3
        ///         },
        ///         {
        ///             "name": "sugar",
        ///             "quantity": 5,
        ///             "price": 1
        ///         }
        ///     ],
        ///     "emails": [
        ///         "email1@email.com",
        ///         "email2@email.com",
        ///         "email3@email.com"
        ///     ]
        /// }
        ///     
        /// </remarks>
        /// <param name="request"></param>
        /// <returns>A dictionary where the key is the user's email and the value is the bill.</returns>
        /// <response code="200">Bills successfully calculated.</response>
        /// <response code="400">Invalid request.</response>
        /// <response code="500">Internal Server Error.</response>
        [HttpPost("[action]")]
        public async Task<IActionResult> CalculateBills([FromBody] CalculateBillsRequest request)
                => await SendCommand(request);
    }
}