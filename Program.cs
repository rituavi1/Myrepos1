using System;
using System.Collections.Generic;
using System.Linq;

namespace DriverApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Trip> driverList = new List<Trip>();
                                        
                Trip trip1 = new Trip();
                trip1.driverName = "Dan";             
                trip1.startTime = new TimeSpan(7,15,0);
                trip1.endTime = new TimeSpan(7,45,0);
                trip1.milesDriven = 17.3;
                driverList.Add(trip1);

                Trip trip2 = new Trip();
                trip2.driverName = "Dan";             
                trip2.startTime = new TimeSpan(6,12,0);
                trip2.endTime = new TimeSpan(6,32,0);
                trip2.milesDriven = 21.8;
                driverList.Add(trip2);

               Trip trip3 = new Trip();
                trip3.driverName = "Alex";             
                trip3.startTime = new TimeSpan(12,1,0);
                trip3.endTime = new TimeSpan(13,16,0);
                trip3.milesDriven = 42.0;
                driverList.Add(trip3);
                
                Console.WriteLine("----- Input Details -----");
                foreach(var driver in driverList)
                {    
                string driverInfo = string.Format("Trip {0} {1} {2} {3}", driver.driverName, driver.startTime, driver.endTime, driver.milesDriven);         
                Console.WriteLine(driverInfo);                                 
                }     

                Console.WriteLine("Enter Driver Name: ");
                string driveName = Console.ReadLine();

         
        var driversList =
    from p in driverList
    group p by p.driverName into g
    where g.Count() > 1
    select g.Key;

if(driversList.Any() && driversList.Any(p => driversList.Contains(driveName)))
{
    IEnumerable<Trip> duplicatedDrivers = driverList.FindAll(p => driversList.Contains(p.driverName));
    var result = CalculateMilesAndSpeed(duplicatedDrivers.ToList());
    Console.WriteLine(result);
}
  
else
{
    var driversList1 =
    from p in driverList
    where p.driverName.Equals(driveName)  
    select p;

    var result = CalculateMilesAndSpeed(driversList1.ToList());
    Console.WriteLine(result);
}
  }
        private static string CalculateMilesAndSpeed(List<Trip> duplicated)
        {
            string driverName = string.Empty;
            Double totalTimeInMinutes = 0;
            Double previousTotalTimeInMinutes = 0;
            Double milesDriven = 0;
            Double previousMilesDriven = 0;
            TimeSpan timeDifference = new TimeSpan(0,0,0);
            for(int i = 0;i<duplicated.Count;i++)
            {
                  if(duplicated[i].startTime.Hours <= duplicated[i].endTime.Hours) 
                  {
                     timeDifference = duplicated[i].endTime.Subtract(duplicated[i].startTime); 
                  }
                  else
                  {
                      return null;
                  }
                  milesDriven = previousMilesDriven + duplicated[i].milesDriven;

          if(timeDifference.Hours != 0)
          {
              totalTimeInMinutes = (timeDifference.Hours * 60) + timeDifference.Minutes;
              totalTimeInMinutes = totalTimeInMinutes + previousTotalTimeInMinutes;
          }
          else
          {
             totalTimeInMinutes = timeDifference.Minutes;
             totalTimeInMinutes = totalTimeInMinutes + previousTotalTimeInMinutes;
          }    
               previousTotalTimeInMinutes = totalTimeInMinutes;
               previousMilesDriven = milesDriven;
               driverName = duplicated[i].driverName;
            }
       
          double speed = (milesDriven / totalTimeInMinutes) * 60;
          if(string.IsNullOrEmpty(driverName) || speed < 5 || speed > 100)
          {
              return null;
          }
          return Convert.ToString(driverName + ": " + Math.Round(milesDriven) + " miles " + "@ " + Math.Round(speed) + " mph");
        }
    }

   internal class Driver
    {
      internal string driverName {get; set;}    
    }

    internal class Trip: Driver
    {
       internal  TimeSpan startTime {get; set;}
       internal TimeSpan endTime {get; set;}
       internal double milesDriven {get; set;}
       
       }
    }

    


