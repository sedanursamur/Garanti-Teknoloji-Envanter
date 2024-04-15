using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TeknikEnvanter_3
{
    class Program
    {
        static void Main(string[] args)
        {
            int müşteriNumarası = 15000000;

            ÇalıştırmaMotoru.KomutÇalıştır("MuhasebeModulu", "MaaşYatır", new object[] { müşteriNumarası });

            ÇalıştırmaMotoru.KomutÇalıştır("MuhasebeModulu", "YıllıkÜcretTahsilEt", new object[] { müşteriNumarası });

            ÇalıştırmaMotoru.BekleyenİşlemleriGerçekleştir();

            Console.ReadKey();
        }
    }

    public class ÇalıştırmaMotoru
    {
        private static Queue<Job> queue = new Queue<Job>();

        public static object KomutÇalıştır(string modülSınıfAdı, string methodAdı, object[] inputs)
        {
            Veritabanıİşlemleri db = new Veritabanıİşlemleri();
            var job = db.GetJobByModuleClassNameAndMethodName(modülSınıfAdı, methodAdı);

            if (job == null)
            {
                return InvokeMethod(modülSınıfAdı, methodAdı, inputs);
            }
            else
            {
                queue.Enqueue(job);
                return null;
            }
        }

        public static async Task BekleyenİşlemleriGerçekleştir()
        {
            while (queue.Count > 0)
            {
                var job = queue.Dequeue();

                if (job.ExecutionTime <= DateTime.Now)
                {
                    // Zamanı geçmiş işlemler hemen yürütülür
                    await InvokeJob(job);
                }
                else
                {
                    // Zamanı gelmemiş işlemler için bekleme süresi hesaplanır
                    var delay = (int)Math.Max((job.ExecutionTime - DateTime.Now).TotalMilliseconds, 0);

                    // Belirli bir süre bekleyip sonra işlemi yürütür
                    await Task.Delay(delay);
                    await InvokeJob(job);
                }
            }
        }

        private static async Task InvokeJob(Job job)
        {
            var type = typeof(ÇalıştırmaMotoru).Assembly.GetTypes().FirstOrDefault(a => a.Name == job.ModuleClassName);
            var methodInfo = type.GetMethod(job.MethodName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            await Task.Run(() => methodInfo.Invoke(Activator.CreateInstance(type), job.GetObjectParameters()));
        }

        private static object InvokeMethod(string modülSınıfAdı, string methodAdı, object[] inputs)
        {
            var type = typeof(ÇalıştırmaMotoru).Assembly.GetTypes().FirstOrDefault(a => a.Name == modülSınıfAdı);
            var methodInfo = type.GetMethod(methodAdı, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            return methodInfo.Invoke(Activator.CreateInstance(type), inputs);
        }
    }

    public class MuhasebeModulu
    {
        private void MaaşYatır(int müşteriNumarası)
        {
            Console.WriteLine(string.Format("{0} numaralı müşterinin maaşı yatırıldı.", müşteriNumarası));
        }

        private void YıllıkÜcretTahsilEt(int müşteriNumarası)
        {
            Console.WriteLine("{0} numaralı müşteriden yıllık kart ücreti tahsil edildi.", müşteriNumarası);
        }

        private void OtomatikÖdemeleriGerçekleştir(int müşteriNumarası)
        {
            Console.WriteLine("{0} numaralı müşterinin otomatik ödemeleri gerçekleştirildi.", müşteriNumarası);
        }
    }

    public class Veritabanıİşlemleri
    {
        public Job GetJobByModuleClassNameAndMethodName(string ModulSinifAdi, string MethodAdi)
        {
            List<Job> jobs = new List<Job>();

            jobs.Add(new Job()
            {
                Id = 1,
                MethodName = "MaaşYatır",
                ModuleClassName = "MuhasebeModulu",
                ExecutionTime = new DateTime(2019, 12, 12, 12, 01, 000),
                Parameters = new List<JobParameter>() { new JobParameter() { JobId = 1, OrderNo = 1, ParameterType = ParameterType.Number, ParameterValue = "15000000" } }
            });

            return jobs.FirstOrDefault(a => a.ModuleClassName == ModulSinifAdi && a.MethodName == MethodAdi);
        }
    }
}

