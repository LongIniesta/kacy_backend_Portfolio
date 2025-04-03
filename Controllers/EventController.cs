using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Services.DTO.Request;
using Services.DTO.Response;
using Services.Services.ImplementServices;
using Services.Services.InterfaceServices;

namespace Kacy_Backend.Controllers
{
    [Route("api/event")]
    [ApiController]
    public class EventController : Controller
    {
        private readonly IEventService _eventService;
        private const string ADMIN = "Admin";
        private const string CUSTOMER = "Customer";
        private const string STAFF = "Staff";
        public EventController(IEventService eventService)
        {
            _eventService = eventService;
        }

        /// <summary>
        /// Create an event
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = ADMIN)]
        [HttpPost]
        public async Task<ActionResult<EventResponse>> createEvent([FromBody] CreateEventRequest request) {
            var rs = await _eventService.CreateEvent(request);
            return Ok(rs);
        }

        /// <summary>
        /// Get an event by event's id
        /// </summary>
        /// <param name="eventId">Event's id</param>
        [Authorize(Policy = CUSTOMER)]
        [HttpGet("{eventId:int}")]
        public async Task<ActionResult<EventResponse>> getEventById(int eventId) {
            var rs = await _eventService.GetEventById(eventId);
            return Ok(rs);
        }

        /// <summary>Get list events</summary>
        /// <param name="request">Filter</param>
        /// <param name="paging">Paging</param>
        [Authorize(Policy = CUSTOMER)]
        [HttpGet]
        public async Task<ActionResult<PagedResults<EventResponse>>> getEvents([FromQuery] EventRequest request, [FromQuery] PagingRequest paging) { 
            var rs = await _eventService.GetEvents(request, paging);
            return Ok(rs);
        }

        /// <summary>Update an event</summary>
        /// <param name="request">Event's information</param>
        /// <param name="eventId">Event's Id</param>
        [Authorize(Policy = ADMIN)]
        [HttpPut("eventId:int")]
        public async Task<ActionResult<EventResponse>> updateEvent([FromBody]CreateEventRequest request, int eventId) { 
            var rs = await _eventService.UpdateEvent(request, eventId);
            return Ok(rs);
        }

        ///<summary>Delete an event</summary>
        ///<param name="eventId">Event's Id</param>
        [Authorize(Policy = ADMIN)]
        [HttpDelete("eventId:int")]
        public async Task<ActionResult<EventResponse>> deleteEvent(int eventId) { 
            var rs = await _eventService.DeleteEventById(eventId);
            return Ok(rs);
        }

    }
}
