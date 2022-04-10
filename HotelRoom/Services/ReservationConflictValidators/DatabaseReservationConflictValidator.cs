using HotelRoom.DbContexts;
using HotelRoom.DTOs;
using HotelRoom.Models;
using HotelRoom.Services.ReservationProviders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelRoom.Services.ReservationConflictValidators
{
    public class DatabaseReservationConflictValidator : IReservationConflictValidator
    {
        private readonly ReservoomDbContextFactory _dbContextFactory;

        public DatabaseReservationConflictValidator(ReservoomDbContextFactory dbContextFactory)
        {
            _dbContextFactory = dbContextFactory;
        }

        public async Task<Reservation> GetConflictingReservation(Reservation reservation)
        {
            using (ReservoomDbContext context = _dbContextFactory.CreateDbContext())
            {
                ReservationDTO reservationDTO = await context.Reservations
                     .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
                     .Where(r => r.FloorNumber == reservation.RoomID.FloorNumber)
                     .Where(r => r.EndTime > reservation.StartTime)
                     .Where(r => r.StartTime < reservation.EndTime)
                    .FirstOrDefaultAsync();

                if(reservationDTO == null)
                {
                    return null;
                }

                return ToReservation(reservationDTO);
            }
        }

        private static Reservation ToReservation(ReservationDTO r)
        {
            return new Reservation(new RoomID(r.FloorNumber, r.RoomNumber),
                    r.Username, r.StartTime, r.EndTime);
        }
    }
}
