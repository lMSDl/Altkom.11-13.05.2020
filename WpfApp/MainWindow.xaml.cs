using DAL.Services;
using Models;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private IServiceAsync<Person> Service = new PersonServiceAsync();

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
        }

        public ICollection<Person> People { get; set; }
        public Person SelectedPerson { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private async void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            People = await Service.ReadAsync();
            PropertyChanged(this, new PropertyChangedEventArgs(nameof(People)));
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson == null)
                return;
            var dialog = new PersonWindow(SelectedPerson);
            dialog.ShowDialog();
            await Service.UpdateAsync(SelectedPerson);
        }

        private async void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedPerson == null)
                return;
            await Service.DeleteAsync(SelectedPerson.GetType(), SelectedPerson.Id);
            RefreshButton_Click(this, new RoutedEventArgs());
        }
    }
}
