using System.Diagnostics;
using System.Threading;
using System.Web;
namespace exempleTheats
{
    internal class Program
    {
        public static bool isPing = true;
        public static int rounds = 20;
        public static object locker = new object();
        static void Main()
        {
            //Exercici 2
            PutCurrentProcessorsInFile();
            //Exercici 3
            PutOneProcessInFile();
            //Exercici 4
            CountRandom();
            CountSleep();
            //Exercici 5
            Carrera();
        }
        public static void Carrera()
        {
            int nombreCamells = 3;

            // Paràmetres per a cada camell
            (int min, int max)[] parametresCamells = new (int, int)[]
            {
            (30, 70),   // Camell 1
            (50, 90),   // Camell 2
            (20, 60)    // Camell 3
            };

            for (int i = 0; i < nombreCamells; i++)
            {
                int numeroCamell = i + 1;
                int descansMin = parametresCamells[i].min;
                int descansMax = parametresCamells[i].max;

                Thread camell = new Thread(() =>
                {
                    Random rnd = new Random(Guid.NewGuid().GetHashCode());

                    for (int j = 0; j <= 100; j++)
                    {
                        Console.WriteLine($"Camell {numeroCamell} ➤ {j}");
                        int espera = rnd.Next(descansMin, descansMax + 1);
                        Thread.Sleep(espera);
                    }

                    Console.WriteLine($"El Camell {numeroCamell} ha arribat a la meta!");
                });

                camell.Start();
            }
            }
        public static void CountRandom()
        {
            for (int i = 1; i <= 5; i++)
            {
                int numeroFil = i; 
                Thread fil = new Thread(() =>
                {
                    Console.WriteLine($"Hola! Soc el fil número {numeroFil}");
                });
                fil.Start();
            }
        }
        public static void CountSleep()
        {
            for (int i = 1; i <= 5; i++)
            {
                int numeroFil = i;
                Thread fil = new Thread(() =>
                {
                    // Esperem segons el número de fil per l'ordre invers
                    Thread.Sleep((5 - numeroFil) * 200); 
                    Console.WriteLine($"Hola! Soc el fil número {numeroFil}");
                });
                fil.Start();
            }
        }
        public static void PutCurrentProcessorsInFile()
        {
            string rutaFitxer = "../../../llista_processos.txt";
            //Executa el programa dotnet amb l’argument –info. 
            var process = new Process
            {
                //Configurem el process amb la classe ProcessStartInfo
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "--info",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            //Capturem el que s’ha imprés per pantalla:
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            Console.WriteLine(output);

            var processos = Process.GetProcesses();
            using (StreamWriter escriptor = new StreamWriter(rutaFitxer))
            {
                foreach (Process os in processos)
                {
                    Console.WriteLine($"PID: {os.Id}, Name {os.ProcessName}");
                    escriptor.WriteLine($"PID: {os.Id}, Name {os.ProcessName}");
                }
            }
        }
        public static void PutOneProcessInFile()
        {
            string rutaFitxer = "../../../one_process.txt";
            Console.WriteLine("Escull un process: (PID)");
            int pid = int.Parse(Console.ReadLine());
            Process chromeP = null;
            try
            {
                chromeP = Process.GetProcessById(pid);
                ProcessThreadCollection pTC = chromeP.Threads;
                Console.WriteLine("Threats del programa {0}, Threats count: {1}", chromeP.ProcessName, pTC.Count);
                using (StreamWriter escriptor = new StreamWriter(rutaFitxer))
                {
                    foreach (ProcessThread pt in pTC)
                    {
                        Console.WriteLine($"{pt.Id} \t Container {pt.Container}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            try
            {
                ProcessModuleCollection pMC = chromeP.Modules;
                Console.WriteLine("Moduls del programa {0}, ModulCount: {1}", chromeP.ProcessName, pMC.Count);
                using (StreamWriter escriptor = new StreamWriter(rutaFitxer))
                {
                    foreach (ProcessModule pM in pMC)
                    {
                        Console.WriteLine($"{pM.ModuleName} \t Container: {pM.Container}");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        //========================APUNTES=======================
        public static void ViewCurrentProcesses()
        {
            //Executa el programa dotnet amb l’argument –info. 
            var process = new Process
            {
                //Configurem el process amb la classe ProcessStartInfo
                StartInfo = new ProcessStartInfo
                {
                    FileName = "dotnet",
                    Arguments = "--info",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                }
            };

            process.Start();

            //Capturem el que s’ha imprés per pantalla:
            string output = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            Console.WriteLine(output);

            var processos = Process.GetProcesses();
            foreach ( Process os in processos )
            {
                Console.WriteLine($"PID: {os.Id}, Name {os.ProcessName}");
            }
            
        }
        public static void CarreraApuntesClase()
        {
            //-----------------------crear thread
            Console.WriteLine("Abans de comenzar el fil");
            Thread th1 = new Thread(() => Console.WriteLine("Soc el primer teu fil 1"));
            Thread th2 = new Thread(() => Console.WriteLine("Soc el primer teu fil 2"));
            Thread th3 = new Thread(() => Console.WriteLine("Soc el primer teu fil 3"));
            Thread th4 = new Thread(() => Console.WriteLine("Soc el primer teu fil 4"));
            Thread th5 = new Thread(() => Console.WriteLine("Soc el primer teu fil 5"));

            th1.Start();
            th2.Start();
            th3.Start();
            th4.Start();
            th5.Start();
            // join sigbnifica que el hilo principal espera al hilo en el que ponemos join
            // aqui todos se pelean por llegar ante.
            th1.Join();
            th2.Join();
            th3.Join();
            th4.Join();
            th5.Join();

            Console.WriteLine("Fi del programa");
        }
        public static void ViewOneProcess()
        {
            Console.WriteLine("Escull un process: (PID)");
            int pid = int.Parse(Console.ReadLine());
            Process chromeP = null;
            try
            {
                chromeP = Process.GetProcessById(pid);
                ProcessThreadCollection pTC = chromeP.Threads;
                Console.WriteLine("Threats del programa {0}, Threats count: {1}", chromeP.ProcessName, pTC.Count);
                foreach (ProcessThread pt in pTC)
                {
                    Console.WriteLine($"{pt.Id} \t Container {pt.Container}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            try
            {
                ProcessModuleCollection pMC = chromeP.Modules;
                Console.WriteLine("Moduls del programa {0}, ModulCount: {1}", chromeP.ProcessName, pMC.Count);
                foreach (ProcessModule pM in pMC)
                {
                    Console.WriteLine($"{pM.ModuleName} \t Container: {pM.Container}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
        
        public static void PingPong()
        {
            Thread thPing = new Thread(Ping);
            Thread thPong = new Thread(Pong);
            thPing.Start();
            thPong.Start();
            thPing.Join();
            thPong.Join();
        }
        
        public static void Ping()
        {
            for(int i = 0; i< rounds; i++)
            {
                lock (locker)
                {
                    while (!isPing)
                    {
                        Monitor.Wait(locker);
                    }
                    Console.WriteLine("Ping");
                    isPing = false;
                    Monitor.Pulse(locker);
                }
                
            }
        }
        public static void Pong()
        {
            lock (locker)
            {
                while (isPing)
                {
                    Monitor.Wait(locker);
                }
                Console.WriteLine("Pong");
                isPing = true;
                Monitor.Pulse(locker);
            }
        }
        
        public static void Count()
        {
            const int num = 10;
            for (int i = num; i > 0; i--)
                {
                    Console.WriteLine(i);
                }
        }

    }
}
