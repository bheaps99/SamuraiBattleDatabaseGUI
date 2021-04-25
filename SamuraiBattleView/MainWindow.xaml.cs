using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using DomainClasses;
using Repository2;
using ViewModel2;
using SamuraiApp.Service;
using System.Linq;

namespace SamuraiBattleView
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string csvSamurai = AppDomain.CurrentDomain.BaseDirectory + "SamuraiData.csv";
        string csvBattle = AppDomain.CurrentDomain.BaseDirectory + "BattleData.csv";
        string csvBattleSamurai = AppDomain.CurrentDomain.BaseDirectory + "BattleSamuraiData.csv";

        public MainWindow()
        {
            IRepository repository;
            var ctx = new SamuraiAppEntities();
            if (ctx.Database.Exists())
            {
                repository = new DBRepository(); 
                if (!ctx.BattleEntities.Any()) { ctx.BattleEntities.AddRange(SeedData.SeedBattle()); }
                if (!ctx.SamuraiEntities.Any()) { ctx.SamuraiEntities.AddRange(SeedData.SeedSamurai()); }
                if (!ctx.BattleSamuraiEntities.Any()) { ctx.BattleSamuraiEntities.AddRange(SeedData.SeedBattleSamurai()); }
                InstantiateTimer();
            }
            else repository = new CSVRepository();

            InitializeComponent();
            DataContext = new SamuraiBattleViewModel(repository);
            
        }
        private void InstantiateTimer()
        {
            var timer = new System.Timers.Timer(300000);// 5 min
            timer.Elapsed += Timer_Elapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
        }

        private async void Timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            IRepository repo = new DBRepository();

            IEnumerable<Samurai> samurais = await Task.Run(() => repo.GetAllSamurai());
            IEnumerable<Battle> battles = await Task.Run(() => repo.GetAllBattle());

            await Task.Run(() => WriteToCSV(samurais, csvSamurai));
            await Task.Run(() => WriteToCSV(battles, csvBattle));
        }

        private static void WriteToCSV(IEnumerable<object> objList, string filePath)
        {
            using (var writer = new StreamWriter(filePath))
            using (var csv = new CsvHelper.CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csv.WriteRecords(objList);
            }

        }
    }
}
