using EBP.API.Mappers;
using EBP.API.Models;
using EBP.Application.Commands;
using EBP.Application.Constants;
using EBP.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EBP.API.Controllers
{
    [ApiController]
    [Authorize(Roles = AppRoles.User)]
    [Route("api/user")]
    public class UserController(ISender _sender) : ControllerBase
    {
        /// <summary>
        /// Get events with optional filtering by ticket type. If ticketType is provided, only events that have tickets of the specified type will be returned. If ticketType is null, all events will be returned.
        /// </summary>
        /// <param name="ticketType">The type of ticket to filter events by. If null, all events will be returned.</param>
        /// <returns>List of events matching the specified ticket type filter.</returns>
        [HttpGet("events")]
        public async Task<IEnumerable<EventResponse>> Get(TicketTypeContract? ticketType)
        {
            var query = new GetEventsQuery(ticketType.ToDomain());
            var result = await _sender.Send(query);
            return result.ToResponses();
        }

        /// <summary>
        /// Books tickets for a specific event. The request body should contain the number of regular, VIP, and student tickets to be booked. The endpoint will return a booking ID that can be used for further actions such as releasing, submitting, or canceling the booking.
        /// </summary>
        /// <param name="eventId">The ID of the event for which tickets are being booked.</param>
        /// <param name="bookTicketRequest">The request containing the number of tickets to be booked for each ticket type.</param>
        /// <returns>The booking ID of the newly created booking.</returns>
        [HttpPost("events/{eventId}/book")]
        public async Task<IActionResult> Book([FromRoute] Guid eventId, [FromBody] BookTicketRequest bookTicketRequest)
        {
            var command = new BookEventTicketsCommand(
                eventId,
                bookTicketRequest.ReqularCount,
                bookTicketRequest.VipCount,
                bookTicketRequest.StudentCount);
            var bookingId = await _sender.Send(command);
            return Ok(new { bookingId });
        }

        /// <summary>
        /// Releases a booking with the specified identifier.
        /// </summary>
        /// <param name="bookingId">The unique identifier of the booking to release.</param>
        /// <returns>An HTTP 200 OK response if the booking is released successfully.</returns>
        [HttpPost("bookings/{bookingId}/release")]
        public async Task<IActionResult> Release([FromRoute] Guid bookingId)
        {
            var command = new ReleaseBookingCommand(bookingId);
            await _sender.Send(command);
            return Ok();
        }

        /// <summary>
        /// Submits a booking to use it for an event. 
        /// </summary>
        /// <param name="bookingId">The unique identifier of the booking to submit.</param>
        /// <returns>An HTTP 200 OK response if the booking is submitted successfully.</returns>
        [HttpPost("bookings/{bookingId}/submit")]
        public async Task<IActionResult> Submit([FromRoute] Guid bookingId)
        {
            var command = new SubmitBookingCommand(bookingId);
            await _sender.Send(command);
            return Ok();
        }

        /// <summary>
        /// Cancels a booking with the specified identifier. It can be canceled any time before the event starts, but once the event starts, there will not be refunds of the booking.
        /// </summary>
        /// <param name="bookingId">The unique identifier of the booking to cancel.</param>
        /// <returns>An HTTP 200 OK response if the cancellation is successful.</returns>
        [HttpPost("bookings/{bookingId}/cancel")]
        public async Task<IActionResult> Cancel([FromRoute] Guid bookingId)
        {
            var command = new CancelBookingCommand(bookingId);
            await _sender.Send(command);
            return Ok();
        }

        /// <summary>
        /// Retrieves the booking history for the current user.
        /// </summary>
        /// <returns>A collection of user's booking history.</returns>
        [HttpPost("bookings/history")]
        public async Task<IEnumerable<TicketResponse>> History()
        {
            return null;
        }
    }
}
