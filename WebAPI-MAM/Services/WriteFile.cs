namespace WebAPI_MAM.Services
{
    public class WriteFile : IHostedService
    {
        private readonly IWebHostEnvironment env;

        private readonly string FileName = "EventManager.txt";
        // private readonly string archivo = "ListadoAlumnos.txt";
        private Timer timer;

        public WriteFile(IWebHostEnvironment env)
        {
            this.env = env;

        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            //Se ejecuta cuando cargamos la aplicacion 1 vez
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(2));
            Escribir("Process Started");
            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Se ejecuta cuando detenemos la aplicacion aunque puede que no se ejecute por algun error. 
            timer.Dispose();
            Escribir("Process Ended");
            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            Escribir("Excecuting Process: " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
            //GuardarAlumnos();
        }
        private void Escribir(string msg)
        {
            string ex = "";
            try
            {
                var rute = $@"{env.ContentRootPath}wwwroot\{FileName}";

                using (StreamWriter writer = new StreamWriter(rute, append: true)) { writer.WriteLine(msg); }
            }
            catch (Exception e) { ex = e.ToString(); }

        }

        private void GuardarAlumnos()
        {
            //var ruta = $@"{env.ContentRootPath}\wwwroot\{archivo}";
            //ActionResult task = alumnosController.ObtenerGuid();
            //object Alumno = task.Result.Value;
            //using (StreamWriter writer = new StreamWriter(ruta, append: true)) { writer.WriteLine(Alumno); }
        }
    }
}
