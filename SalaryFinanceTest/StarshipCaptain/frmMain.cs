using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using StarshipCaptain.Model;

namespace StarshipCaptain
{
    public partial class frmMain : Form
    {

        /// <summary>
        /// The file will be created here
        /// </summary>
        string jsonFile = Path.Combine(Application.StartupPath, "universe.json");

        /// <summary>
        /// The location of the home world
        /// </summary>
        UniverseLocation home = new UniverseLocation(
            new UniverseCoordinate("123.123.99.1"),
            new UniverseCoordinate("98.98.11.1"),
            new UniverseCoordinate("456.456.99.9")
            );


        public frmMain()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Generate the 15000 coordinates and save them to a text file in JSON format
        /// </summary>
        private void btnGenerateCoordinates_Click(object sender, EventArgs e)
        {

            txtLog.AppendText(string.Format("\r\n{0} {1}\r\n", DateTime.Now.ToString("HH:mm:ss"), "Creating coordinates..."));

            //Create the universe with the 15000 coordinates 
            Universe universe = new Universe(true);

            txtLog.AppendText(string.Format("{0} {1}\r\n", DateTime.Now.ToString("HH:mm:ss"), "OK"));

            //Save the JSON serialized text in a text file
            File.WriteAllText(jsonFile, JsonConvert.SerializeObject(universe));

            txtLog.AppendText(string.Format("{0} {1} {2}\r\n", DateTime.Now.ToString("HH:mm:ss"), "JSON data created", jsonFile));

        }

        /// <summary>
        /// Recreate the universe by reading and deserializing the JSON data.
        /// Find the closest habitable planet to the home world. Spend some time travelling there and colonizing the planet.
        /// Then repeat by finding the closest habitable planet from there.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnExplore_Click(object sender, EventArgs e)
        {
            try
            {

                txtLog.AppendText(string.Format("\r\n{0} {1}\r\n", DateTime.Now.ToString("HH:mm:ss"), "Reading coordinates from file..."));

                //Read the JSON data back into a Universe instance
                string json = File.ReadAllText(jsonFile);
                Universe universe = JsonConvert.DeserializeObject<Universe>(json);
                txtLog.AppendText(string.Format("{0} {1}\r\n", DateTime.Now.ToString("HH:mm:ss"), "OK"));

                //find all habitable planets
                var planets = universe.cosmos.Where(l => l.Type == UniverseLocation.UniverseLocationType.Planet && l.IsHabitable).ToList();

                txtLog.AppendText(string.Format("{0} Total Habitable Planets: {1}\r\n", DateTime.Now.ToString("HH:mm:ss"), planets.Count));

                //find the immediate Neighbor
                var immediateNeighbor = (UniverseLocation)planets.OrderBy(p => Universe.GetDistanceBetweenLocations(home, p)).Take(1).ToList()[0];

                //travel to the immediate neighbor and then repeat until 10 planets have been colonized
                int totalSecondsPassed = 0;
                int secondsIn24H = 24 * 60 * 60;
                long totalAreaColonized = 0;
                long totalDistanceCovered = 0;

                //this list contains the list of colonized planets, in the order that we have traveled to them.
                List<UniverseLocation> colonizedPlanets = new List<UniverseLocation>();

                //while (totalSecondsPassed < secondsIn24H) //This requirement wouldn't work because colonizing even one small planet would take more than 24h, using the numbers provided in the assignment
                while (colonizedPlanets.Count<10)
                {

                    //spend 10 minutes traveling to the closest planet
                    totalSecondsPassed = totalSecondsPassed + 10 * 60;

                    long distance = Universe.GetDistanceBetweenLocations(home, immediateNeighbor);
                    txtLog.AppendText(string.Format("{0} {1}: {2}\r\n", DateTime.Now.ToString("HH:mm:ss"), "Colonizing planet", immediateNeighbor.ToString()));
                    txtLog.AppendText(string.Format("{0} {1}: {2}\r\n", DateTime.Now.ToString("HH:mm:ss"), "Distace from current location", distance));
                    totalDistanceCovered = totalDistanceCovered + distance;

                    //spend some time inhabiting the planet. We only need to colonize more than 50% of the planet
                    totalSecondsPassed = totalSecondsPassed + (int)Math.Round(0.43 * (immediateNeighbor.PlanetArea / 2 + 1), 0);

                    home = immediateNeighbor;
                    totalAreaColonized = totalAreaColonized + immediateNeighbor.PlanetArea;

                    //remove the colonized planet from the list of available planets and add it to the list of colonized planets
                    planets.Remove(immediateNeighbor);
                    colonizedPlanets.Add(immediateNeighbor);

                    //find the immediate Neighbor from where we are now
                    immediateNeighbor = (UniverseLocation)planets.OrderBy(p => Universe.GetDistanceBetweenLocations(home, p)).Take(1).ToList()[0];

                }

                TimeSpan timePassed = TimeSpan.FromSeconds(totalSecondsPassed);

                txtLog.AppendText(string.Format("{0} {1}: {2}\r\n", DateTime.Now.ToString("HH:mm:ss"), "Total planets colonized", colonizedPlanets.Count));
                txtLog.AppendText(string.Format("{0} {1}: {2}\r\n", DateTime.Now.ToString("HH:mm:ss"), "Total time", timePassed.Days + " Days " +  timePassed.Hours + " Hours " + timePassed.Minutes + " Minutes " + timePassed.Seconds + " Seconds"));
                txtLog.AppendText(string.Format("{0} {1}: {2}\r\n", DateTime.Now.ToString("HH:mm:ss"), "Total distance covered", totalDistanceCovered));
                txtLog.AppendText(string.Format("{0} {1}: {2} sq kilometers\r\n", DateTime.Now.ToString("HH:mm:ss"), "Total area colonized", totalAreaColonized));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "An error occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}
