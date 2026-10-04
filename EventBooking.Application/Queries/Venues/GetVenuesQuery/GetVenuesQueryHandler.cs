using MediatR;
using EventBooking.Application.DTOs;
using EventBooking.Application.Interfaces;

namespace EventBooking.Application.Queries.Venues.GetVenues;

public class GetVenuesQueryHandler
    : IRequestHandler<GetVenuesQuery, IReadOnlyList<VenueDto>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetVenuesQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IReadOnlyList<VenueDto>> Handle(
        GetVenuesQuery request,
        CancellationToken cancellationToken)
    {
        var venues = await _unitOfWork.GetVenuesAsync(
            cancellationToken);

        return venues
            .Select(venue => new VenueDto
            {
                Id = venue.Id,
                Name = venue.Name,
                Address = venue.Address,
                Capacity = venue.Capacity,
                StartTime = venue.StartTime,
                EndTime = venue.EndTime,
                Description = venue.Description
            })
            .ToList();
    }
}