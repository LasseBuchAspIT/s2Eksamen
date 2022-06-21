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

namespace s2Eksamen
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Repository repo;
        public MainWindow()
        {
            InitializeComponent();
            repo = new();
            avaliablePitchesList.ItemsSource = repo.GetAllPitches();
            pitchList.ItemsSource = repo.GetAllPitches();

            
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
            Booker booker = new(1, nameInput.Text, mailInput.Text);
            Pitch p = pitchList.SelectedItem as Pitch;
            if(pitchList.SelectedItem != null)
            {
                Booking b = new(1, fromDatePicker.SelectedDate.Value, toDatePicker.SelectedDate.Value, booker, p.id);
                MessageBox.Show($"{b.PitchId.ToString()}, {b.Id.ToString()}, {b.BookingBooker.Id.ToString()}");
                repo.AddNewBookingWithNewBooker(b, booker, p);

            }
            else
            {
                MessageBox.Show("Vælg en plads");
            }
        }

        private void searchBtn_Click(object sender, RoutedEventArgs e)
        {
            if(searchFromDatePicker != null && searchToDatePicker != null)
            {
                avaliablePitchesList.ItemsSource = repo.GetAllAvaliablePitches(searchFromDatePicker.SelectedDate.Value, searchToDatePicker.SelectedDate.Value);
            }
            else
            {
                MessageBox.Show("Vælg datoer");
            }
        }
    }
}
