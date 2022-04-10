using HotelRoom.Models;
using HotelRoom.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace HotelRoom.Commands
{
    public class LoadReservationsCommand : AsyncCommandBase
    {
        private readonly ReservationListingViewModel _viewmodel;
        private readonly Hotel _hotel;

        public LoadReservationsCommand(ReservationListingViewModel viewmodel, Hotel hotel)
        {
            _hotel = hotel;
            _viewmodel = viewmodel;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                IEnumerable<Reservation> reservations = await _hotel.GetAllReservations();
                _viewmodel.UpdateReservations(reservations);

            }
            catch (Exception)
            {
                MessageBox.Show("Failed.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

            }
        }
    }
}
