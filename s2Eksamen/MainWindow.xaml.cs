using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccess;
using Entities;
using Services;

namespace s2Eksamen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Repository repo;
        WeatherService weatherService;
        bool isEditing = false;
        public MainWindow()
        {
            InitializeComponent();
            repo = new();
            weatherService = new();
            avaliablePitchesList.ItemsSource = repo.GetAllPitches();
            pitchList.ItemsSource = repo.GetAllPitches();
            weatherLbl.Content = weatherService.GetWeather();
            
        }

        private void avaliablePitchesList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(avaliablePitchesList.SelectedItem != null)
            {
                Pitch p = (Pitch)avaliablePitchesList.SelectedItem;
                dGrid.ItemsSource = p.bookings;
            }

        }

        private void pitchList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void avaliablityBtn_Click(object sender, RoutedEventArgs e)
        {
            if (fromDatePicker.SelectedDate != null && toDatePicker.SelectedDate != null)
            {
                pitchList.ItemsSource = repo.GetAllAvaliablePitches(fromDatePicker.SelectedDate.Value, toDatePicker.SelectedDate.Value);
                createBtn.IsEnabled = true;
            }
            else
            {
                MessageBox.Show("Indsæt venligst datoer...");
            }
        }

        void ClearUI()
        {
            nameInput.Text = null;
            mailInput.Text = null;
            toDatePicker.SelectedDate = null;
            fromDatePicker.SelectedDate = null;
            searchFromDatePicker.SelectedDate = null;
            searchToDatePicker.SelectedDate = null;
            pitchList.ItemsSource = null;
            avaliablePitchesList.ItemsSource = null;
            dGrid.ItemsSource=null;
        }

        void UpdateUI()
        {
            ClearUI();
            createBtn.IsEnabled = false;
            pitchList.ItemsSource = repo.GetAllPitches();
            avaliablePitchesList.ItemsSource = repo.GetAllPitches();
        }



        private void toDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            createBtn.IsEnabled = false;
        }

        private void fromDatePicker_CalendarClosed(object sender, RoutedEventArgs e)
        {
            createBtn.IsEnabled = false;
        }

        private void createBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!isEditing)
            {
                Booker booker = new(1, nameInput.Text, mailInput.Text);
                Pitch p = pitchList.SelectedItem as Pitch;
                if (pitchList.SelectedItem != null)
                {
                    Booking b = new(1, fromDatePicker.SelectedDate.Value, toDatePicker.SelectedDate.Value, booker, p.id);
                    repo.AddNewBookingWithNewBooker(b, booker, p);
                    UpdateUI();

                }
                else
                {
                    MessageBox.Show("Vælg en plads");
                }
            }
            else
            {
                Booking b = (Booking)dGrid.SelectedItem;
                b.Start = fromDatePicker.SelectedDate.Value;
                b.End = toDatePicker.SelectedDate.Value;
                b.BookingBooker.Name = nameInput.Text;
                b.BookingBooker.Mail = mailInput.Text;
                if(pitchList.SelectedItem != null)
                {
                    Pitch p = pitchList.SelectedItem as Pitch;
                    b.PitchId = p.id;
                }
                repo.EditBooker(b.BookingBooker);
                repo.EditBooking(b);
                UpdateUI();
                createBtn.Content = "Opret";
                createBtn.IsEnabled = false;
                isEditing = false;
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            if(searchFromDatePicker.SelectedDate != null && searchToDatePicker.SelectedDate != null)
            {
                avaliablePitchesList.ItemsSource = null;
                avaliablePitchesList.ItemsSource = repo.GetAllAvaliablePitches(searchFromDatePicker.SelectedDate.Value, searchToDatePicker.SelectedDate.Value);
                MessageBox.Show(repo.GetAllAvaliablePitches(searchFromDatePicker.SelectedDate.Value, searchToDatePicker.SelectedDate.Value).Count.ToString());
            }
            else
            {
                MessageBox.Show("Vælg datoer");
            }
        }

        private void dGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(dGrid.SelectedItem != null)
            { 
                Booking booking = (Booking)dGrid.SelectedItem;
                nameInput.Text = booking.BookingBooker.Name;
                mailInput.Text = booking.BookingBooker.Mail;
                fromDatePicker.SelectedDate = booking.Start;
                toDatePicker.SelectedDate = booking.End;
                createBtn.Content = "Rediger";
                isEditing = true;
                foreach(Pitch p in pitchList.ItemsSource)
                {
                    if(p.id == booking.PitchId)
                    {
                        pitchList.SelectedItem = p;
                    }
                }
                createBtn.IsEnabled = true;
            }
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if(dGrid.SelectedItem != null)
            {
                repo.DeleteBooking(dGrid.SelectedItem as Booking);
                UpdateUI();
            }
            else
            {
                MessageBox.Show("Vælg en booking at slette");
            }
        }
    }
}
