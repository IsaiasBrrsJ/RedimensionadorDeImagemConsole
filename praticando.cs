using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace Redimensionador
{
    internal class Program
    {
        /// <summary>
        /// Praticando Redimensionamento de imagens;
        /// </summary>
        /// <param name="args"></param>
        
       
        static void Main(string[] args)
        {

            #region "Diretorios"
            var pastaOriginal = "Original";
            var pastaProcessada = "Processada";
            var pastaFinalizada = "Finalizada";

            if (!Directory.Exists(pastaOriginal))
            {
                Directory.CreateDirectory(pastaOriginal);
            }
            if (!Directory.Exists(pastaProcessada))
            {
                Directory.CreateDirectory(pastaProcessada);
            }
            if (!Directory.Exists(pastaFinalizada))
            {
                Directory.CreateDirectory(pastaFinalizada);
            }
            #endregion
            FileInfo fileInfo = null!;
            Bitmap bitmapResize = null!;
            var diretorioAtual = Environment.CurrentDirectory;
            var imagemWidth = 1080;
            var imagemHeight = 720;
            var filesDoDiretorio = Directory.EnumerateFiles(pastaOriginal);
            foreach (var FileOriginal in filesDoDiretorio)
            {
                Console.Clear();

               
                bitmapResize = new Bitmap(imagemWidth, imagemHeight);
                var image = Image.FromFile(FileOriginal);
                using (var graphicsImage = Graphics.FromImage(bitmapResize))
                {
                    graphicsImage.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphicsImage.DrawImage(image, 0, 0, imagemWidth, imagemHeight);
                    graphicsImage.Dispose();
                }
           


                Console.WriteLine("Digite o nome que deseja:  ");
                var novoNomeArquivo = Console.ReadLine();

                string trocaNomeAntigoProNovoNomeDoArquivo = FileOriginal.Replace(FileOriginal.Substring(FileOriginal.IndexOf(@"\") + 1), novoNomeArquivo);

                string arquivoProcessado =
                String.Format("Ok_Processado_{0}_{1}.png", DateTime.Now.ToString("yyyyMMdd_HHmmss"),
                novoNomeArquivo.Replace(" ", "_"));

                bitmapResize.Save(arquivoProcessado, ImageFormat.Png);
                bitmapResize.Dispose();
                image.Dispose();


                fileInfo = new FileInfo(arquivoProcessado);
                var moverParaFinalizados = Path.Combine(diretorioAtual, pastaFinalizada, fileInfo.Name);
                fileInfo.MoveTo(moverParaFinalizados);
                var nomeDoArquivoOriginal = Path.GetFileName(FileOriginal);


                var localOriginal = Path.Combine(diretorioAtual, pastaOriginal,   nomeDoArquivoOriginal);
                var localDestino =  Path.Combine(diretorioAtual, pastaProcessada, nomeDoArquivoOriginal);
                
                File.Move(localOriginal, localDestino);
               
            }
        }
      
    }
}
